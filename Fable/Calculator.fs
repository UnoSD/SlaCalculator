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
open SlaCalculator.Calculate

module Icon = Free.Fa.Solid

let private iconField (labelText : string) icon input =
    Field.div [ ]
              [ Label.label [] [ Html.text labelText ]
                Control.div [ Control.HasIconLeft ]
                            [ input
                              Icon.icon [ Icon.Size IsSmall; Icon.IsLeft ]
                                        [ Fa.i [ icon ] [ ] ] ] ]

let private row dispatch (entrypoint : Component option) (comp : Component) =
    let sla =
        comp.SLA.ToString()
        
    let dependsOn =
        comp.Dependencies
        |> List.map (function
                     | Distributed _  -> "db"
                     | Direct      lx -> lx.Name)
        |> String.concat ", "
    
    let isEntrypoint =
        match entrypoint with
        | Some ep -> comp = ep
        | None    -> false
    
    let button color icon disabled =
        Button.button [
            Button.Color color
            Button.Disabled disabled
            Button.OnClick (fun _ -> comp |> SetEntryPoint |> dispatch)
        ] [ Fa.i [ icon ] [] ]
    
    [
        Html.tableCell [ Html.strong [ Html.text comp.Name ] ]
        Html.tableCell [ Html.strong [ Html.text sla ] ]
        Html.tableCell [ Html.text dependsOn ]
        Html.tableCell [ Html.text (isEntrypoint.ToString()) ]
        Html.tableCell [ Button.list [ Button.List.Option.HasAddons ] [
            button IsSuccess Icon.Edit false
            button IsDanger Icon.Ban false
            button IsInfo Icon.ArrowUp isEntrypoint
        ] ]
    ] |>
    Html.tableRow

let private card content =
    card content Html.none

let private tableColumn (text : string) =
    Html.tableHeader [ Html.text text ]

let private tableHeader (content : ReactElement list) =
    Html.thead [ Html.tableRow content ]

let private totals model =
    let compositeSla =
        match model.EntryPoint with
        | None    -> None
        | Some ep -> calculateCompositeSla ep |> Some
    
    let level =
        Level.level [ ]
        
    let downtimeHoursPerYear =
        match compositeSla with
        | Some sla -> (100m - sla) * 365m * 24m / 100m |> sprintf "%f hours"
        | None     -> "Missing entrypoint"
    
    let compositeSla =
        match compositeSla with
        | Some sla -> sprintf "%f%%" sla
        | None     -> "Missing entrypoint"
    
    let tile name value =
        [ Html.div [ Level.heading [ ]
                                   [ str name ]
                     Level.title   [ ]
                                   [ str value ] ] ]
        |> Level.item [ Level.Item.HasTextCentered ]
    
    level [
        tile "Composite SLA" compositeSla
        
        tile "Number of components" (sprintf "%i" (List.length model.Components))
        
        tile "Downtime per year" downtimeHoursPerYear
    ]

let private componentsTable model dispatch =
    Html.table [
        tableHeader [
            tableColumn "Name"
            tableColumn "SLA"
            tableColumn "Depends on"
            tableColumn "Entry point"
            tableColumn "Edit"
        ]
        
        Html.tableBody (
            List.map (row dispatch model.EntryPoint) model.Components
        )
    ]

let private dependenciesBox model dispatch =
    let createOption (comp : Component) =
        option [
            // To prevent flickering when selecting dependencies, we can try and keep the original order
            OnClick (fun _ -> comp |> ToggleDependency |> dispatch)
        ] [ str comp.Name ]
    
    Select.select [
        Select.IsMultiple
    ] [ select [ Multiple true
                 ValueMultiple (model.Dependencies |> List.map (fun c -> c.Name) |> Array.ofList)
                 ReadOnly true
                 Size 4. ] [
        yield! model.Components |> List.map createOption
    ] ]

let private button dispatch color name event =
    Button.button [
        Button.Color color
        Button.OnClick (fun _ -> dispatch event)
    ] [ str name ]

let private textField dispatch value isValid label icon event =
    Input.text [
        if isValid then () else Input.Color Color.IsDanger
        Input.OnChange (fun ev -> ev.Value |> event |> dispatch)
        Input.Value value
    ]
    |> iconField label icon

let private checkBox dispatch isChecked isDisabled text event =
    let options = [
            Props [
                Disabled isDisabled
                OnClick (fun _ -> if isDisabled then () else event |> dispatch)
                ReadOnly true
                Checked isChecked
            ]
        ]
    
    [ Checkbox.input options
      str text ]
    |> Checkbox.checkbox options
    |> List.singleton
    |> Control.div []
    |> List.singleton
    |> Field.div []

let calculatorCard model dispatch =
    let button color = button dispatch color
    let componentsTable = componentsTable model dispatch
    let dependenciesBox = dependenciesBox model dispatch
    let textField = textField dispatch
    let isSlaValid = (Decimal.TryParse model.SLA |> fst)
    let totalsBox = Box.box' [] [ totals model ]
    let container (content : seq<ReactElement>) = card [ Html.div content ]
    let entryPointSelector = checkBox dispatch model.IsEntryPoint model.EntryPoint.IsSome "Entrypoint" ToggleIsEntryPoint
    
    container [
        totalsBox
        
        textField model.Name true "Name" Icon.Atom ChangeName
        
        textField model.SLA isSlaValid "SLA" Icon.Ambulance ChangeSLA
        
        entryPointSelector
        
        dependenciesBox
        
        button IsPrimary "Add" ClickAdd
        
        Button.button [ Button.Color IsSuccess ] [ str "Export" ]
        
        Button.button [ Button.Color IsInfo ] [ str "Import" ]
        
        componentsTable
    ]