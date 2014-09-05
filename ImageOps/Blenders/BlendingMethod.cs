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
            if (value > 1) return 1;
            return value > 0 ? value : 0;
        }
    }
}