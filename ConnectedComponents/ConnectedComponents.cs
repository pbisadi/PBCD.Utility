using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCD.Algorithms.Graphs
{
    public class ConnectedComponents
    {
        private bool[] marked;
        private int[] id;
        private int count = 0;

        public ConnectedComponents(Graph G)
        {
            marked = new bool[G.V];
            id = new int[G.V];
            for (int v = 0; v < G.V; v++)
            {
                if (!marked[v])
                {
                    DFS(G, v);
                    count++;
                }
            }

        }

        private void DFS(Graph G, int v)
        {
            marked[v] = true;
            id[v] = count;
            foreach (var w in G.Adj(v))
                if (!marked[w]) 
                    DFS(G, w);
        }

        public int Count() { return count; }
        public int ID(int v) { return id[v]; }

        public bool Connected(int v, int w)
        {
            return (id[v] == id[w]);
        }
    }
}
