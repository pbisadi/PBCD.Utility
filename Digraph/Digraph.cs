using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCD.Algorithms.Graphs.Digraph
{
    /// <summary>
    /// Basic API of a graph.
    /// Vertices are represented by integers starting from 0
    /// The graph is represented by:
    /// NOT set of edges (process inefficient)
    /// NOT Adjacency-matrix (space inefficient)
    /// -> Adjacency-list: an array of bags of adjacent vertices for each vertex
    /// </summary>
    public class Digraph
    {
        private readonly int _V;
        private HashSet<int>[] adj;
        /// <summary>
        /// Creates an empty graph with V vertices
        /// </summary>
        /// <param name="v">Vertices count</param>
        public Digraph(int v)
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
        public Digraph(StreamReader input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add an edge v-w
        /// </summary>
        public void AddEdge(int v, int w)
        {
            adj[v].Add(w);
        }

        /// <summary>
        /// Vertices adjacent to v
        /// </summary>
        /// <param name="v">Vertex index</param>
        public IEnumerable<int> Adj(int v)
        {
            return adj[v];
        }

        /// <summary>
        /// Number of vertices
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

        public Digraph Reverse()
        {
            var rev = new Digraph(this._V);
            for (int v = 0; v < _V; v++)
            {
                foreach (int w in adj[v])
                {
                    rev.AddEdge(w, v);
                }
            }
            return rev;
        }
    }
}
