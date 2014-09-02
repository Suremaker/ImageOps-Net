using ImageOps.PerformanceTests.Helpers;
using ImageOps.Sources;

namespace ImageOps.PerformanceTests.Scenarios
{
    public class CroppedSource : SourceTestCase
    {
        protected override IPixelSource CreateSource(int width, int height)
        {
            return Utils.CreateStandardSource(width + 40, height + 40).Crop(new PixelRectangle(15, 15, width, height));
        }
    }
}