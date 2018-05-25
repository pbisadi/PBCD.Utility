using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PBCD.Algorithms.ComputationalGeometry;

namespace PBCD.Algorithms.ComputationalGeometry_UnitTest
{
    [TestClass]
    public class ConvexHull_UnitTest
    {
        [TestMethod]
        public void GrahamScan_Basic()
        {
            Point2D[] p = { 
                              new Point2D(2,2), 
                              new Point2D(0,1), 
                              new Point2D(-2,2), 
                              new Point2D(-2,0), 
                              new Point2D(0,-1), 
                              new Point2D(2,0) 
                          };
            Point2D[] copy = new Point2D[6];
            p.CopyTo(copy,0);
            var actual = ConvexHull.GrahamScan(copy);
            Assert.AreEqual(p[3].ToString(), actual[0].ToString());
            Assert.AreEqual(p[2].ToString(), actual[1].ToString());
            Assert.AreEqual(p[0].ToString(), actual[2].ToString());
            Assert.AreEqual(p[5].ToString(), actual[3].ToString());
            Assert.AreEqual(p[4].ToString(), actual[4].ToString());
        }

        [TestMethod]
        public void GrahamScan_InlineHorPoints()
        {
            Point2D[] p = { 
                              new Point2D(-1,0), 
                              new Point2D(0,0), 
                              new Point2D(1,0), 
                              new Point2D(0,-1) 
                          };
            Point2D[] copy = new Point2D[4];
            p.CopyTo(copy, 0);
            var actual = ConvexHull.GrahamScan(copy);
            Assert.AreEqual(p[0].ToString(), actual[0].ToString());
            Assert.AreEqual(p[2].ToString(), actual[1].ToString());
            Assert.AreEqual(p[3].ToString(), actual[2].ToString());
        }
    }
}
