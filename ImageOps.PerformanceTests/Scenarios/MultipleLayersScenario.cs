using System.Drawing;
using ImageOps.PerformanceTests.Helpers;
using ImageOps.Sources;

namespace ImageOps.PerformanceTests.Scenarios
{
    public class MultipleLayersScenario : SourceTestCase
    {
        protected override IPixelSource CreateSource(int width, int height)
        {
            var halfWidth = width / 2;
            var halfHeight = height / 2;
            var bitmap = Utils.CreateStandardBitmap(width, height).AsPixelSource();
            var topLeft = bitmap.Multiply(Color.Yellow.AsPixelSource(width, height)).AddAlphaMask(Color.White.AsPixelSource(halfWidth, halfHeight).Expand(0, 0, halfWidth, halfHeight, PixelColor.Transparent));
            var bottomRight = bitmap.Crop(halfWidth, halfHeight, halfWidth, halfHeight).Add(Color.Gray.AsPixelSource(halfWidth, halfHeight));
            var background = Color.DarkOrange.AsPixelSource(width, height);
            return background
                .Mix(topLeft)
                .BlendRegion(Regions.Rectangle(halfWidth, halfHeight, halfWidth, halfHeight), bottomRight, BlendingMethods.Normal)
                .BlendRegion(Regions.Polygon(new Point(halfWidth, 0), new Point(0, height - 1), new Point(width - 1, height - 1)), PixelColor.FromGrayscale(25).AsPixelSource(width, height), BlendingMethods.Multiply);
        }
    }
}