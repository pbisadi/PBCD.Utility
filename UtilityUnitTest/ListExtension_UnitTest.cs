using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PBCD.Extensions.List;
using System.Linq;

namespace PBCD.Extensions_UnitTest
{
    [TestClass]
    public class ListExtension_UnitTest
	{
		List<int> list;

		[TestInitialize]
		public void Initialization()
		{
			list = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		}

		[TestMethod]
		public void ListExtension_Basic() => Assert.AreEqual(9, list.R(2, 5).Sum()); // 2, 3, 4

		[TestMethod]
		public void ListExtension_NegativeEnd() => Assert.AreEqual(21, list.R(6, -1).Sum()); // 6, 7, 8

		[TestMethod]
		public void ListExtension_NegativeStart() => Assert.AreEqual(3, list.R(-9, 3).Sum()); // 1, 2

		[TestMethod]
		public void ListExtension_NegativeBoundary() => Assert.AreEqual(8, list.R(-2, -1)[0]); // 8

		[TestMethod]
		public void ListExtension_String()
		{
			var s = "Hello world!";
			Assert.AreEqual("wor", s.R(6, -3));
		}
	}
}
