module SlaCalculator.Models

type Tab =
    | Calculator
    | About

type Component = {
    Name : string
    Dependencies : Dependency list
    SLA : decimal
}
and Dependency =
    | Direct of Component
    | Distributed of Component list

type Model = {
    // Status
    CurrentTab : Tab
    Components : Component list    
    EntryPoint : Component option
    // Editor
    Name : string
    SLA : string
    IsEntryPoint : bool
    Dependencies : Component list
    EditingComponent : Component option
}
    
let emptyModel = {
    // Status
    CurrentTab = Calculator
    Components = [
        {
            Name = "Web App"
            Dependencies = []
            SLA = 99.9m
        }
    ]
    EntryPoint = None
    // Editor
    Name = ""
    SLA = "0.0"
    IsEntryPoint = false
    Dependencies = []
    EditingComponent = None
}