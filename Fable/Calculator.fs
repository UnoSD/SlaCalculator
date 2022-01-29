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

let private tableColumn (text : string) =
    Html.tableHeader [ Html.text text ]

let private tableHeader (content : ReactElement list) =
    Html.thead [ Html.tableRow content ]

let private totals model =
    let level =
        Level.level [ ]
        
    let downtimeHoursPerYear =
        (100m - model.CompositeSLA) * 365m * 24m / 100m
    
    let tile name value =
        [ Html.div [ Level.heading [ ]
                                   [ str name ]
                     Level.title   [ ]
                                   [ str value ] ] ]
        |> Level.item [ Level.Item.HasTextCentered ]
    
    level [
        tile "Composite SLA" (sprintf "%f%%" model.CompositeSLA)
        
        tile "Number of components" (sprintf "%i" (List.length model.Components))
        
        tile "Downtime per year" (sprintf "%f hours" downtimeHoursPerYear)
    ]

let private componentsTable model =
    let componentRow (comp : Component) =
        let sla =
            comp.SLA.ToString()
            
        let dependsOn =
            comp.Dependencies
            |> List.map (function
                         | Distributed _  -> "db"
                         | Direct      lx -> lx.Name)
            |> String.concat ", "
        
        let isEntryPoint =
            comp = model.EntryPoint.Value
        
        row comp.Name sla dependsOn isEntryPoint
    
    Html.table [
        tableHeader [
            tableColumn "Name"
            tableColumn "SLA"
            tableColumn "Depends on"
            tableColumn "Entry point"
        ]
        
        Html.tableBody (
            List.map componentRow model.Components
        )
    ]

let private dependenciesBox model =
    Select.select [
        Select.IsMultiple
    ] [ select [ Multiple true
                 Size 4. ] [
        yield! model.Components |> List.map (fun c -> option [ Value c.Name ] [ str c.Name ])
    ] ]

let private button dispatch name event =
    Button.button [
        Button.OnClick (fun _ -> dispatch event)
    ] [ str name ]

let private textField dispatch isValid label icon event =
    Input.text [
        if isValid then () else Input.Color Color.IsDanger
        Input.OnChange (fun ev -> ev.Value |> event |> dispatch)
    ]
    |> iconField label icon

let calculatorCard model dispatch =
    let button = button dispatch
    let componentsTable = componentsTable model
    let dependenciesBox = dependenciesBox model
    let textField = textField dispatch
    let isSlaValid = (Decimal.TryParse model.SLA |> fst)
    let totalsBox = Box.box' [] [ totals model ]
    let container (content : seq<ReactElement>) = card [ Html.div content ]
    
    container [
        totalsBox
        
        textField true "Name" Icon.Atom ChangeName
        
        textField isSlaValid "SLA" Icon.Ambulance ChangeSLA
        
        dependenciesBox
        
        button "Add" ClickAdd
        
        Button.button [] [ str "Export" ]
        
        Button.button [] [ str "Import" ]
        
        componentsTable
    ]