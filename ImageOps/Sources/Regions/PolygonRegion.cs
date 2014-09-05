using System;
using System.Drawing;
using System.Linq;

namespace ImageOps.Sources.Regions
{
    public class PolygonRegion : IRegion
    {
        class Section
        {
            public Section(Point item1, Point item2)
            {
                P1 = item1;
                P2 = item2;
                if (P1.X != P2.X)
                {
                    A = (item1.Y - item2.Y) / (double)(item1.X - item2.X);
                    B = item1.Y - A * item1.X;
                }
            }

            public Point P1 { get; private set; }
            public Point P2 { get; private set; }
            public double A { get; private set; }
            public double B { get; private set; }

            public PointF? IsCrossedByLeftHorizontalRay(Point startPoint)
            {
                if (!IsOnOppositeSides(P1.Y, P2.Y, startPoint.Y))
                    return null;
                if (P1.X == P2.X)
                    return P1.X <= startPoint.X ? (PointF?)new Point(P1.X, startPoint.Y) : null;
                var crossX = (startPoint.Y - B) / A;
                var isCrossed = !double.IsInfinity(crossX) && !double.IsNaN(crossX) && crossX <= startPoint.X && !(crossX == P1.X && startPoint.Y == P1.Y);
                return isCrossed ? (PointF?)new PointF((float)crossX, startPoint.Y) : null;
            }

            private static bool IsOnOppositeSides(int sy1, int sy2, int py)
            {
                return Math.Sign(py - sy1) * Math.Sign(py - sy2) != 1;
            }
        }
        private readonly Section[] _sections;

        public PolygonRegion(params Point[] points)
        {
            if (points == null || points.Length == 0)
                throw new ArgumentException("PolygonRegion has to have at least 1 point");
            _sections = points.Zip(points.Skip(1).Concat(points.Take(1)), (p1, p2) => new Section(p1, p2)).ToArray();

            BoundingBox = PixelRectangle.FromPoints(points.Min(p => p.X), points.Min(p => p.Y), points.Max(p => p.X), points.Max(p => p.Y));
        }

        public bool IsInside(int x, int y)
        {
            if (x < BoundingBox.Left || x > BoundingBox.Right || y < BoundingBox.Top || y > BoundingBox.Bottom)
                return false;
            var px = new Point(x, y);
            int crossCount = 0;
            foreach (var section in _sections)
            {
                var point=section.IsCrossedByLeftHorizontalRay(px);
                if (point == null) 
                    continue;
                if (point.Value.X == x && point.Value.Y == y) 
                    return true;
                    crossCount += 1;
            }
            return (crossCount % 2) == 1;
        }

        public PixelRectangle BoundingBox { get; private set; }
    }
}