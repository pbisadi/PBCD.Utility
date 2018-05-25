using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PBCD.Algorithms.DataStructure;

namespace PBCD.Algorithms.DataStructure_UnitTest
{
	[TestClass]
	public class MinPQ_UnitTest
	{
		[TestMethod]
		public void MinPQ_Basic()
		{
			var pq = new MinPriorityQueue<int>();
			pq.Insert(2, null);
			pq.Insert(6, null);
			pq.Insert(1, null);
			Assert.AreEqual(1, (int)(pq.DelTop().Item1));
			pq.Insert(9, null);
			pq.Insert(7, null);
			pq.Insert(1, null);
			Assert.AreEqual(1, (int)(pq.DelTop().Item1));
			pq.Insert(2, null);
			pq.Insert(6, null);
			pq.Insert(1, null);
			Assert.AreEqual(1, (int)(pq.DelTop().Item1));
			Assert.AreEqual(2, (int)(pq.Top().Item1));
			Assert.AreEqual(2, (int)(pq.DelTop().Item1));
			Assert.AreEqual(2, (int)(pq.DelTop().Item1));
			Assert.AreEqual(6, (int)(pq.DelTop().Item1));
		}
	}
}
