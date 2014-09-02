using System;
using System.Drawing;
using ImageOps.PerformanceTests.Helpers;
using ImageOps.Sources;

namespace ImageOps.PerformanceTests.Scenarios
{
    public class RectangleRegionBlendedSource : SourceTestCase
    {
        protected override IPixelSource CreateSource(int width, int height)
        {
            var rand = new Random(1256);
            var rectangleWidth = 6;
            var rectangleHeight = 4;
            var layer = Utils.CreateColorSource(rectangleWidth, rectangleHeight, Color.Blue);
            var background = Utils.CreateColorSource(width, height);

            for (int i = 0; i < 20; ++i)
            {
                var x = rand.Next(width - rectangleWidth);
                var y = rand.Next(height - rectangleHeight);
                background = background.BlendRegion(
                    Regions.Rectangle(x, y, rectangleWidth, rectangleHeight),
                    layer,
                    BlendingMethods.Normal);
            }
            return background;
        }
    }
}