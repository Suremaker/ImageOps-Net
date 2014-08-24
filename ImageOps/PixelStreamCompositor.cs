using System.Linq;
using ImageOps.Streaming.Blenders;
using ImageOps.Streaming.Sources;

namespace ImageOps
{
    public static class PixelStreamCompositor
    {
        private static IPixelStream nullSource = new ColorSource(1, 1, PixelColor.FromRgb(0, 0, 0));
        public static IPixelSource Multiply(this IPixelSource source, params IPixelSource[] layers)
        {
            return layers.Aggregate(source, (current, stream) => new BlendedSource(new MultiplyBlend(nullSource, nullSource), current, stream));
        }

        public static IPixelSource Add(this IPixelSource source, params IPixelSource[] layers)
        {
            return layers.Aggregate(source, (current, stream) => new BlendedSource(new AddBlend(nullSource, nullSource), current, stream));
        }

        public static IPixelSource Mix(this IPixelSource source, params IPixelSource[] layers)
        {
            return layers.Aggregate(source, (current, stream) => new BlendedSource(new NormalBlend(nullSource, nullSource), current, stream));
        }

        public static IPixelSource Burn(this IPixelSource source, params IPixelSource[] layers)
        {
            return layers.Aggregate(source, (current, stream) => new BlendedSource(new BurnBlend(nullSource, nullSource), current, stream));
        }

        public static IPixelSource GrainMerge(this IPixelSource source, params IPixelSource[] layers)
        {
            return layers.Aggregate(source, (current, stream) => new BlendedSource(new GrainMergeBlend(nullSource, nullSource), current, stream));
        }

        public static IPixelSource AddAlphaMask(this IPixelSource source, IPixelSource mask, ColorChannel maskChannel = ColorChannel.Alpha)
        {
            return new BlendedSource(new AlphaMaskBlend(nullSource, nullSource, maskChannel), source, mask);
        }
    }
}
