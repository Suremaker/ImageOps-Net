using System;
using System.Drawing;
using System.Linq;

namespace ImageOps.Sources.Regions
{
    public class PolygonRegion : IRegion
    {
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
            if (!BoundingBox.IsInside(x, y))
                return false;
            var px = new Point(x, y);
            int crossCount = 0;
            foreach (var section in _sections)
            {
                var point = section.IsCrossedByLeftHorizontalRay(px);
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