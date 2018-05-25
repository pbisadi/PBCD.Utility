using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCD.Algorithms.Sorts
{
    public class HeapSort
    {

        public static void Sort(IComparable[] pq)
        {
            int N = pq.Length;
            for (int k = N/2; k >=1; k--)
            {
                Sink(pq, k, N);
            }

            while (N>1)
            {
                Exch(pq, 1, N);
                Sink(pq, 1, --N);
            }
        }

        /// <summary>
        /// Sinks down the node at the index to its proper position 
        /// </summary>
        /// <param name="k"></param>
        private static void Sink(IComparable[] pq, int k, int N)
        {
            while (2 * k <= N)
            {
                int j = 2 * k;
                if (j < N && Less(pq, j, j + 1)) j++;   //The other child is larger
                if (!Less(pq, k, j)) break; //it sinked enough
                Exch(pq, k, j);
                k = j;
            }
        }

        private static bool Less(IComparable[] pq, int i, int j)
        {
            return pq[i-1].CompareTo(pq[j-1]) < 0;
        }

        private static void Exch(IComparable[] pq, int i, int j)
        {
            i--; j--;
            var temp = pq[i];
            pq[i] = pq[j];
            pq[j] = temp;
        }
    }
}
