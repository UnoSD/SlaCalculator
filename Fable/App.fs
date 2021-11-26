module App

open Elmish
open Elmish.HMR
open Elmish.Debug
open SlaCalculator.Model
open SlaCalculator.Updates
open SlaCalculator.View

Program.mkProgram init update view
|> Program.withReactSynchronous "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run