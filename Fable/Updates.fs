module SlaCalculator.Updates

open System
open Elmish
open SlaCalculator.Models
open SlaCalculator.Messages
open SlaCalculator.Calculate

let init _ =
    emptyModel, Cmd.none

let withComponentFromModel model =
    { emptyModel with
        Components = {
            Name = model.Name
            SLA = Decimal.Parse model.SLA
            Dependencies = model.Components |> List.filter (fun x -> List.contains x.Name []) |> List.map Direct
        } :: model.Components
        EntryPoint = model.EntryPoint
        CompositeSLA = match model.EntryPoint with
                       | None    -> 100m
                       | Some ep -> calculateCompositeSla ep }

let update message model =
    match message with    
    | ChangeToTab tab -> { model with CurrentTab = tab }, Cmd.none
    | ChangeName name -> { model with Name = name }     , Cmd.none
    | ChangeSLA sla   -> { model with SLA = sla }       , Cmd.none
    | ClickAdd        -> model |> withComponentFromModel, Cmd.none