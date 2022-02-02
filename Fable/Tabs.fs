module SlaCalculator.Tabs

open Fulma
open Fable.React
open Fable.React.Props
open SlaCalculator.Messages
open SlaCalculator.Models

let tabs model dispatch =
    let tab tabType title =
        Tabs.tab [ Tabs.Tab.IsActive (model.CurrentTab = tabType) ]
                 [ a [ OnClick (fun _ -> ChangeToTab tabType |> dispatch) ] [ str title ] ]
    
    let howToLinkTab title =
        Tabs.tab [ Tabs.Tab.IsActive false ]
                 [ a [ Href "https://github.com/UnoSD/SlaCalculator/blob/main/README.md"
                       Target "_blank"
                       Rel "noopener noreferrer" ] [ str title ] ]
    
    Tabs.tabs [ Tabs.IsCentered ]
              [ tab Calculator "Calculator"
                howToLinkTab   "How to use it"
                tab About      "About"  ]