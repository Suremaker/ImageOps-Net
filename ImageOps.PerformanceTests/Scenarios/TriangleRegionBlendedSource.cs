using System;
using System.Drawing;
using ImageOps.PerformanceTests.Helpers;
using ImageOps.Sources;

namespace ImageOps.PerformanceTests.Scenarios
{
    public class TriangleRegionBlendedSource : SourceTestCase
    {
        protected virtual int TriangleCount { get { return 20; } }
        protected virtual int TriangleSize { get { return 6; } }

        protected override IPixelSource CreateSource(int width, int height)
        {
            var rand = new Random(1256);
            var layer = Utils.CreateColorSource(TriangleSize, TriangleSize, Color.Blue);
            var background = Utils.CreateColorSource(width, height);

            for (int i = 0; i < TriangleCount; ++i)
            {
                var x = rand.Next(width - TriangleSize);
                var y = rand.Next(height - TriangleSize);
                background = background.BlendRegion(
                    Regions.Polygon(
                        new Point(x, y),
                        new Point(x + TriangleSize - 1, y),
                        new Point(x + TriangleSize - 1, y + TriangleSize - 1)),
                    layer,
                    BlendingMethods.Normal);
            }
            return background;
        }
    }

    public class BiggerTriangleRegionBlendedSource : TriangleRegionBlendedSource
    {
        protected override int TriangleCount { get { return 1000; } }
        protected override int TriangleSize { get { return 12; } }
    }
}