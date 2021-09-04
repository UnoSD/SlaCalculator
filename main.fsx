open System

type Component = {
    Dependencies : Dependency list
    SLA : decimal
}
and Dependency =
    | Direct of Component
    | Distributed of Component list

let sqlDatabase = {
        SLA = 99.95m
        Dependencies = []
    }

let appService = {
    SLA = 99.95m
    Dependencies = [ Direct sqlDatabase ]
}

let rec calculateCompountSla entryComponent =
    let dependencyDownProbability =
        function
        | Direct c       -> 100m - calculateCompountSla c
        | Distributed cs -> failwith "Not implemented"

    match entryComponent.Dependencies with
    | []         -> entryComponent.SLA
    | components -> 100m - (Direct { entryComponent with Dependencies = [] } :: components |> List.sumBy dependencyDownProbability)
    
    
calculateCompountSla appService
|> printfn "%A"