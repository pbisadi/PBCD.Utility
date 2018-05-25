using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PBCD.Algorithms.Sorts
{
    public class MergeSortBU
    {
        static IComparable[] aux = null;
        public static void Sort(IComparable[] a)
        {
            int N = a.Length;
            aux = new IComparable[a.Length];
            for (int sz = 1; sz < N; sz = sz + sz)
            {
                for (int lo = 0; lo < N - sz; lo += sz + sz)
                {
                    Merge(a, lo, lo + sz - 1, Math.Min(lo + sz + sz - 1, N - 1));
                }
            }
        }

        /// <summary>
        /// Uses an auxiliary array to merge parts of a[] 
        /// </summary>
        private static void Merge(IComparable[] a, int lo, int mid, int hi)
        {
            /// each part must be sorted at the beginning
            Debug.Assert(isSorted(a, lo, mid));
            Debug.Assert(isSorted(a, mid + 1, hi));


            for (int k = lo; k <= hi; k++) aux[k] = a[k];

            int i = lo;
            int j = mid + 1;
            for (int k = lo; k <= hi; k++)
            {
                if (i > mid) a[k] = aux[j++];
                else if (j > hi) a[k] = aux[i++];
                else if (aux[j].CompareTo(aux[i]) == -1) a[k] = aux[j++];
                else a[k] = aux[i++];
            }

            Debug.Assert(isSorted(a, lo, hi));
        }

        private static bool isSorted(IComparable[] a, int lo, int hi)
        {
            Debug.Assert(hi < a.Length && lo >= 0 && lo <= hi);
            for (int i = lo + 1; i <= hi; i++)
            {
                if (a[i].CompareTo(a[i - 1]) < 0) return false;
            }
            return true;
        }
    }
}
