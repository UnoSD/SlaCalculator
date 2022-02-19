module SlaCalculator.Calculator

open Feliz
open Fulma
open System
open Fable.React
open Fable.Import
open Fable.FontAwesome
open Fable.React.Props
open SlaCalculator.Helpers
open SlaCalculator.Models
open SlaCalculator.Messages
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
                     | Distributed ds -> ds |> List.map (fun ds -> ds.Name) |> String.concat ", " |> sprintf "{%s}"
                     | Direct      dd -> sprintf "%s" dd.Name)
        |> String.concat ", "
    
    let isEntrypoint =
        match entrypoint with
        | Some ep -> comp = ep
        | None    -> false
    
    let button color icon disabled onClick =
        Button.button [
            Button.Color color
            Button.Disabled disabled
            Button.OnClick (fun _ -> onClick |> dispatch)
        ] [ Fa.i [ icon ] [] ]
    
    [
        Html.tableCell [ Html.strong [ Html.text comp.Name ] ]
        Html.tableCell [ Html.strong [ Html.text sla ] ]
        Html.tableCell [ Html.text dependsOn ]
        Html.tableCell [ Html.text (isEntrypoint.ToString()) ]
        Html.tableCell [ Button.list [ Button.List.Option.HasAddons ] [
            button IsSuccess Icon.Edit false (comp |> EditComponent)
            button IsDanger Icon.Ban false (comp |> DeleteComponent)
            button IsInfo Icon.ArrowUp isEntrypoint (comp |> SetEntryPoint)
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

type private DependencyTypeSelector =
    | DirectDependency
    | DistributedDependency

let private dependenciesBox model dispatch dependencyType dependencyTypeSelector =
    let createOption (comp : Component) =
        option [
            // To prevent flickering when selecting dependencies, we can try and keep the original order
            OnClick (fun _ -> dependencyType comp |> ToggleDependency |> dispatch)
        ] [ str comp.Name ]
    
    let dependenciesBySelector dep =
        match dependencyTypeSelector, dep with
        | DirectDependency, Direct x            -> [ x.Name ]
        | DistributedDependency, Distributed xs -> xs |> List.map (fun x -> x.Name)
        | _                                     -> []
    
    let boxTitle = 
        match dependencyTypeSelector with
        | DirectDependency      -> "Direct dependencies"
        | DistributedDependency -> "Distributed dependencies"
    
    Select.select [
        Select.IsMultiple
        Select.IsFullWidth
    ] [ str boxTitle
        select [ Multiple true
                 ValueMultiple (model.Dependencies |> List.collect dependenciesBySelector |> Array.ofList)
                 ReadOnly true
                 Size 4. ] [
        yield! model.Components |> List.map createOption
    ] ]

let private button dispatch color name event =
    Button.button [
        Button.Color color
        Button.OnClick (fun _ -> dispatch event)
    ] [ str name ]
    
let private fileButton dispatch color name =
    Button.button [
        Button.Color color
    ] [
        File.input [
            Props [
                OnInput (fun ev ->
                    let target = ev.target :?> Browser.Types.HTMLInputElement
                    
                    match target.files.length with
                    | 1 -> let reader = Browser.Dom.FileReader.Create()

                           reader.onload <- (fun evt ->
                               let target = evt.target :?> Browser.Types.FileReader
                               target.result |> string |> CompletedImport |> dispatch)

                           reader.onerror <- (fun _ -> dispatch FailedImport)

                           reader.readAsText(target.files.[0])
                    | _ -> ()
                )
            ]
        ]
        str name
    ]

let private textField dispatch value isValid label placeholder icon event =
    Input.text [
        if isValid then () else Input.Color Color.IsDanger
        Input.OnChange (fun ev -> ev.Value |> event |> dispatch)
        Input.Value value
        Input.Placeholder placeholder
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
    let directDependenciesBox = dependenciesBox model dispatch Direct DirectDependency
    // We need the ability to add multiple distributed dependencies box per component
    // Imagine the Web App relies on two SB queues primary/secondary and on two Redis caches primary/secondary
    // And does in-code retries with fallback
    let distributedDependenciesBox = dependenciesBox model dispatch (List.singleton >> Distributed) DistributedDependency
    let textField = textField dispatch
    let isSlaValid = (Decimal.TryParse model.SLA |> fst)
    let totalsBox = Box.box' [] [ totals model ]
    let container (content : seq<ReactElement>) = card [ Html.div content ]
    
    let entryPointSelector =
        checkBox dispatch model.IsEntryPoint model.EntryPoint.IsSome "Entrypoint" ToggleIsEntryPoint
    
    let editUpdateButton =
        match model.EditingComponent with
        | None   -> "Add"   , ClickAdd
        | Some c -> "Update", ClickUpdate c
        ||> button IsPrimary
    
    container [
        totalsBox
        
        textField model.Name true "Name" "Ex: Azure Front Door" Icon.Atom ChangeName
        
        textField model.SLA isSlaValid "SLA" "Ex: 99.99" Icon.Ambulance ChangeSLA
        
        entryPointSelector
        
        directDependenciesBox
        
        distributedDependenciesBox
        
        editUpdateButton
        
        button IsSuccess "Export" Export
        
        fileButton dispatch IsInfo "Import"
        
        button IsLink "Load example" LoadExample
        
        model.EditingComponent
        |> Option.map (fun _ -> button IsWarning "Cancel edit" CancelEdit)
        |> Option.defaultValue Html.none
        
        button IsDanger "Reset" Reset
        
        componentsTable
        
        Html.div [ prop.id "mynetwork"; prop.style [ Feliz.style.width(length.auto); Feliz.style.height(600) ] ]
    ]