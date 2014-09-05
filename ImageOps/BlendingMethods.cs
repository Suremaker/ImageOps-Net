using System;
using ImageOps.Blenders;

namespace ImageOps
{
    public static class BlendingMethods
    {
        public static readonly IBlendingMethod Add = new AddBlend();
        public static readonly IBlendingMethod Burn = new BurnBlend();
        public static readonly IBlendingMethod GrainMerge = new GrainMergeBlend();
        public static readonly IBlendingMethod Multiply = new MultiplyBlend();
        public static readonly IBlendingMethod Normal = new NormalBlend();
        public static readonly IBlendingMethod AlphaMask = new AlphaMaskBlend();
        public static readonly IBlendingMethod RedMask = new RedMaskBlend();
        public static readonly IBlendingMethod GreenMask = new GreenMaskBlend();
        public static readonly IBlendingMethod BlueMask = new BlueMaskBlend();
        public static IBlendingMethod ChannelMask(ColorChannel maskChannel)
        {
            switch (maskChannel)
            {
                case ColorChannel.Alpha:
                    return AlphaMask;
                case ColorChannel.Red:
                    return RedMask;
                case ColorChannel.Green:
                    return GreenMask;
                case ColorChannel.Blue:
                    return BlueMask;
            }
            throw new ArgumentException(string.Format("Unknown ColorChannel value: {0}", maskChannel));
        }
    }
}