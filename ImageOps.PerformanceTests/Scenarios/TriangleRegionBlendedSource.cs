using System;
using System.Drawing;
using ImageOps.PerformanceTests.Helpers;
using ImageOps.Sources;

namespace ImageOps.PerformanceTests.Scenarios
{
    public class TriangleRegionBlendedSource : SourceTestCase
    {
        protected override IPixelSource CreateSource(int width, int height)
        {
            var rand = new Random(1256);
            var triangleSize = 6;
            var layer = Utils.CreateColorSource(triangleSize, triangleSize, Color.Blue);
            var background = Utils.CreateColorSource(width, height);

            for (int i = 0; i < 20; ++i)
            {
                var x = rand.Next(width - triangleSize);
                var y = rand.Next(height - triangleSize);
                background = background.BlendRegion(
                    Regions.Polygon(
                        new Point(x, y),
                        new Point(x + triangleSize - 1, y),
                        new Point(x + triangleSize - 1, y + triangleSize - 1)),
                    layer,
                    BlendingMethods.Normal);
            }
            return background;
        }
    }
}