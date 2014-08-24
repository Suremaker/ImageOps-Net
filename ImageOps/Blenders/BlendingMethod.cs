﻿using System;

namespace ImageOps.Blenders
{
    public abstract class BlendingMethod : IBlendingMethod
    {
        public abstract PixelColor Blend(PixelColor back, PixelColor front);

        protected static float Comp(float color, float alpha)
        {
            return color*(1 - alpha);
        }

        protected static float Clamp(float value)
        {
            return Math.Min(1, Math.Max(0, value));
        }
    }
}