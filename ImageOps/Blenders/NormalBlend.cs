namespace ImageOps.Blenders
{
    public class NormalBlend : IBlendingMethod
    {
        public PixelColor Blend(PixelColor back, PixelColor front)
        {
            if (front.A == Discrete.MaxColor)
                return new PixelColor(front.Argb);
            if (front.A == 0)
                return new PixelColor(back.Argb);
            var backAlpha = back.A;
            var frontAlpha = front.A;
            var outAlpha = (byte)(backAlpha + frontAlpha - Discrete.MulRatio(backAlpha, frontAlpha));

            return new PixelColor(
                outAlpha,
                Blend(back.R, front.R, backAlpha, frontAlpha, outAlpha),
                Blend(back.G, front.G, backAlpha, frontAlpha, outAlpha),
                Blend(back.B, front.B, backAlpha, frontAlpha, outAlpha));
        }

        private static byte Blend(int backColor, int frontColor, int backAlpha, int frontAlpha, int outAlpha)
        {
            return (byte)((Discrete.MulRatio(frontColor, frontAlpha) + Discrete.CompRatio(Discrete.MulRatio(backColor, backAlpha), frontAlpha)) * Discrete.MaxColor / outAlpha);
        }
    }
}