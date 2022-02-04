module.exports = {
    createDiagram: function (nodes, edges) {
        let container = document.getElementById("mynetwork");
        
        let data = {
            nodes: nodes,
            edges: edges,
        };
        
        let options = {};

        new vis.Network(container, data, options);
    }
}