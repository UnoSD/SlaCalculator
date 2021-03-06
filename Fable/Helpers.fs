module SlaCalculator.Helpers

open Fable.React.Props
open Fable.React
open Fulma

let card content footer =
    Card.card [ GenericOption.Modifiers [ Modifier.Spacing (Spacing.Margin, Spacing.Is6) ] ]
              [ Card.content [ ]
                             [ Content.content [] content
                               footer ] ]
              
let cardFooterLink text url =
    Card.Footer.a [ Props [ Href url
                            Target "_blank" ] ]
                  [ str text ]