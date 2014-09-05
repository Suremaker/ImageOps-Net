namespace ImageOps.Blenders
{
    public class BurnBlend : IBlendingMethod
    {
        public PixelColor Blend(PixelColor back, PixelColor front)
        {
            if (Discrete.HasNothingToBlend(back.A, front.A))
                return new PixelColor(back.Argb);
            int ratio = Discrete.CalcAlphaRatio(back.A, front.A);
            return new PixelColor(
                back.A,
                Blend(back.R, front.R, ratio),
                Blend(back.G, front.G, ratio),
                Blend(back.B, front.B, ratio));
        }

        private static byte Blend(int backColor, int frontColor, int ratio)
        {
            return Discrete.BlendWithRatio(backColor, Burn(backColor, frontColor), ratio);
        }

        private static int Burn(int backColor, int frontColor)
        {
            if (frontColor == 0)
                return 0;
            return Discrete.MaxColor - Discrete.Clamp(((Discrete.MaxColor - backColor) * Discrete.MaxColor) / frontColor);
        }
    }
}