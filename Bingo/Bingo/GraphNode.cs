using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bingo
{
    /// <summary>
    /// Represents a node in a RelationshipGraph
    /// </summary>
    class GraphNode
    {
        private string name;
        private List<GraphEdge> incidentEdges;

        // constructor
        public GraphNode(string v)
        {
            name = v;
            incidentEdges = new List<GraphEdge>();
        }

	// Add an edge (but don't add duplicate edges)
        public void AddIncidentEdge(GraphEdge e)
        {
	    foreach (GraphEdge edge in incidentEdges)
	    {
	        if (edge.ToString() == e.ToString()) 
		    return;
	    }
            incidentEdges.Add(e);
        }

        // return a list of all outgoing edges
        public List<GraphEdge> GetEdges()
        {
            return incidentEdges;
        }

        // return a list of outgoing edges of specified label
        public List<GraphEdge> GetEdges(string label)
        {
            List<GraphEdge> list = new List<GraphEdge>();
            foreach (GraphEdge e in incidentEdges)
                if (e.Label() == label)
                    list.Add(e);
            return list;
        }

        // return text form of node, including outgoing edges
        public override string ToString()
        {
            string result = name + "\n";
            foreach (GraphEdge e in incidentEdges)
            {
                result = result + "  " + e.ToString() + "\n";
            }
            return result;
        }

        // return name of node
        public string Name()
        {
            return name;
        }
    }
}
