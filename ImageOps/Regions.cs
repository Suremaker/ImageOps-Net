using System.Drawing;
using ImageOps.Sources.Regions;

namespace ImageOps
{
    public static class Regions
    {
        public static IRegion Polygon(params Point[] points) { return new PolygonRegion(points); }
        public static IRegion Rectangle(int x, int y, int width, int height)
        {
            return new PolygonRegion(
                new Point(x, y),
                new Point(x + width - 1, y),
                new Point(x + width - 1, y + height - 1),
                new Point(x, y + height - 1));
        }
    }
}