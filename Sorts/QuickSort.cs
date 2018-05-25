using PBCD.Algorithms.Randomization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCD.Algorithms.Sorts
{
    public class QuickSort
    {
        static IComparer cmpr = null;
        public static void Sort(object[] a, IComparer comparer)
        {
            cmpr = comparer;
            Shuffler.Shuffle<object>(a);
            Sort(a, 0, a.Length - 1);
        }

        private static void Sort(object[] a, int lo, int hi)
        {
            if (hi <= lo) return;
            //Instead of normal partition method by Hoare http://en.wikipedia.org/wiki/Tony_Hoare
            //Three way partitioning by Dijkstra is used http://fpl.cs.depaul.edu/jriely/ds1/extras/demos/23DemoPartitioningDijkstra.pdf
            int lt = lo, gt = hi;
            var v = a[lo];
            int i = lo;
            while (i <= gt)
            {
                int cmpResult = cmpr.Compare(a[i], v);
                if (cmpResult < 0) Exch(a, lt++, i++);
                else if (cmpResult > 0) Exch(a, i, gt--);
                else i++;
            }

            Sort(a, lo, lt-1);
            Sort(a, gt + 1, hi);
        }

        /// <summary>
        /// Selects the (k+1)th smallest object of a in (average) linear time
        /// </summary>
        /// <param name="a"></param>
        /// <param name="k"></param>
        public static object Select(object[] a, int k, IComparer comparer)
        {
            cmpr = comparer;
            Shuffler.Shuffle<object>(a);
            int lo = 0;
            int hi = a.Length - 1;
            while (hi > lo)
            {
                int j = Partition(a, lo, hi);
                if (j < k) lo = j + 1;
                else if (j > k) hi = j - 1;
                else return a[k];
            }
            return a[k];
        }

        private static int Partition(object[] a, int lo, int hi)
        {
            int i = lo;
            int j = hi+1;

            while (true)
            {
                while (Less(a[++i], a[lo]))     //find item on left to swap 
                    if (i == hi) break;

                while (Less(a[lo], a[--j]))     //find item on right to swap
                    if (j == lo) break;

                if (i >= j) break;              //check if pointers cross
                Exch(a, i, j);                  //swap    
            }

            Exch(a, lo, j);
            return j;
        }

        private static void Exch(object[] a, int i, int j)
        {
            var temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }

        private static bool Less(object a, object b)
        {
            return cmpr.Compare(a, b) < 0;
        }
    }
}
