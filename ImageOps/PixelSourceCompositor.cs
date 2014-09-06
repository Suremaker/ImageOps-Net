using System;
using System.Linq;
using ImageOps.Blenders;
using ImageOps.Sources;
using ImageOps.Sources.Regions;

namespace ImageOps
{
    public static class PixelSourceCompositor
    {
        public static IPixelSource Blend(this IPixelSource source, IBlendingMethod method, params IPixelSource[] layers)
        {
            return layers.Aggregate(source, (current, layer) => new BlendedSource(method, current, layer));
        }

        public static IPixelSource BlendRegion(this IPixelSource source, IRegion region, IPixelSource layer, IBlendingMethod blendingMethod)
        {
            var blendingSource = source as RegionBlendedSource ?? new RegionBlendedSource(source);
            return blendingSource.AddRegion(region, layer, blendingMethod);
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
            return source.Blend(BlendingMethods.ChannelMask(maskChannel), mask);
        }

        public static IPixelSource RepeatSource(this IPixelSource source, int newWidth, int newHeight)
        {
            return new RepeatedSource(source, newWidth, newHeight);
        }
    }
}