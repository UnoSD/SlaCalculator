module SlaCalculator.Updates

open System
open Elmish
open SlaCalculator.Models
open SlaCalculator.Messages
open Thoth.Json
open VisNetwork

let init _ =
    emptyModel, Cmd.none

let private withComponentFromModel model =
    let newComponent = {
        Name = model.Name
        SLA = Decimal.Parse model.SLA
        Dependencies = model.Dependencies
                       |> List.partition (function | Direct _ -> true | Distributed _ -> false)
                       |> (fun (direct, distributed) -> (distributed
                                                         |> List.collect (function | Distributed x -> x | _ -> failwith "Impossible")
                                                         |> Distributed) :: direct)
                       |> List.filter (function | Distributed [] -> false | _ -> true)
    }

    let entryPoint = 
        match model.IsEntryPoint with
        | true    -> Some newComponent
        | false   -> model.EntryPoint
        
    { emptyModel with
        Components = newComponent :: model.Components
        EntryPoint = entryPoint }

let private toggleDependency model dep =
    match dep, model.Dependencies |> List.contains dep with
    | Direct _, true     -> model.Dependencies |> List.except [dep]
    | Direct _, false    -> dep :: model.Dependencies
    | Distributed [x], _ -> let currentDependencies = model.Dependencies
                            let updatedDependencies = 
                                model.Dependencies
                                |> List.choose (function
                                                | Distributed [d] when d = x -> None
                                                | Distributed dps            -> List.except [x] dps |> Distributed |> Some
                                                | d                          -> Some d)
                            match currentDependencies = updatedDependencies with
                            | true  -> dep :: model.Dependencies
                            | false -> updatedDependencies
    | _                  -> failwith "The view should only send single item list of distributed dependencies"

let rec private replaceComponent oldComponent newComponent (components : Component list) : Component list =
    let rec replaceDependencies (dependencies : Dependency list) oldComponent newComponent : Dependency list =
        let replace oldComponent newComponent =
            function
            | Direct d when d = oldComponent -> Direct newComponent
            | Direct d                       -> { d with Dependencies = replaceDependencies d.Dependencies oldComponent newComponent }
                                                |> Direct // Non-tail recursion
                                                    
            | Distributed []                              -> failwith "Empty distributed dep"
            | Distributed [d] when d = oldComponent       -> Distributed [newComponent]
            | Distributed (d :: ds) when d = oldComponent -> newComponent :: replaceComponent oldComponent newComponent ds |> Distributed // Non-tail recursion
            | Distributed (d :: ds)                       -> { d with Dependencies = replaceDependencies d.Dependencies oldComponent newComponent } ::
                                                             replaceComponent oldComponent newComponent ds |> Distributed // Non-tail recursion
        
        dependencies
        |> List.map (replace oldComponent newComponent)
    
    match components with
    | [] -> []
    | [x] when x = oldComponent     -> [newComponent] // I don't think we need to check dependencies, if it depends on itself it's a circular dependency and would be an infinite loop anyway (avoid this in code as it could be possible)
    | [x]                           -> [ { x with Dependencies = replaceDependencies x.Dependencies oldComponent newComponent } ]
    | x :: xs when x = oldComponent -> newComponent :: replaceComponent oldComponent newComponent xs // Non-tail recursion
    | x :: xs                       -> { x with Dependencies = replaceDependencies x.Dependencies oldComponent newComponent } ::
                                       replaceComponent oldComponent newComponent xs // Non-tail recursion

let private withReplacementComponent oldComponent newComponent model =
    { model with
        Components = model.Components |> replaceComponent oldComponent newComponent
        EntryPoint = Option.map (List.singleton
                                 >> replaceComponent oldComponent newComponent
                                 >> List.head) model.EntryPoint }

let private withUpdatedComponentFromModel oldComponent model =
    let newComponent = {
        Name = model.Name
        SLA = Decimal.Parse model.SLA
        Dependencies = model.Dependencies
    }

    let replacedComponentModel =
        withReplacementComponent oldComponent newComponent model
        
    { replacedComponentModel with
        Name = ""
        SLA = ""
        IsEntryPoint = false
        Dependencies = []
        EditingComponent = None }

let private withComponentEdit (comp : Component) model =
    { model with
        Name = comp.Name
        SLA = comp.SLA.ToString()
        IsEntryPoint = model.EntryPoint |> Option.map (fun c -> c = comp) |> Option.defaultValue false
        Dependencies = comp.Dependencies
        EditingComponent = Some comp }

let private exportState (model : Model) =
    let anchor = Browser.Dom.document.createElement "a"
    let json = Encode.Auto.toString(4, model, extra = (Extra.empty |> Extra.withDecimal))
    let encodedContent = json |> sprintf "data:text/plain;charset=utf-8,%s" |> Fable.Core.JS.encodeURI
    anchor.setAttribute("href", encodedContent)
    anchor.setAttribute("download", "export.json")
    anchor.click()

