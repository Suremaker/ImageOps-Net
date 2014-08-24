﻿using System;

namespace ImageOps.Blenders
{
    public class AlphaMaskBlend : BlendingMethod
    {
        private readonly Func<PixelColor, float> _alphaSelector;

        public AlphaMaskBlend(ColorChannel maskChannel)
        {
            _alphaSelector = CreateSelector(maskChannel);
        }

        private Func<PixelColor, float> CreateSelector(ColorChannel maskChannel)
        {
            switch (maskChannel)
            {
                case ColorChannel.Red:
                    return c => c.GetRed();
                case ColorChannel.Green:
                    return c => c.GetGreen();
                case ColorChannel.Blue:
                    return c => c.GetBlue();
                default:
                    return c => c.GetAlpha();
            }
        }

        public override PixelColor Blend(PixelColor source, PixelColor mask)
        {
            float alpha = CalculateAlpha(source.GetAlpha(), _alphaSelector(mask));
            return PixelColor.FromFargb(alpha, source.GetRed(), source.GetGreen(), source.GetBlue());
        }

        private float CalculateAlpha(float sourceAlpha, float maskAlpha)
        {
            return sourceAlpha*maskAlpha;
        }
    }
}