using ImageOps.PerformanceTests.Helpers;
using ImageOps.Sources;

namespace ImageOps.PerformanceTests.Scenarios
{
    public class AlphaMaskBlendedSource : SourceTestCase
    {
        protected override IPixelSource CreateSource(int width, int height)
        {
            return Utils.CreateColorSource(width, height).AddAlphaMask(Utils.CreateStandardSource(width, height));
        }
    }
    public class RedMaskBlendedSource : SourceTestCase
    {
        protected override IPixelSource CreateSource(int width, int height)
        {
            return Utils.CreateColorSource(width, height).AddAlphaMask(Utils.CreateStandardSource(width, height), ColorChannel.Red);
        }
    }
    public class GreenMaskBlendedSource : SourceTestCase
    {
        protected override IPixelSource CreateSource(int width, int height)
        {
            return Utils.CreateColorSource(width, height).AddAlphaMask(Utils.CreateStandardSource(width, height), ColorChannel.Green);
        }
    }
    public class BlueMaskBlendedSource : SourceTestCase
    {
        protected override IPixelSource CreateSource(int width, int height)
        {
            return Utils.CreateColorSource(width, height).AddAlphaMask(Utils.CreateStandardSource(width, height), ColorChannel.Blue);
        }
    }
}