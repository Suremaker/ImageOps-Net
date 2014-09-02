using ImageOps.PerformanceTests.Helpers;
using ImageOps.Sources;

namespace ImageOps.PerformanceTests.Scenarios
{
    public class ComputedSource : SourceTestCase
    {
        protected override IPixelSource CreateSource(int width, int height)
        {
            return new Sources.ComputedSource(width, height, (x, y) => PixelColor.FromGrayscale((byte)((x + y) % 256)));
        }
    }
}