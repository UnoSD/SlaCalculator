type Component = {
    Dependencies : Dependency list
    SLA : decimal
}
and Dependency =
    | Direct of Component
    | Distributed of Component list

let rec calculateCompositeSla entryComponent =
    let downProbabilityPercentage components =
        let folder acc c =
            let componentSla = calculateCompositeSla c // 99.99, 99.9
        
            (1m - (componentSla / 100m)) * acc
    
        let downProbability =
            List.fold folder 1m components
        
        downProbability * 100m

    let dependencyDownProbability =
        function
        | Direct c       -> 100m - calculateCompositeSla c
        | Distributed cs -> downProbabilityPercentage cs

    match entryComponent.Dependencies with
    | []         -> entryComponent.SLA
    | components -> entryComponent.SLA - (components |> List.sumBy dependencyDownProbability)
