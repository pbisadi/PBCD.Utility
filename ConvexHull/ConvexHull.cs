using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCD.Algorithms.ComputationalGeometry
{
    public static class ConvexHull
    {
        public static Point2D[] GrahamScan(Point2D[] p)
        {
            Debug.Assert(p != null);
            Debug.Assert(p.Length > 2);

            Stack<Point2D> hull = new Stack<Point2D>();

            Array.Sort(p, Point2D.Y_ORDER);     //p[0] is now point with lowest y-coordinate
            Array.Sort(p, p[0].POLAR_ORDER);    //sort with respect to p[0]

            hull.Push(p[0]);
            hull.Push(p[1]);

            for (int i = 2; i < p.Length; i++)
            {
                Point2D top = hull.Pop();
                while (Point2D.ccw(hull.Peek(), top, p[i]) <= 0)
                {
                    top = hull.Pop();
                }
                hull.Push(top);
                hull.Push(p[i]);
            }

            return hull.ToArray();
        }
    }
}
