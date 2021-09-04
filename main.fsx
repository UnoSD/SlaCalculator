// https://devops.stackexchange.com/questions/711/how-do-you-calculate-the-compound-service-level-agreement-sla-for-cloud-servic
// https://docs.microsoft.com/en-us/azure/architecture/framework/resiliency/business-metrics
// https://docs.microsoft.com/en-us/answers/questions/122986/composite-slas.html

// Does it exist already? https://github.com/mspnp/samples/tree/master/Reliability/SLAEstimator

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