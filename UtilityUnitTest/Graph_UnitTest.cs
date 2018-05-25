using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PBCD.Algorithms.Graphs;
using PBCD.Algorithms.Graphs.Digraph;

namespace PBCD.Algorithms.Graphs_UnitTest
{
    /// <summary>
    /// Summary description for Graph_UnitTest
    /// </summary>
    [TestClass]
    public sealed class Graph_UnitTest
    {
        public Graph_UnitTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static Graph basicGraph;
        private static Digraph baseicDigraph;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            basicGraph = new Graph(9);
            basicGraph.AddEdge(0, 1);
            basicGraph.AddEdge(0, 2);
            basicGraph.AddEdge(0, 5);
            basicGraph.AddEdge(0, 6);
            basicGraph.AddEdge(2, 7);
            basicGraph.AddEdge(6, 4);
            basicGraph.AddEdge(4, 3);
            basicGraph.AddEdge(4, 5);

            baseicDigraph = new Digraph(7);
            baseicDigraph.AddEdge(5, 0);
            baseicDigraph.AddEdge(2, 4);
            baseicDigraph.AddEdge(3, 2);
            baseicDigraph.AddEdge(1, 2);
            baseicDigraph.AddEdge(0, 1);
            baseicDigraph.AddEdge(4, 3);
            baseicDigraph.AddEdge(3, 5);
            baseicDigraph.AddEdge(0, 2);
            baseicDigraph.AddEdge(6, 5);
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Graph_API_Test()
        {
            Graph G = new Graph(3);
            G.AddEdge(0, 1);
            G.AddEdge(0, 2);
            List<int> e = new List<int>(G.Adj(0));
            Assert.AreEqual(1, e[0]);
        }

        [TestMethod]
        public void DFS_Test()
        {
            DepthFirstPaths DFS = new DepthFirstPaths(basicGraph, 0);
            Assert.IsTrue(DFS.HasPathTo(3));
            Assert.IsFalse(DFS.HasPathTo(8));
        }

        [TestMethod]
        public void BFS_Test()
        {
            BreadthFirstPaths BFS = new BreadthFirstPaths(basicGraph, 0);
            Assert.IsTrue(BFS.HasPathTo(3));
            Assert.IsFalse(BFS.HasPathTo(8));
            var path = BFS.PathTo(7);
            var SB = new StringBuilder();
            foreach (int v in path)
            {
                SB.Append(v.ToString());
            }
            Assert.AreEqual("027", SB.ToString());
        }

        [TestMethod]
        public void BFS_MultiSource_Test()
        {
            var sources = new HashSet<int>() { 0, 3 };
            BreadthFirstPaths BFS = new BreadthFirstPaths(basicGraph, sources);
            Assert.IsTrue(BFS.HasPathTo(3));
            Assert.IsFalse(BFS.HasPathTo(8));
            var path = BFS.PathTo(7);
            var SB = new StringBuilder();
            foreach (int v in path)
            {
                SB.Append(v.ToString());
            }
            Assert.AreEqual("027", SB.ToString());
            Stack<int> path4 = (Stack<int>)BFS.PathTo(4); //We know that returned type is an stack
            Assert.AreEqual(3, path4.Pop());
        }

        [TestMethod]
        public void ConnectedComponents_Test()
        {
            ConnectedComponents CC = new ConnectedComponents(basicGraph);
            Assert.AreEqual(2, CC.Count());
            Assert.AreEqual(0, CC.ID(0));
            Assert.AreEqual(1, CC.ID(8));
        }

        [TestMethod]
        public void StronglyConnectedComponents_Test()
        {
            SCC_KosarajuSharir CC = new SCC_KosarajuSharir(baseicDigraph);
            Assert.AreEqual(2, CC.Count());
            Assert.AreEqual(0, CC.ID(0));
            Assert.AreEqual(1, CC.ID(6));
        }

        [TestMethod]
        public void Bipartite_Test()
        {
            var BG = new Graph(7);
            BG.AddEdge(0, 1);
            BG.AddEdge(0, 2);
            BG.AddEdge(0, 5);
            BG.AddEdge(0, 6);
            BG.AddEdge(2, 3);
            BG.AddEdge(2, 4);
            BG.AddEdge(1, 3);
            BG.AddEdge(4, 6);
            BG.AddEdge(4, 5);
            IEnumerable<int> G1;
            IEnumerable<int> G2;
            bool actual = BipartiteGraph.Depart(BG, out G1, out G2);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Digraph_API_Test()
        {
            Digraph G = new Digraph(3);
            G.AddEdge(0, 1);
            G.AddEdge(0, 2);
            List<int> e = new List<int>(G.Adj(0));
            Assert.AreEqual(1, e[0]);
        }

        [TestMethod]
        public void DirectedDFS_Test()
        {
            DirectedDFS DFS = new DirectedDFS(baseicDigraph, 0);
            Assert.IsTrue(DFS.HasPathTo(5));
            Assert.IsFalse(DFS.HasPathTo(6));
        }

        [TestMethod]
        public void TopologicalSort_Test()
        {
            var G = new Digraph(4);
            G.AddEdge(3, 2);
            G.AddEdge(3, 1);
            G.AddEdge(1, 0);
            G.AddEdge(2, 0);
            var DFO = new TopologicalSort(G);
            var actual = new List<int>( DFO.GetTopologicalySorted());
            Assert.AreEqual(3, actual[0]);
            Assert.AreEqual(0, actual[3]);
        }

        [TestMethod]
        public void DirectedBFS_MultiSource_Test()
        {
            var sources = new HashSet<int>() { 0, 4 };
            DirectedBFS BFS = new DirectedBFS(baseicDigraph, sources);
            Assert.IsTrue(BFS.HasPathTo(3));
            Assert.IsFalse(BFS.HasPathTo(6));
            var path = BFS.PathTo(5);
            var SB = new StringBuilder();
            foreach (int v in path)
            {
                SB.Append(v.ToString());
            }
            Assert.AreEqual("435", SB.ToString());
            Stack<int> path4 = (Stack<int>)BFS.PathTo(2); //We know that returned type is an stack
            Assert.AreEqual(0, path4.Pop());
        }

        //[TestMethod]
        //public void Isomorphic_Test()
        //{
        //    var G1 = new Graph(7);
        //    G1.AddEdge(0, 1);
        //    G1.AddEdge(0, 2);
        //    G1.AddEdge(0, 6);
        //    G1.AddEdge(0, 5);
        //    G1.AddEdge(4, 5);
        //    G1.AddEdge(4, 3);
        //    G1.AddEdge(4, 6);
        //    G1.AddEdge(3, 5);

        //    var G2 = new Graph(7);
        //    G2.AddEdge(4, 0);
        //    G2.AddEdge(4, 1);
        //    G2.AddEdge(4, 2);
        //    G2.AddEdge(4, 3);
        //    G2.AddEdge(5, 0);
        //    G2.AddEdge(5, 1);
        //    G2.AddEdge(5, 6);
        //    G2.AddEdge(0, 6);

        //    Assert.IsTrue(Isomorphic.IsIsomorphic(G1, G2));
        //    Assert.IsFalse(Isomorphic.IsIsomorphic(G1, basicGraph));
        //    Assert.IsFalse(Isomorphic.IsIsomorphic(G2, basicGraph));
        //}
    }
}
