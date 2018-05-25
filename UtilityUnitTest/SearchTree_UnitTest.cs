using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PBCD.Algorithms.DataStructure;
using PBCD.Algorithms.SearchTree;

namespace PBCD.Algorithms.SearchTree_UnitTest
{
    [TestClass]
    public class SearchTree_UnitTest
    {
        [TestMethod]
        public void BST_Floor()
        {
            var BST = new BinarySearchTree<int, object>();
            BST[4] = 0;
            BST[6] = 0;
            BST[8] = 0;
            BST[10] = 0;
            BST[14] = 0;
            BST[12] = 0;
            BST[10] = 0;
            Assert.AreEqual(10, BST.Floor(11).Item1);
        }

        [TestMethod]
        public void BST_Size()
        {
            var BST = new BinarySearchTree<int, object>();
            BST[4] = 0; BST[3] = 0; BST[2] = 0; BST[1] = 0;
            BST[5] = 0; BST[6] = 0; BST[7] = 0;

            Assert.AreEqual(7, BST.Size());
        }

        [TestMethod]
        public void BST_MinMax()
        {
            var BST = new BinarySearchTree<int, object>();
            BST[4] = 0; BST[3] = 0; BST[2] = 0; BST[1] = 0;
            BST[5] = 0; BST[6] = 0; BST[7] = 0;

            Assert.AreEqual(1, BST.Min().Item1);
            Assert.AreEqual(7, BST.Max().Item1);
        }

        [TestMethod]
        public void BST_DelMinMax()
        {
            var BST = new BinarySearchTree<int, object>();
            BST[4] = 0; BST[3] = 0; BST[2] = 0; BST[1] = 0;
            BST[5] = 0; BST[6] = 0; BST[7] = 0;

            BST.DeleteMin();
            Assert.AreEqual(2, BST.Min().Item1);

            BST.DeleteMax();
            Assert.AreEqual(6, BST.Max().Item1);
        }

        [TestMethod]
        public void BST_Rank()
        {
            var BST = new BinarySearchTree<int, object>();
            BST[4] = 0; BST[3] = 0; BST[2] = 0; BST[1] = 0;
            BST[5] = 0; BST[6] = 0; BST[7] = 0;

            Assert.AreEqual(3, BST.Rank(4));
            Assert.AreEqual(6, BST.Rank(7));
            Assert.AreEqual(5, BST.Rank(6));
        }

        [TestMethod]
        public void BST_Enumerator()
        {
            var BST = new BinarySearchTree<int, object>();
            BST[4] = 0; BST[3] = 0; BST[2] = 0; BST[1] = 0;
            BST[5] = 0; BST[6] = 0; BST[7] = 0;

            var keys = BST.Enumerator();
            string actual = "";
            foreach (var item in keys)
            {
                actual += item.ToString();
            }
            Assert.AreEqual("1234567", actual);
        }

        [TestMethod]
        public void BST_Delete()
        {
            var BST = new BinarySearchTree<int, object>();
            BST[4] = 0; BST[3] = 0; BST[2] = 0; BST[1] = 0;
            BST[5] = 0; BST[6] = 0; BST[7] = 0;

            BST.Delete(4);
            BST.Delete(6);

            var keys = BST.Enumerator();
            string actual = "";
            foreach (var item in keys)
            {
                actual += item.ToString();
            }
            Assert.AreEqual("12357", actual);
        }

        [TestMethod]
        public void RBT_Floor()
        {
            var RBT = new RedBlackTree<int, object>();
            RBT[4] = 0;
            RBT[6] = 0;
            RBT[8] = 0;
            RBT[10] = 0;
            RBT[14] = 0;
            RBT[12] = 0;
            RBT[10] = 0;
            Assert.AreEqual(10, RBT.Floor(11).Item1);
        }

        [TestMethod]
        public void RBT_Size()
        {
            var RBT = new RedBlackTree<int, object>();
            RBT[4] = 0; RBT[3] = 0; RBT[2] = 0; RBT[1] = 0;
            RBT[5] = 0; RBT[6] = 0; RBT[7] = 0;

            Assert.AreEqual(7, RBT.Size());
        }

        [TestMethod]
        public void RBT_MinMax()
        {
            var RBT = new RedBlackTree<int, object>();
            RBT[4] = 0; RBT[3] = 0; RBT[2] = 0; RBT[1] = 0;
            RBT[5] = 0; RBT[6] = 0; RBT[7] = 0;

            Assert.AreEqual(1, RBT.Min().Item1);
            Assert.AreEqual(7, RBT.Max().Item1);
        }

        [TestMethod]
        public void RBT_DelMinMax()
        {
            var RBT = new RedBlackTree<int, object>();
            RBT[4] = 0; RBT[3] = 0; RBT[2] = 0; RBT[1] = 0;
            RBT[5] = 0; RBT[6] = 0; RBT[7] = 0;

            RBT.DeleteMin();
            Assert.AreEqual(2, RBT.Min().Item1);

            RBT.DeleteMax();
            Assert.AreEqual(6, RBT.Max().Item1);
        }

        [TestMethod]
        public void RBT_Rank()
        {
            var RBT = new RedBlackTree<int, object>();
            RBT[4] = 0; RBT[3] = 0; RBT[2] = 0; RBT[1] = 0;
            RBT[5] = 0; RBT[6] = 0; RBT[7] = 0;

            Assert.AreEqual(3, RBT.Rank(4));
            Assert.AreEqual(6, RBT.Rank(7));
            Assert.AreEqual(5, RBT.Rank(6));
        }

        [TestMethod]
        public void RBT_Enumerator()
        {
            var RBT = new RedBlackTree<int, object>();
            RBT[4] = 0; RBT[3] = 0; RBT[2] = 0; RBT[1] = 0;
            RBT[5] = 0; RBT[6] = 0; RBT[7] = 0;

            var keys = RBT.Enumerator();
            string actual = "";
            foreach (var item in keys)
            {
                actual += item.ToString();
            }
            Assert.AreEqual("1234567", actual);
        }

        [TestMethod]
        public void RBT_Delete()
        {
            var RBT = new RedBlackTree<int, object>();
            RBT[4] = 0; RBT[3] = 0; RBT[2] = 0; RBT[1] = 0;
            RBT[5] = 0; RBT[6] = 0; RBT[7] = 0;

            RBT.Delete(4);
            RBT.Delete(6);

            var keys = RBT.Enumerator();
            string actual = "";
            foreach (var item in keys)
            {
                actual += item.ToString();
            }
            Assert.AreEqual("12357", actual);
        }
    }
}
