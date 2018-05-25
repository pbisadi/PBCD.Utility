using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCD.Algorithms.DataStructure
{
    //TODO: Use flexible array size like stack implementation
    public abstract class PriorityQueue<Key> where  Key : IComparable
    {
        protected Tuple<Key, object>[] pq = new Tuple<Key,object>[4]; //4 rather than starting from 1
        protected int N = 0;  //Size of the heap

        /// <summary>
        /// Create an empty priority queue
        /// </summary>
        public PriorityQueue() {}

        /// <summary>
        /// create a priority queue with given tuples
        /// </summary>
        /// <param name="a"></param>
        public PriorityQueue(Tuple<Key,object>[] a)
        {
            pq = a;
            N = a.Length;
        }

        public void Insert(Key k, Object value)
        {
            ExpandSize();
            pq[++N] = new Tuple<Key, object>(k, value);
            Swim(N);
        }

        /// <summary>
        /// Return and remove largest/smallest key
        /// </summary>
        /// <returns></returns>
        public Tuple<Key, object> DelTop()
        {
            if (this.IsEmpty())
                throw new InvalidOperationException("The priority queue is empty.");
            var max = pq[1];
            Exch(1, N--);
            Sink(1);
            pq[N + 1] = null;
            ShrinkSize();
            return max;
        }

        public bool IsEmpty()
        {
            return N == 0;
        }

        public Tuple<Key, object> Top()
        {
            return pq[1];
        }

        public int Size()
        {
            return N;
        }

        /// <summary>
        /// Swims up the node at index k to its proper position 
        /// </summary>
        /// <param name="k"></param>
        private void Swim(int k)
        {
            while (k > 1 && Less(k/2,k))
            {
                Exch(k, k / 2);
                k = k / 2;
            }
        }

        /// <summary>
        /// Sinks down the node at the index to its proper position 
        /// </summary>
        /// <param name="k"></param>
        private void Sink(int k)
        {
            while (2*k <= N)
            {
                int j = 2 * k;
                if (j < N && Less(j, j + 1)) j++;   //The other child is larger
                if (!Less(k, j)) break; //it sinked enough
                Exch(k, j);
                k = j;
            }
        }

        protected abstract bool Less(int k, int p);

        private void Exch(int k, int p)
        {
            var temp = pq[k];
            pq[k] = pq[p];
            pq[p] = temp;
        }

        /// <summary>
        /// Double the size of inner array when it is about to full
        /// </summary>
        private void ExpandSize()
        {
            if (pq.Length - 1 == N )
            {
                var temp = new Tuple<Key, object>[2*N];
                pq.CopyTo(temp, 0);
                pq = temp;
            }
        }

        /// <summary>
        /// Release memory when the size of required array shrinks 
        /// </summary>
        private void ShrinkSize()
        {
            if (pq.Length-1 < N / 4)
            {
                var temp = new Tuple<Key, object>[N/2];
                pq.CopyTo(temp, 0);
                pq = temp;
            }
        }

		public override string ToString()
		{
			var result = from i in pq where i != null select i.Item1.ToString();
			return string.Join(",", result);
		}
	}

    public class MaxPriorityQueue<Key> :PriorityQueue<Key> where Key : IComparable
    {
        protected override bool Less(int k, int p)
        {
            return pq[k].Item1.CompareTo(pq[p].Item1) < 0;
        }
    }

    public class MinPriorityQueue<Key> : PriorityQueue<Key> where Key : IComparable
    {
        protected override bool Less(int k, int p)
        {
            return pq[k].Item1.CompareTo(pq[p].Item1) > 0;
        }
    } 
}
