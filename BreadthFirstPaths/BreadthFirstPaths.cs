using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCD.Algorithms.Graphs
{
    public class BreadthFirstPaths
    {
        private bool[] marked;
        private int[] edgeTo;
        private HashSet<int> s;
        public BreadthFirstPaths(Graph G, IEnumerable<int> sources)
        {
            marked = new bool[G.V];
            edgeTo = new int[G.V];
            this.s = new HashSet<int>(sources);
            BFS(G, s);
        }

        public BreadthFirstPaths(Graph G, int source)
        {
            marked = new bool[G.V];
            edgeTo = new int[G.V];
            this.s = new HashSet<int>();
            s.Add(source);
            BFS(G, s);
        }


        private void BFS(Graph G, IEnumerable<int> sources)
        {
            Queue<int> q = new Queue<int>();
            foreach (int source in sources)
            {
                q.Enqueue(source);
            }     
       
            while (!(q.Count == 0))
            {
                int v = q.Dequeue();
                foreach (int w in G.Adj(v))
                {
                    if (!marked[w])
                    {
                        q.Enqueue(w);
                        marked[w] = true;
                        edgeTo[w] = v;
                    }
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
            int x;
            for (x = v; !s.Contains(x); x = edgeTo[x])
            {
                path.Push(x);
            }
            path.Push(x);
            return path;
        } 
    }
}
