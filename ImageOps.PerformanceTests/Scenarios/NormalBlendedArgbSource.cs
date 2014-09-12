using ImageOps.PerformanceTests.Helpers;
using ImageOps.Sources;

namespace ImageOps.PerformanceTests.Scenarios
{
    public class NormalBlendedArgbSource : SourceTestCase
    {
        protected override IPixelSource CreateSource(int width, int height)
        {
            return Utils.CreateColorSource(width, height).Mix(Utils.CreateStandardSourceWithTransparency(width, height));
        }
    }
}