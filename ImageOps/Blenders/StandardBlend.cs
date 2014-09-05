using System;

namespace ImageOps.Blenders
{
    /// <summary>
    /// Generic blend algorithm basing on GIMP multiply mode
    /// </summary>
    public abstract class StandardBlend : BlendingMethod
    {
        public override PixelColor Blend(PixelColor back, PixelColor front)
        {
            double ratio = CalcRatio(back.GetAlpha(), front.GetAlpha());

            if (ratio == 0.0)
                return PixelColor.FromArgb(0, 0, 0, 0);

            return PixelColor.FromFargb(
                back.GetAlpha(),
                Blend(back.GetRed(), front.GetRed(), ratio),
                Blend(back.GetGreen(), front.GetGreen(), ratio),
                Blend(back.GetBlue(), front.GetBlue(), ratio));
        }

        private double Blend(double color1, double color2, double ratio)
        {
            return Blend(color1, color2)*ratio + Comp(color1, ratio);
        }

        protected abstract double Blend(double color1, double color2);

        private static double CalcRatio(double backAlpha, double frontAlpha)
        {
            var minAlpha = Math.Min(backAlpha, frontAlpha);
            var newAlpha = backAlpha + Comp(minAlpha, backAlpha);
            return newAlpha != 0.0 ? minAlpha/newAlpha : 0;
        }
    }
}