using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCD.Algorithms.Sorts
{
    public class ShellSort
    {
        public static void Sort<T>(T[] a, IComparer<T> c){
            int N = a.Length;

            int h = 1;
            while (h < N / 3)
            {
                h = 3 * h + 1;
            }

            while (h >= 1)
            {
                for (int i = h; i < N; i++)
                {
                    for (int j = i; j >= h && c.Compare(a[j],a[j-h]) == -1; j -= h)
                    {
                        var swap = a[i];
                        a[i] = a[j];
                        a[j] = swap;
                    }
                }
                
                h /= 3;
            }
        }

        public static void Sort(IComparable[] a)
        {
            int N = a.Length;
            
            int h = 1;
            while (h < N / 3)
            {
                h = 3 * h + 1;
            }

            while (h >= 1)
            {
                for (int i = h; i < N; i++)
                {
                    for (int j = i; j >= h && Less(a[j], a[j - h]); j -= h)
                    {
                        Exch(a, j, j - h);
                    }
                }

                h = h / 3;
            }


        }

        private static bool Less(IComparable a, IComparable b)
        { return a.CompareTo(b) < 0; }

        private static void Exch(IComparable[] a, int i, int j)
        {
            IComparable swap = a[i];
            a[i] = a[j];
            a[j] = swap;
        }

    }
}
