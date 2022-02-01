module SlaCalculator.Updates

open System
open Elmish
open SlaCalculator.Models
open SlaCalculator.Messages
open Thoth.Json

let init _ =
    emptyModel, Cmd.none

let private withComponentFromModel model =
    let newComponent = {
        Name = model.Name
        SLA = Decimal.Parse model.SLA
        Dependencies = model.Dependencies
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
    | Distributed [x], _ -> model.Dependencies
                            |> List.choose (function
                                            | Direct d                   -> Direct d |> Some
                                            | Distributed [d] when d = x -> None
                                            | Distributed dps            -> List.except [x] dps |> Distributed |> Some)
    | _                  -> failwith "The view should only send single item list of distributed dependencies"

let private withReplacementComponent comp replacement model =
    let rec removeComponent components =
        let removeDepsComponent =
            List.choose (function
                        | Distributed cs         -> match cs with
                                                    | [  ]                -> failwith ("Distributed dependency " +
                                                                                       "on no component, " +
                                                                                       "state is corrupted")
                                                    | [co] when co = comp -> replacement |> Option.map (List.singleton
                                                                                                        >> Distributed)
                                                    |  cs                 -> cs
                                                                             |> List.except [comp]
                                                                             |> Distributed
                                                                             |> Some
                        | Direct d when d = comp -> Option.map Direct replacement
                        | dep                    -> Some dep)
        
        match components with
        | []                    -> []
        | x :: xs when comp = x -> (replacement |> Option.toList) @ removeComponent xs  
        | x :: xs               -> { x with Dependencies = removeDepsComponent x.Dependencies } :: removeComponent xs  
    
    { model with
        Components = model.Components |> removeComponent
        EntryPoint = match model.EntryPoint with
                     | Some c when c = comp -> replacement
                     | _                    -> model.EntryPoint }

let private withUpdatedComponentFromModel comp model =
    let updatedComponent = Some {
        Name = model.Name
        SLA = Decimal.Parse model.SLA
        Dependencies = model.Dependencies
    }

    withReplacementComponent comp updatedComponent model

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

let update message model =
    match message with    
    | ChangeToTab tab    -> { model with CurrentTab = tab }                       , Cmd.none
    | ChangeName name    -> { model with Name = name }                            , Cmd.none
    | ChangeSLA sla      -> { model with SLA = sla }                              , Cmd.none
    | ToggleIsEntryPoint -> { model with IsEntryPoint = not model.IsEntryPoint }  , Cmd.none
    | ToggleDependency d -> { model with Dependencies = toggleDependency model d }, Cmd.none
    | SetEntryPoint comp -> { model with EntryPoint = Some comp }                 , Cmd.none
    | EditComponent comp -> model |> withComponentEdit comp                       , Cmd.none
    | DeleteComponent co -> model |> withReplacementComponent co None             , Cmd.none
    | ClickAdd           -> model |> withComponentFromModel                       , Cmd.none
    | ClickUpdate comp   -> model |> withUpdatedComponentFromModel comp           , Cmd.none
    | Export             -> exportState model; model                              , Cmd.none
    | CompletedImport dx -> importState dx model                                  , Cmd.none
    | FailedImport       -> printfn "Import failed"; model                        , Cmd.none
    | Reset              -> emptyModel                                            , Cmd.none
    | LoadExample        -> exampleModel                                          , Cmd.none
    | CancelEdit         -> cancelEditing model                                   , Cmd.none