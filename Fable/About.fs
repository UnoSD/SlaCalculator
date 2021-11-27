module SlaCalculator.About

open Fulma
open SlaCalculator.Helpers
open Fable.React

let private buttons =
    Card.footer []
                [ cardFooterLink "Contact" "https://github.com/UnoSD/SlaCalculator/issues"
                  cardFooterLink "GitHub"  "https://github.com/UnoSD"
                  cardFooterLink "Blog"    "https://dev.to/unosd"                           ]

let aboutCard _ _ =
    let modifiers =
        Container.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ]

    card [ Hero.body []
                     [ Container.container [ Container.IsFluid
                                             modifiers ]
                     [ Heading.h1 [] [ str "SlaCalculator" ]
                       Heading.h4 [ Heading.IsSubtitle ] [ str "Calculate solutions uptime" ] ] ] ]
         buttons