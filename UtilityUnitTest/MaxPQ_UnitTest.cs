using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PBCD.Algorithms.DataStructure;

namespace PBCD.Algorithms.DataStructure_UnitTest
{
	[TestClass]
	public class MaxPQ_UnitTest
	{
		[TestMethod]
		public void MaxPQ_Basic()
		{
			var pq = new MaxPriorityQueue<int>();
			pq.Insert(2, null);
			pq.Insert(6, null);
			pq.Insert(1, null);
			Assert.AreEqual(6, (int)(pq.DelTop().Item1));
			pq.Insert(9, null);
			pq.Insert(7, null);
			pq.Insert(1, null);
			Assert.AreEqual(9, (int)(pq.DelTop().Item1));
			pq.Insert(2, null);
			pq.Insert(6, null);
			pq.Insert(1, null);
			Assert.AreEqual(7, (int)(pq.DelTop().Item1));
			Assert.AreEqual(6, (int)(pq.Top().Item1));
			Assert.AreEqual(6, (int)(pq.DelTop().Item1));
			Assert.AreEqual(2, (int)(pq.DelTop().Item1));
			Assert.AreEqual(2, (int)(pq.DelTop().Item1));
		}
	}
}
