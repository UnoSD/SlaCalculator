module SlaCalculator.View

open SlaCalculator.Tabs
open SlaCalculator.Page
open Feliz

let view model dispatch =
    Html.div [ tabs model dispatch
               page model dispatch ]