using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCD.Algorithms.Graphs.Digraph
{
    /// <summary>
    /// Strongly Connected Components Algorithm by Kosaraju and Sharir
    /// </summary>
    public class SCC_KosarajuSharir
    {
        private bool[] marked;
        private int[] id;
        private int count = 0;

        public SCC_KosarajuSharir(Digraph G)
        {
            marked = new bool[G.V];
            id = new int[G.V];
			TopologicalSort dfs = new TopologicalSort(G.Reverse());
            foreach (int v in dfs.GetTopologicalySorted())
            {
                if (!marked[v])
                {
                    DFS(G, v);
                    count++;
                }
            }
        }

        private void DFS(Digraph G, int v)
        {
            marked[v] = true;
            id[v] = count;
            foreach (var w in G.Adj(v))
                if (!marked[w]) 
                    DFS(G, w);
        }

        public int Count() { return count; }
        public int ID(int v) { return id[v]; }

        public bool StronglyConnected(int v, int w)
        {
            return (id[v] == id[w]);
        }
    }
}
