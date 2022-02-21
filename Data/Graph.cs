using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace RpgNotes.Desktop.Data {

/// <summary>
/// Generic graph implementation
/// </summary>
/// <typeparam name="V">Vertex data type</typeparam>
/// <typeparam name="E">Edge data type</typeparam>
public class Graph<V,E> {
    public class Node {
        public int Id;
        public string Name;
        public V Data;
        public List<Edge> Outbound;
    }

    public class Edge {
        public E Data;
        public Node StartsAt;
        public Node GoesTo;
    }

    private List<Node> nodes = new List<Node>();
    public IEnumerable<Node> Nodes => nodes.AsReadOnly();
    public IEnumerable<Edge> Edges => nodes.SelectMany(node => node.Outbound ?? Enumerable.Empty<Edge>());

    public Graph() {}

    public Node NewNode(string name) {
        var node = new Node {
            Id = this.nodes.Count,
            Name = name,
            Outbound = new List<Edge>(),
        };
        this.nodes.Add(node);
        return node;
    }
    public Node NewNode(string name, V data) {
        var node = NewNode(name);
        node.Data = data;
        return node;
    }
    public void AddEdge(Node from, Node to, E edge) {
        if (from.Outbound == null) {
            from.Outbound = new List<Edge>();
        }
        from.Outbound.Add(
            new Edge {
                Data = edge,
                StartsAt = from,
                GoesTo = to,
            }
        );
    }
    public void RemoveEdge(Node from, Node to) {
        if (from.Outbound != null) {
            from.Outbound.RemoveAll((edge) => edge.GoesTo == to);
        }
    }
    public bool EdgeExists(Node from, Node to) {
        return from.Outbound != null && from.Outbound.Where(edge => edge.GoesTo == to).Any();
    }
}

/// <summary>
/// Base class for vertices that get displayed on a screen
/// </summary>
public class DisplayedVertex {
    public Vector2 Position {get; set;}
}

/// <summary>
/// Base class for graph layouts
/// </summary>
public abstract class GraphLayoutAlgorithm {
    public abstract void Arrange<V,E>(Graph<V,E> graph) where V:DisplayedVertex;
}

/// <summary>
/// Simple spring layout for graphs based on https://cs.brown.edu/people/rtamassi/gdhandbook/chapters/force-directed.pdf chapter 12.2.
/// </summary>
public class SpringLayout : GraphLayoutAlgorithm {
    public int M {get; set;} = 100; 
    public float C1 {get; set;} = 20f;
    public float C2 {get; set;} = 10f; // Where the graph switches to negatives "repulsion" when too close
    public float C3 {get; set;} = 10f;
    public float C4 {get; set;} = 1.2f;

    private static Random rng = new Random();

    private static float randomBetween(float one, float two) {
        var t = rng.NextSingle();
        return (1 - t) * one + t * two;
    }
    
    public override void Arrange<V,E>(Graph<V,E> graph) {
        /*
        algorithm SPRING(G:graph);
            place vertices of G in random locations;
            repeat M times
                calculate the force on each vertex;
                move the vertex c4 ∗ (force on vertex)
            draw graph on CRT or plotter.
        */
        var nodes = graph.Nodes.ToList();
        var forces = new Vector2[nodes.Count];

        // Place vertices of G in random locations
        foreach (var vertex in nodes) {
            vertex.Data.Position = new Vector2(
                randomBetween(-10, 10),
                randomBetween(-10, 10)
            );
        }

        // Repeat M times
        for (var i = 0; i < M; i++) {
            // Calculate the force on each vertex;
            for(var v = 0; v < nodes.Count; v++) {
                var vertex = nodes[v];
                var f = Vector2.Zero;

                // Spring "pull" force between adjacent vertices
                foreach (var edge in vertex.Outbound) {
                    var dir = edge.GoesTo.Data.Position - vertex.Data.Position;
                    var d = dir.Length();
                    f += (C1 * MathF.Log(d/C2)) * (dir / d);
                }
                // Secondly, we make nonadjacent vertices repel each other.
                foreach (var node in nodes) {
                    if (node == vertex || graph.EdgeExists(vertex, node) || graph.EdgeExists(node, vertex))
                        continue;
                    var dir = node.Data.Position - vertex.Data.Position;
                    var d = dir.Length();
                    f += (C3/(d * d)) * (-dir/d);
                }

                forces[v] = f;
            }

            // Move the vertex c4 ∗ (force on vertex)
            for (var v = 0; v < nodes.Count; v++) {
                var vertex = nodes[v];
                vertex.Data.Position += C4 * forces[v];
            }
        }
    }
}

}