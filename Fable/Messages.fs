module SlaCalculator.Messages

open SlaCalculator.Models

type Message =
    | ChangeToTab of Tab
    | ChangeName of string
    | ChangeSLA of string
    | ClickAdd
    | ClickUpdate of Component
    | ToggleIsEntryPoint
    | ToggleDependency of Dependency
    | SetEntryPoint of Component
    | EditComponent of Component
    | DeleteComponent of Component
    | Export
    | CompletedImport of string
    | FailedImport
    | Reset
    | LoadExample
    | CancelEdit
    | UpdateDiagram