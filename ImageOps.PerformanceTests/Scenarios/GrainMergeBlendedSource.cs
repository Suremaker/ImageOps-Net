using ImageOps.PerformanceTests.Helpers;
using ImageOps.Sources;

namespace ImageOps.PerformanceTests.Scenarios
{
    public class GrainMergeBlendedSource : SourceTestCase
    {
        protected override IPixelSource CreateSource(int width, int height)
        {
            return Utils.CreateColorSource(width, height).GrainMerge(Utils.CreateStandardSource(width, height));
        }
    }
}