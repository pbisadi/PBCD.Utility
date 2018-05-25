using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using PBCD.Algorithms;
using System.Text;
using PBCD.Algorithms.DataStructure;

namespace PBCD.Algorithms.DataStructure_UnitTest
{
    [TestClass]
    public sealed class QuickUnion_UnitTest
    {
        static string _baseAddress;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path = Directory.GetParent(path).ToString();
            path = Directory.GetParent(path).ToString();
            _baseAddress = Path.Combine(path, "Tests\\UnionFind");
        }

		[TestMethod]
		public void QU_Basic()
		{
			string path = Path.Combine(_baseAddress, "BasicAPI_Test_input.txt");
			var connect = new int[5,2] {
				{ 0, 1 },
				{ 1, 3 },
				{ 5, 4 },
				{ 5, 4 },	//to test repeated union call
				{ 3, 5 }
			};

			int N = 6;
			var QU = new QuickUnion(N);

			for (int i = 0; i <= connect.GetUpperBound(0); i++)
			{
				int p = connect[i,0];
				int q = connect[i,1];
				QU.Union(p, q);
			}			
			
			//0 and 4 are expected to be connected to the same components
			Assert.AreEqual(QU.Find(0), QU.Find(4));

			Assert.AreEqual(QU.ComponentsCount, 2);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void QU_Constructor_NotZeroSize_Exp()
		{
			var QU = new QuickUnion(0);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void QU_Constructor_NotNegativeSize_Exp()
		{
			var QU = new QuickUnion(0);
		}
	}
}
