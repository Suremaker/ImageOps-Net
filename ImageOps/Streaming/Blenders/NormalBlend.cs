namespace ImageOps.Streaming.Blenders
{
    public class NormalBlend : BlendingStream
    {
        public override PixelColor Blend(PixelColor back, PixelColor front)
        {
            var backAlpha = back.GetAlpha();
            var frontAlpha = front.GetAlpha();
            var outAlpha = backAlpha + frontAlpha - backAlpha * frontAlpha;

            return PixelColor.FromFargb(
                outAlpha,
                Blend(back.GetRed(), front.GetRed(), backAlpha, frontAlpha, outAlpha),
                Blend(back.GetGreen(), front.GetGreen(), backAlpha, frontAlpha, outAlpha),
                Blend(back.GetBlue(), front.GetBlue(), backAlpha, frontAlpha, outAlpha));
        }

        protected float Blend(float color1, float color2, float alpha1, float alpha2, float alpha3)
        {
            return (color2 * alpha2 + Comp(color1 * alpha1, alpha2)) / alpha3;
        }
    }
}
