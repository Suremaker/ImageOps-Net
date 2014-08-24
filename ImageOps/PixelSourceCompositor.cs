using System.Linq;
using ImageOps.Blenders;
using ImageOps.Sources;

namespace ImageOps
{
    public static class PixelSourceCompositor
    {
        public static IPixelSource Blend(this IPixelSource source, IBlendingMethod method, params IPixelSource[] layers)
        {
            return layers.Aggregate(source, (current, stream) => new BlendedSource(method, current, stream));
        }

        public static IPixelSource Multiply(this IPixelSource source, params IPixelSource[] layers)
        {
            return source.Blend(BlendingMethods.Multiply, layers);
        }

        public static IPixelSource Add(this IPixelSource source, params IPixelSource[] layers)
        {
            return source.Blend(BlendingMethods.Add, layers);
        }

        public static IPixelSource Mix(this IPixelSource source, params IPixelSource[] layers)
        {
            return source.Blend(BlendingMethods.Normal, layers);
        }

        public static IPixelSource Burn(this IPixelSource source, params IPixelSource[] layers)
        {
            return source.Blend(BlendingMethods.Burn, layers);
        }

        public static IPixelSource GrainMerge(this IPixelSource source, params IPixelSource[] layers)
        {
            return source.Blend(BlendingMethods.GrainMerge, layers);
        }

        public static IPixelSource AddAlphaMask(this IPixelSource source, IPixelSource mask, ColorChannel maskChannel = ColorChannel.Alpha)
        {
            return source.Blend(new AlphaMaskBlend(maskChannel), mask);
        }
    }
}