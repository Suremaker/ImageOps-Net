namespace ImageOps.Blenders
{
    public class NormalBlend : BlendingMethod
    {
        public override PixelColor Blend(PixelColor back, PixelColor front)
        {
            var backAlpha = back.GetAlpha();
            var frontAlpha = front.GetAlpha();
            var outAlpha = backAlpha + frontAlpha - backAlpha*frontAlpha;

            return PixelColor.FromFargb(
                outAlpha,
                Blend(back.GetRed(), front.GetRed(), backAlpha, frontAlpha, outAlpha),
                Blend(back.GetGreen(), front.GetGreen(), backAlpha, frontAlpha, outAlpha),
                Blend(back.GetBlue(), front.GetBlue(), backAlpha, frontAlpha, outAlpha));
        }

        protected double Blend(double color1, double color2, double alpha1, double alpha2, double alpha3)
        {
            return (color2*alpha2 + Comp(color1*alpha1, alpha2))/alpha3;
        }
    }
}