using System.Linq;
using ImageOps.Streaming.Blenders;
using ImageOps.Streaming.Sources;

namespace ImageOps
{
    public static class PixelStreamCompositor
    {
        public static IPixelSource Multiply(this IPixelSource source, params IPixelSource[] layers)
        {
            return layers.Aggregate(source, (current, stream) => new BlendedSource(new MultiplyBlend(), current, stream));
        }

        public static IPixelSource Add(this IPixelSource source, params IPixelSource[] layers)
        {
            return layers.Aggregate(source, (current, stream) => new BlendedSource(new AddBlend(), current, stream));
        }

        public static IPixelSource Mix(this IPixelSource source, params IPixelSource[] layers)
        {
            return layers.Aggregate(source, (current, stream) => new BlendedSource(new NormalBlend(), current, stream));
        }

        public static IPixelSource Burn(this IPixelSource source, params IPixelSource[] layers)
        {
            return layers.Aggregate(source, (current, stream) => new BlendedSource(new BurnBlend(), current, stream));
        }

        public static IPixelSource GrainMerge(this IPixelSource source, params IPixelSource[] layers)
        {
            return layers.Aggregate(source, (current, stream) => new BlendedSource(new GrainMergeBlend(), current, stream));
        }

        public static IPixelSource AddAlphaMask(this IPixelSource source, IPixelSource mask, ColorChannel maskChannel = ColorChannel.Alpha)
        {
            return new BlendedSource(new AlphaMaskBlend(maskChannel), source, mask);
        }
    }
}