let private importState json oldState =
    match Decode.Auto.fromString<Model>(json, extra = (Extra.empty |> Extra.withDecimal)) with
    | Ok newState -> newState
    | Error error -> printfn "%s" error; oldState

let private cancelEditing model =
    { model with
        Name = ""
        SLA = "0.0"
        IsEntryPoint = false
        Dependencies = []
        EditingComponent = None }

let private withoutComponent toRemove model =
    let rec removeDependency toRemove dependencies : Dependency list =
        dependencies
        |> List.filter (function
                        | Direct d        -> d <> toRemove
                        | Distributed []  -> failwith "Empty distributed dependency"
                        | Distributed [x] -> x <> toRemove
                        | _               -> true)
        |> List.map (function
                     | Distributed (_ :: _ :: _) & Distributed ds -> ds
                                                                     |> List.filter ((<>)toRemove)
                                                                     |> List.map (fun d -> { d with Dependencies = removeDependency toRemove d.Dependencies })
                                                                     |> Distributed
                     | x -> x)
    
    let removeEntrypoint (ep : Component) =
        if ep = toRemove then
            None
        else
            Some { ep with Dependencies = removeDependency toRemove ep.Dependencies }
    
    { model with
        Components = List.filter ((<>)toRemove) model.Components
                     |> List.map (fun c -> { c with Dependencies = removeDependency toRemove c.Dependencies })
        EntryPoint = Option.bind removeEntrypoint model.EntryPoint }

let private updateDiagramFromModel components =    
    let rec getIcon (name : string) =
        Map.tryFindKey (fun k _ -> name.Contains(k)) Icons.Azure.iconsMap
        |> Option.map (fun k -> Map.find k Icons.Azure.iconsMap)
        |> Option.defaultValue "Azure/General/10001-icon-service-All-Resources.svg"
    
    let createNode (comp : Component) =
        let sanitizedName (name : string) =
            name.Replace("es ", "")
                .Replace("s ", "")
                .Replace(" ", "")
                .ToLowerInvariant()
                .Replace("azure", "")
                .Replace("-", "")
        
        {
            id    = comp.Name // Temporary, not enough to identify uniquely a component, to fix
            label = comp.Name
            image = getIcon (sanitizedName comp.Name)
            shape = "image"
        }
    
    let createEdge (c1 : Component) (c2 : Component) =
        {
            from = c1.Name
            ``to`` = c2.Name
            length = 200
        }
    
    let rec createEdges (comp : Component) dependencies =
        match dependencies with
        | []                  -> []
        | Direct      x :: xs -> [ createEdge comp x ] @ createEdges comp xs
        | Distributed x :: xs -> List.map (createEdge comp) x @ createEdges comp xs
    
    let rec getVisNodesAndEdges components nodes edges =
        match components with
        | [     ] -> nodes                            , edges
        
        | x :: xs -> let n = createNode x :: nodes
                     let e = createEdges x x.Dependencies @ edges
                     
                     getVisNodesAndEdges xs n e
    
    getVisNodesAndEdges components [] []
    |> (fun (n, e) -> (n |> Array.ofList, e |> Array.ofList))
    ||> updateDiagram

let update message model =
    match message with    
    | ChangeToTab tab    -> { model with CurrentTab = tab }                       , Cmd.none
    | ChangeName name    -> { model with Name = name }                            , Cmd.ofMsg UpdateDiagram
    | ChangeSLA sla      -> { model with SLA = sla }                              , Cmd.none
    | ToggleIsEntryPoint -> { model with IsEntryPoint = not model.IsEntryPoint }  , Cmd.none
    | ToggleDependency d -> { model with Dependencies = toggleDependency model d }, Cmd.ofMsg UpdateDiagram
    | SetEntryPoint comp -> { model with EntryPoint = Some comp }                 , Cmd.none
    | EditComponent comp -> model |> withComponentEdit comp                       , Cmd.none
    | DeleteComponent co -> model |> withoutComponent co                          , Cmd.ofMsg UpdateDiagram
    | ClickAdd           -> model |> withComponentFromModel                       , Cmd.ofMsg UpdateDiagram
    | ClickUpdate comp   -> model |> withUpdatedComponentFromModel comp           , Cmd.ofMsg UpdateDiagram
    | Export             -> exportState model; model                              , Cmd.none
    | CompletedImport dx -> importState dx model                                  , Cmd.ofMsg UpdateDiagram
    | FailedImport       -> printfn "Import failed"; model                        , Cmd.none
    | Reset              -> emptyModel                                            , Cmd.none
    | LoadExample        -> exampleModel                                          , Cmd.ofMsg UpdateDiagram
    | CancelEdit         -> cancelEditing model                                   , Cmd.none
    | UpdateDiagram      -> updateDiagramFromModel model.Components; model        , Cmd.none