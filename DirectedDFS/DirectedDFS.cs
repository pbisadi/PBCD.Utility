using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCD.Algorithms.Graphs.Digraph
{
    public class DirectedDFS
    {
        private bool[] marked;
        private int[] edgeTo;
        private int s;
        public DirectedDFS(Digraph G, int s)
        {
            marked = new bool[G.V];
            edgeTo = new int[G.V];
            this.s = s;
            DFS(G, s);
        }

        private void DFS(Digraph G, int v)
        {
            marked[v] = true;
            foreach (var w in G.Adj(v))
            {
                if (!marked[w])
                {
                    DFS(G, w);
                    edgeTo[w] = v;
                }
            }
        }

        public bool HasPathTo(int v)
        {
            return marked[v];
        }

        public IEnumerable<int> PathTo(int v)
        {
            if (!HasPathTo(v)) return null;
            Stack<int> path = new Stack<int>();
            for (int x = v; x != s; x = edgeTo[x])
            {
                path.Push(x);
            }
            path.Push(s);
            return path;
        }
    }
}
