module SlaCalculator.Updates

open System
open Elmish
open SlaCalculator.Models
open SlaCalculator.Messages
open SlaCalculator.Calculate

let init _ =
    emptyModel, Cmd.none

let withComponentFromModel model =
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
        Components   = newComponent :: model.Components
        EntryPoint   = entryPoint
        CompositeSLA = match entryPoint with
                       | None    -> None
                       | Some ep -> calculateCompositeSla ep |> Some }

let private toggleDependency model comp =
    match model.Dependencies |> List.contains comp with
    | true  -> model.Dependencies |> List.except [comp]
    | false -> comp :: model.Dependencies

let update message model =
    match message with    
    | ChangeToTab tab    -> { model with CurrentTab = tab }                       , Cmd.none
    | ChangeName name    -> { model with Name = name }                            , Cmd.none
    | ChangeSLA sla      -> { model with SLA = sla }                              , Cmd.none
    | ChangeIsEntryPoint -> { model with IsEntryPoint = not model.IsEntryPoint }  , Cmd.none
    | ToggleDependency d -> { model with Dependencies = toggleDependency model d }, Cmd.none
    | ClickAdd           -> model |> withComponentFromModel                       , Cmd.none