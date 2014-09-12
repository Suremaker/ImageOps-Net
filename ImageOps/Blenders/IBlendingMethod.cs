using ImageOps.Sources;
using ImageOps.Sources.Readers;

namespace ImageOps.Blenders
{
    public interface IBlendingMethod
    {
        PixelColor Blend(PixelColor background, PixelColor foreground);
        IPixelReader OpenBlendingReader(IPixelSource background, IPixelSource foregorund);
    }
}