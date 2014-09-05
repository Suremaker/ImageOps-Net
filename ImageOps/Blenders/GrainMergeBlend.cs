namespace ImageOps.Blenders
{
    public class GrainMergeBlend : IBlendingMethod
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
            return Discrete.BlendWithRatio(backColor, Merge(backColor, frontColor), ratio);
        }

        private static int Merge(int backColor, int frontColor)
        {
            return Discrete.Clamp(backColor + frontColor - 128);
        }
    }
}