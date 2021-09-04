open System

// A component can be a Load Balancer
// A component can be multiple A/A instances, in that case, redundancy is > 1
type Component = {
    Dependencies : Dependency list
    SLA : decimal // option if a component does not have an SLA, what do we do? that should mean that the whole solution does not have one, unless we can ignore the component, should we add a "canGoDown flag?"
//    Redundancy : int
//    RTO : TimeSpan option
//    RPO : TimeSpan option
}
and Dependency =
    | Simple of Component // AND dependency
    | Redundant of Component list // OR dependency

// Let's pretend we have two storage accounts for redundancy and Azure Function has retry mechanism to try the primary and go to secondary on failure
let blobStorage = {
    SLA = 99.99m
    Dependencies = []
//    Redundancy = 2
//    RTO = None
//    RPO = None
}

let formRecognizer = {
    SLA = 99.5m
    Dependencies = []
//    Redundancy = 1
//    RTO = None
//    RPO = None
}

let azureFunction = {
    SLA = 99.5m
    Dependencies = [ blobStorage; formRecognizer ]
//    Redundancy = 1
//    RTO = None
//    RPO = None
}

let azureAd = {
    SLA = 99.999m
    Dependencies = []
//    Redundancy = 1
//    RTO = None
//    RPO = None
}

// TODO: What happens when both Azure Function and APIM have a dependency on blob, make sure we do the math right
let apiManagement = {
    SLA = 99.9m
    Dependencies = [ azureFunction; azureAd; (*blobStorage*) ]
//    Redundancy = 1
//    RTO = None
//    RPO = None
}

// Should we add components that do not affect the availability? App Insight where the app does not fail if it cannot log?

// What if the redundancy is not just a duplicate service? What if we write to a db, but if it's unavailable, we save data to a queue? queue and db will have different SLA...

let appSvc = {
    SLA = 99.95m
    Dependencies = [
        {
            SLA = 99.95m
            Dependencies = []
//            Redundancy = 1
//            RTO = None
//            RPO = None
        }
    ]
//    Redundancy = 1
//    RTO = None
//    RPO = None
}

let downProbability' sla =
    100.0m - sla

let downProbability component' =
    downProbability' component'.SLA

let rec calculateCompountSla entryComponent =
    match entryComponent.Dependencies with
    | []         -> entryComponent.SLA // Now we need to consider also Redundancy
    | components -> 100.0m - List.sumBy (calculateCompountSla >> downProbability') ({ entryComponent with Dependencies = [] } :: components)
    
    //| components -> entryComponent.SLA :: (List.map (calculateCompountSla) components) |> List.fold ((*)) 1.0m
    
calculateCompountSla appSvc
|> printfn "%A"