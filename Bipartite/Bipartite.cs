using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCD.Algorithms.Graphs
{
    public static class BipartiteGraph
    {
        static byte[] marked;
        /// 0 = Unmarked
        /// 1 = Group 1
        /// 2 = Group 2
        private const byte FIRST = 1;
        private const byte SECOND = 2;

        /// <summary>
        /// Departs the graph into two groups if possible and returns true. Otherwise returns false
        /// </summary>
        public static bool Depart(Graph G, out IEnumerable<int> group1, out IEnumerable<int> group2)
        {
            marked = new byte[G.V];
            List<int> result1 = new List<int>();
            List<int> result2 = new List<int>();
            bool isBipartite = DFS(G, 0 , ref result1, ref result2, FIRST);
            group1 = result1;
            group2 = result2;
            return isBipartite;
        }

        private static bool DFS( Graph G,int v,  ref List<int> G1, ref List<int> G2, byte mark)
        {
            marked[v] = mark;
            G1.Add(v);
            bool result = true;
            foreach (var w in G.Adj(v))
            {
                if (marked[w] != 0 && marked[w] == mark) return false;
                if (marked[w] == 0)
                {
                    result = result && DFS(G, w, ref G2, ref G1, mark == FIRST? SECOND: FIRST);
                }
            }
            return result;
        }
    }
}
