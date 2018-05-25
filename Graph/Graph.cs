using System;
using System.Collections.Generic;
using System.IO;

namespace PBCD.Algorithms.Graphs
{
    /// <summary>
    /// Basic API of a graph.
    /// Vertexes are represented by integers starting from 0
    /// The graph is represented by:
    /// NOT set of edges (process inefficient)
    /// NOT Adjacency-matrix (space inefficient)
    /// -> Adjacency-list: an array of bags of adjacent vertexes for each vertex
    /// </summary>
    public class Graph
    {
        private readonly int _V;
        private HashSet<int>[] adj;

        /// <summary>
        /// Creates an empty graph with V vertexes
        /// </summary>
        /// <param name="v">vertexes count</param>
        public Graph(int v)
        {
            this._V = v;
            adj = new HashSet<int>[v];
            for (int i = 0; i < v; i++)
            {
                adj[i] = new HashSet<int>();
            }
        }

        /// <summary>
        /// Creates a graph from input
        /// </summary>
        public Graph(StreamReader input)
        {
            throw new NotImplementedException();
        }

        public int MyProperty { get; set; }

        /// <summary>
        /// Add an edge v-w
        /// </summary>
        public void AddEdge(int v, int w)
        {
            adj[v].Add(w);
            adj[w].Add(v);
        }

        /// <summary>
        /// vertexes adjacent to v
        /// </summary>
        /// <param name="v">Vertex index</param>
        public IEnumerable<int> Adj(int v)
        {
            return adj[v];
        }

        /// <summary>
        /// Number of vertexes
        /// </summary>
        public int V { get { return _V; } }

        /// <summary>
        /// Number of edges
        /// </summary>
        public int E
        {
            get
            {
                int count = 0;
                for (int i = 0; i < adj.Length; i++)
                {
                    count += adj[i].Count;
                }
                return count;
            }
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}