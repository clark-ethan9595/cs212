using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bingo
{
    /// <summary>
    /// Represents a labeled, directed edge in a RelationshipGraph
    /// </summary>
    class GraphEdge
    {
        private string label;
        private GraphNode fromNode, toNode;

        // constructor
        public GraphEdge(GraphNode from, GraphNode to, string myLabel)
        {
            fromNode = from;
            toNode = to;
            label = myLabel;
        }

        // return label of edge
        public string Label()
        {
            return label;
        }

        // return the name of the "to" person in the relationship
        public string To()
        {
            return toNode.Name();
        }

        // return string form of edge
        public override string ToString() 
        {
            string result = fromNode.Name() + " --(" + label + ")--> " + toNode.Name();
            return result;
        }
    }
}

