module SlaCalculator.VisNetwork

open Fable.Core.JsInterop

type Node = 
    abstract id : int with get, set
    abstract label : string with get, set
    abstract image : string with get, set
    abstract shape : string with get, set

let networkNode = createEmpty<Node>

networkNode.id    <- 1
networkNode.label <- "Main"
networkNode.image <- "Network-Pipe-icon.png"
networkNode.shape <- "image"

#nowarn "1182"
let createDiagram (nodes : Node []) (edges : Node []) = import "createDiagram" "./VisNetwork.js"
#endnowarn