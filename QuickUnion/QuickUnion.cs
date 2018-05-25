using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCD.Algorithms.DataStructure
{
	/// <summary>
	/// Components are maximal set of objects that are mutually connected 
	/// Used algorithm: weighted quick-union with path compression
	/// </summary>
	public class QuickUnion
	{
		private int[] id;
		private int[] sz;   //Number of items in the tree rooted at i
		private int _count;
		public QuickUnion(int N)
		{
			if (N <= 0) throw new ArgumentOutOfRangeException("N must be a non-zero positive number.");
			_count = N;
			id = new int[N];
			sz = new int[N];
			for (int i = 0; i < N; i++)
			{
				id[i] = i;
				sz[i] = 1;
			}
		}

		private int Root(int i)
		{
			while (i != id[i])
			{
				id[i] = id[id[i]];
				i = id[i];
			}
			return i;
		}

		/// <summary>
		/// Add connection between p and q
		/// </summary>
		public void Union(int p, int q)
		{
			if (Connected(p, q)) return;
			int i = Root(p);
			int j = Root(q);
			if (sz[i] < sz[j])
			{
				id[i] = j;
				sz[j] += sz[i];
			}
			else
			{
				id[j] = i;
				sz[i] += sz[j];
			}
			_count--;
		}

		/// <summary>
		/// Are p and q in the same component?
		/// </summary>
		public bool Connected(int p, int q)
		{
			return Root(p) == Root(q);
		}

		/// <summary>
		/// Component identified for p
		/// </summary>
		/// <returns>returns the id of the component that p is in it</returns>
		public int Find(int p)
		{
			return Root(p);
		}

		public int ComponentsCount { get { return _count; } }
	}
}
