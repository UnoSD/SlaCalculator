module SlaCalculator.Messages

open SlaCalculator.Models

type Message =
    | ChangeToTab of Tab
    | ChangeName of string
    | ChangeSLA of string
    | ClickAdd
    | ToggleIsEntryPoint
    | ToggleDependency of Component
    | SetEntryPoint of Component