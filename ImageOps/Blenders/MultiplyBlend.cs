namespace ImageOps.Blenders
{
    /// <summary>
    /// Multiply blend algorithm basing on GIMP multiply mode
    /// </summary>
    public class MultiplyBlend : IBlendingMethod
    {
        public PixelColor Blend(PixelColor back, PixelColor front)
        {
            if (front.A * back.A == 0) return new PixelColor();
            int ratio = Discrete.CalcAlphaRatio(back.A, front.A);
            return new PixelColor(
                back.A,
                Blend(back.R, front.R, ratio),
                Blend(back.G, front.G, ratio),
                Blend(back.B, front.B, ratio));
        }

        private static byte Blend(int backColor, int frontColor, int ratio)
        {
            return Discrete.BlendWithRatio(backColor, Multiply(backColor, frontColor), ratio);
        }

        private static int Multiply(int backColor, int frontColor)
        {
            return Discrete.DivBy255(backColor * frontColor);
        }
    }
}