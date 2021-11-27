module SlaCalculator.Page

open SlaCalculator.Models
open SlaCalculator.Calculator
open SlaCalculator.About

let page model =
    model |>
    match model.CurrentTab with
    | Calculator -> calculatorCard
    | About      -> aboutCard