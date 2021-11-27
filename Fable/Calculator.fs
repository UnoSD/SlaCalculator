module SlaCalculator.Calculator

open System
open Fable.FontAwesome
open Fable.React.Props
open Feliz
open Fulma
open SlaCalculator.Helpers
open SlaCalculator.Models
open SlaCalculator.Messages
open Fable.React

module Icon = Free.Fa.Solid

let private iconField (labelText : string) icon input =
    Field.div [ ]
              [ Label.label [] [ Html.text labelText ]
                Control.div [ Control.HasIconLeft ]
                            [ input
                              Icon.icon [ Icon.Size IsSmall; Icon.IsLeft ]
                                        [ Fa.i [ icon ] [ ] ] ] ]

let private row (name : string) (sla : string) (dependsOn : string) (entrypoint : bool) =
    [
        Html.tableCell [ Html.strong [ Html.text name ] ]
        Html.tableCell [ Html.strong [ Html.text sla ] ]
        Html.tableCell [ Html.text dependsOn ]
        Html.tableCell [ Html.text (entrypoint.ToString()) ]
    ] |>
    Html.tableRow

let private card content =
    card content Html.none

let private th (text : string) =
    Html.tableHeader [ Html.text text ]

let private tableHeader (content : ReactElement list) =
    Html.thead [ Html.tableRow content ]

let totals model =
    Level.level [ ]
                [ Level.item [ Level.Item.HasTextCentered ]
                             [ div [ ]
                                   [ Level.heading [ ]
                                                   [ str "Composite SLA" ]
                                     Level.title   [ ]
                                                   [ str <| sprintf "%f%%" model.CompositeSLA ] ] ]
                  Level.item [ Level.Item.HasTextCentered ]
                             [ div [ ]
                                   [ Level.heading [ ]
                                                   [ str "Number of components" ]
                                     Level.title   [ ]
                                                   [ str <| sprintf "%i" (List.length model.Components) ] ] ]
                  Level.item [ Level.Item.HasTextCentered ]
                             [ div [ ]
                                   [ Level.heading [ ]
                                                   [ str "Downtime per year" ]
                                     Level.title   [ ]
                                                   [ str <| sprintf "%f hours" ((100m - model.CompositeSLA) * 365m * 24m / 100m) ] ] ] ]

let calculatorCard model dispatch =
    let isSlaValid, value =
        Decimal.TryParse model.SLA
    
    card [
        Html.form [
            Box.box' [] [ totals model ]
            
            iconField "Name" Icon.Atom (Input.text [
                Input.OnChange (fun ev -> ev.Value |> ChangeName |> dispatch)
            ])
            iconField "SLA" Icon.Ambulance <| Input.text [
                if not isSlaValid then Input.Color Color.IsDanger else ()
                Input.OnChange (fun ev -> ev.Value |> ChangeSLA |> dispatch)
            ]
            Select.select [
                Select.IsMultiple
            ] [ select [ Multiple true
                         Size 4. ] [
                yield! model.Components |> List.map (fun c -> option [ Value c.Name ] [ str c.Name ])
            ] ]
            Button.button [
                Button.OnClick (fun _ -> dispatch ClickAdd)
            ] [ str "Add" ]
            Button.button [] [ str "Export" ]
            Button.button [] [ str "Import" ]
            
            Html.table [
                tableHeader [
                    th "Name"
                    th "SLA"
                    th "Depends on"
                    th "Entry point"
                ]
                Html.tableBody [
                    yield! (model.Components |> List.map (fun x -> row x.Name (x.SLA.ToString()) (x.Dependencies |> List.map (function | Distributed _ -> "db" | Direct lx -> lx.Name) |> String.concat ", ") (x = model.EntryPoint.Value)))
                ]
            ]
        ]
    ]