module SlaCalculator.VisNetwork

open Fable.Core.JsInterop

type Node =
    {
        id    : string
        label : string
        image : string
        shape : string
    }

type Edge =
    {
        from   : string
        ``to`` : string
        length : int
    }

#nowarn "1182"
let updateDiagram (nodes : Node []) (edges : Edge []) = import "createDiagram" "./VisNetwork.js"
#endnowarn