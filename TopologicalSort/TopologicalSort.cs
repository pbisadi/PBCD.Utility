using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCD.Algorithms.Graphs.Digraph
{
    public class TopologicalSort
	{
        private bool[] marked;

        /// <summary>
        /// reverse post order of a graph is the topological sort of vertexes
        /// </summary>
        private Stack<int> reversePost;

        public TopologicalSort(Digraph G)
        {
            reversePost = new Stack<int>();
            marked = new bool[G.V];
            for (int v = 0; v < G.V; v++)
                if (!marked[v])
                    DFS(G, v);
        }

        private void DFS(Digraph G, int v)
        {
            marked[v] = true;
            foreach (var w in G.Adj(v))
            {
                if (!marked[w]) DFS(G,w);
            }
            reversePost.Push(v);
        }

        public IEnumerable<int> GetTopologicalySorted()
        {
            return reversePost;
        }

    }
}
