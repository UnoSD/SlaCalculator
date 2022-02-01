module SlaCalculator.Updates

open System
open Elmish
open SlaCalculator.Models
open SlaCalculator.Messages

let init _ =
    emptyModel, Cmd.none

let private withComponentFromModel model =
    let newComponent = {
        Name = model.Name
        SLA = Decimal.Parse model.SLA
        Dependencies = model.Dependencies |> List.map Direct
    }

    let entryPoint = 
        match model.IsEntryPoint with
        | true    -> Some newComponent
        | false   -> model.EntryPoint
        
    { emptyModel with
        Components = newComponent :: model.Components
        EntryPoint = entryPoint }

let private toggleDependency model comp =
    match model.Dependencies |> List.contains comp with
    | true  -> model.Dependencies |> List.except [comp]
    | false -> comp :: model.Dependencies

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
        Dependencies = model.Dependencies |> List.map Direct
    }

    withReplacementComponent comp updatedComponent model

let private withComponentEdit (comp : Component) model =
    { model with
        Name = comp.Name
        SLA = comp.SLA.ToString()
        IsEntryPoint = model.EntryPoint |> Option.map (fun c -> c = comp) |> Option.defaultValue false
        Dependencies = comp.Dependencies |> List.map (function | Direct d -> d | _ -> failwith "Not implemented")
        EditingComponent = Some comp }

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