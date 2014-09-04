using System;

namespace ImageOps.Blenders
{
    public class AlphaMaskBlend : BlendingMethod
    {
        private readonly Func<PixelColor, double> _alphaSelector;

        public AlphaMaskBlend(ColorChannel maskChannel)
        {
            _alphaSelector = CreateSelector(maskChannel);
        }

        private Func<PixelColor, double> CreateSelector(ColorChannel maskChannel)
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
            double alpha = CalculateAlpha(source.GetAlpha(), _alphaSelector(mask));
            return PixelColor.FromFargb(alpha, source.GetRed(), source.GetGreen(), source.GetBlue());
        }

        private double CalculateAlpha(double sourceAlpha, double maskAlpha)
        {
            return sourceAlpha*maskAlpha;
        }
    }
}