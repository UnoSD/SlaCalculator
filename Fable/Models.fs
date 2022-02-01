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
    Dependencies : Dependency list
    EditingComponent : Component option
}
    
let emptyModel = {
    // Status
    CurrentTab = Calculator
    Components = []
    EntryPoint = None
    // Editor
    Name = ""
    SLA = "0.0"
    IsEntryPoint = false
    Dependencies = []
    EditingComponent = None
}

// Example model

let private sqlDatabase = 
    {
        Name = "Azure SQL"
        Dependencies = []
        SLA = 99.995m
    }
    
let private neAppService = 
    {
        Name = "Azure App Service (North Europe)"
        Dependencies = [
            Direct sqlDatabase
        ]
        SLA = 99.95m
    }
    
let weAppService = 
    {
        Name = "Azure App Service (West Europe)"
        Dependencies = [
            Direct sqlDatabase
        ]
        SLA = 99.95m
    }
    
let frontDoor = 
    {
        Name = "Azure Front Door"
        Dependencies = [
            Distributed [
                neAppService
                weAppService
            ]
        ]
        SLA = 99.99m
    }
    
let exampleModel = {
    // Status
    CurrentTab = Calculator
    Components = [
        sqlDatabase
        neAppService
        weAppService
        frontDoor
    ]
    EntryPoint = Some frontDoor
    // Editor
    Name = ""
    SLA = "0.0"
    IsEntryPoint = false
    Dependencies = []
    EditingComponent = None
}