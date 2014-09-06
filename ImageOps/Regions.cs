using System.Drawing;
using ImageOps.Sources.Regions;

namespace ImageOps
{
    public static class Regions
    {
        public static IRegion Polygon(params Point[] points) { return new PolygonRegion(points); }
        public static IRegion Rectangle(int x, int y, int width, int height) { return new RectangleRegion(x, y, width, height); }
    }
}