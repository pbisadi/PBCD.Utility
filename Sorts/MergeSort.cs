using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;

namespace PBCD.Algorithms.Sorts
{
    /// <summary>
    /// Merge sort is a stable sort as it does not move equal items pass each other.
    /// If a sort is stable it keeps the order of equal items. 
    /// In the case that items are already sorted based on another attribute, subsets will stay sorted.
    /// Insertion sort is stable too. That's why it is used in merge sort for sorting small sub arrays.
    /// </summary>
    public class MergeSort
    {
        static object[] aux = null;
        static IComparer cmpr = null;

        public static void Sort(object[] a, IComparer comparer)
        {
            Debug.Assert(comparer != null);
            cmpr = comparer;
            aux = new object[a.Length];
            a.CopyTo(aux, 0);
            Sort(a, aux, 0, a.Length - 1);
        }

        private static void Sort(object[] a, object[] aux, int lo, int hi)
        {
            if (hi <= lo) return;
            int mid = lo + (hi - lo) / 2;
            Sort(aux, a, lo, mid);          //switch roles of aux[] and a[]
            Sort(aux, a, mid + 1, hi);      //switch roles of aux[] and a[]
            //At this point both sides of aux[] must be sorted

            ///* one optimization method is checking if a[] is already sorted (if a[mid]<= a[mid+1]) 
            ///  and avoid calling Merge() but as in (this) copying optimized version, Merge() copies one array
            ///  into the other one, it is always required to call Merge() even if a[]/aux[] is sorted

            Merge(aux, a, lo, mid, hi); //Merge from aux[] to a[]
        }

        /// <summary>
        /// Merges from a[] to b[]
        /// </summary>
        private static void Merge(object[] a, object[] b, int lo, int mid, int hi)
        {
            /// each half must be sorted at the beginning
            Debug.Assert(isSorted(a, lo, mid));
            Debug.Assert(isSorted(a, mid + 1, hi));

            int i = lo;
            int j = mid + 1;
            for (int k = lo; k <= hi; k++)
            {
                if (i > mid) b[k] = a[j++];
                else if (j > hi) b[k] = a[i++];
                else if (Less(a[j], a[i])) b[k] = a[j++];
                else b[k] = a[i++];                         //If equal pick from left sub array. Otherwise the sort will not be stable. 
            }

            ///b = merge of a[] halves
            ///so be must be sorted at the end
            Debug.Assert(isSorted(b, lo, hi));
        }

        private static bool Less(object a, object b)
        {
            return cmpr.Compare(a, b) < 0;
        }

        private static bool isSorted(object[] a, int lo, int hi)
        {
            Debug.Assert(hi < a.Length && lo >= 0 && lo <= hi);
            for (int i = lo + 1; i <= hi; i++)
            {
                if (Less(a[i], a[i - 1])) return false;
            }
            return true;
        }
    }
}
