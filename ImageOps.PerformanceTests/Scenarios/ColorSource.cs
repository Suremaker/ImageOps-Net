using System.Drawing;
using ImageOps.PerformanceTests.Helpers;
using ImageOps.Sources;

namespace ImageOps.PerformanceTests.Scenarios
{
    public class ColorSource : SourceTestCase
    {
        protected override IPixelSource CreateSource(int width, int height)
        {
            return Color.Green.AsPixelSource(width, height);
        }
    }
}