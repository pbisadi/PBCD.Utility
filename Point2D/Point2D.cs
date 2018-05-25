using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PBCD.Algorithms.ComputationalGeometry
{
    public class Point2D
    {
        private readonly double X, Y;

        public Point2D(double x, double y)
        {
            POLAR_ORDER = new PolarOrder(this);
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return X + " , " + Y;
        }

        public readonly Comparer<Point2D> POLAR_ORDER;
        private class PolarOrder : Comparer<Point2D>
        {
            Point2D P = null;
            public PolarOrder(Point2D parent) { P = parent; }

            // compare other points relative to polar angle (between 0 and 2pi) they make with this Point
            public override int Compare(Point2D q1, Point2D q2)
            {
                double dx1 = q1.X - P.X;
                double dy1 = q1.Y - P.Y;
                double dx2 = q2.X - P.X;
                double dy2 = q2.Y - P.Y;

                if (dy1 >= 0 && dy2 < 0) return -1;    // q1 above; q2 below
                else if (dy2 >= 0 && dy1 < 0) return +1;    // q1 below; q2 above
                else if (dy1 == 0 && dy2 == 0)
                {            // 3-collinear and horizontal
                    if (dx1 >= 0 && dx2 < 0) return -1;
                    else if (dx2 >= 0 && dx1 < 0) return +1;
                    else return 0;
                }
                else return -ccw(P, q1, q2);     // both above or below

                // Note: ccw() recomputes dx1, dy1, dx2, and dy2

            }
        }

        public static readonly Comparer<Point2D> Y_ORDER = new YOrder();
        private class YOrder : Comparer<Point2D>
        {
            public override int Compare(Point2D p, Point2D q)
            {
                if (p.Y < q.Y) return -1;
                if (p.Y > q.Y) return +1;
                return 0;
            }
        }

        /// <summary>
        /// Is a->b->c a counterclockwise turn?
        /// </summary>
        /// <returns>{ -1, 0, +1 } if a->b->c is a { clockwise, collinear; counterclocwise } turn</returns>
        public static int ccw(Point2D a, Point2D b, Point2D c)
        {
            double area2 = (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
            if (area2 < 0) return -1;
            else if (area2 > 0) return +1;
            else return 0;
        }
    }
}
