module SlaCalculator.Messages

open SlaCalculator.Models

type Message =
    | ChangeToTab of Tab
    | ChangeName of string
    | ChangeSLA of string
    | ClickAdd
    | ChangeIsEntryPoint