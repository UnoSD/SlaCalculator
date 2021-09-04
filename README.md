# SlaCalculator
Calculate composite SLA for solutions

# Example
```fsharp
let sqlDatabase = {
        SLA = 99.95m
        Dependencies = []
    }

let appService = {
    SLA = 99.95m
    Dependencies = [ Direct sqlDatabase ]
}
    
calculateCompositeSla appService
|> printfn "%A"
    
let queue = {
    SLA = 99.9m
    Dependencies = []
}

let sqlDatabase' = {
    SLA = 99.99m
    Dependencies = []
}

let webApp = {
    SLA = 99.95m
    Dependencies = [ Distributed [ sqlDatabase'; queue ] ]
}

calculateCompositeSla webApp
|> printfn "%A"

let trafficManager = {
    SLA = 99.99m
    Dependencies = [ Distributed [ appService; webApp ] ]
}

calculateCompositeSla trafficManager
|> printfn "%A"
```
```
99.90M
99.9499900M
99.9899499900M
```
