using System;

namespace ImageOps.Blenders
{
    public abstract class BlendingMethod : IBlendingMethod
    {
        public abstract PixelColor Blend(PixelColor back, PixelColor front);

        protected static double Comp(double color, double alpha)
        {
            return color*(1 - alpha);
        }

        protected static double Clamp(double value)
        {
            return Math.Min(1, Math.Max(0, value));
        }
    }
}