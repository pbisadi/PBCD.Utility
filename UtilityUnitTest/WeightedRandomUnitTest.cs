using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PBCD.Algorithms.Randomization;
using System.Linq;

namespace PBCD.Algorithms.Randomization_UnitTest
{
	[TestClass]
	public class WeightedRandomUnitTest
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException), "The size of the selection cannot be more that the size of the list")]
		public void WeightedSelect_OverSelecting()
		{
			var items = new List<int>() { 1, 1, 1};
			items.WeightedChoice(4, i => i);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException), "The size of the selection cannot be less than 1")]
		public void WeightedSelect_UnderSelecting()
		{
			var items = new List<int>() { 1, 1, 1 };
			items.WeightedChoice(0, i => i);
		}

		[TestMethod]
		public void WeightedSelect_SmokeTest()
		{
			var items = new List<int>() { 1, 2, 3, 4, 5 };
			items.WeightedChoice(3, i => i);
		}

		[TestMethod]
		public void WeightedSelect_SmokeTest2()
		{
			var items = new List<int>();
			for (int i = 0; i < 1000000; i++) items.Add(1);
			for (int i = 0; i < 1000000; i++) items.Add(2);
			var result = items.WeightedChoice(10000, i => i);
			int count1 = result.Count(item => item == 1);
			int count2 = result.Count(item => item == 2);
			//TODO: It was expected that the count1/count2 be very close to 0.5
			// which is not the case in few runs (0.56). So, the make sure, implement
			// round robin algorithm and compare the results.
		}

	}
}
