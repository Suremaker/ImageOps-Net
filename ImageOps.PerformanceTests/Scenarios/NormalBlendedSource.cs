using ImageOps.PerformanceTests.Helpers;
using ImageOps.Sources;

namespace ImageOps.PerformanceTests.Scenarios
{
    public class NormalBlendedSource : SourceTestCase
    {
        protected override IPixelSource CreateSource(int width, int height)
        {
            return Utils.CreateColorSource(width, height).Mix(Utils.CreateStandardSource(width, height));
        }
    }
}