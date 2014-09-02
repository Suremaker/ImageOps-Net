using System.Drawing;
using ImageOps.PerformanceTests.Helpers;
using ImageOps.Sources;

namespace ImageOps.PerformanceTests.Scenarios
{
    public class ExpandedSource : SourceTestCase
    {
        protected override IPixelSource CreateSource(int width, int height)
        {
            return Utils.CreateStandardSource(width - 40, height - 40).Expand(15, 10, 25, 30, Color.Red);
        }
    }
}