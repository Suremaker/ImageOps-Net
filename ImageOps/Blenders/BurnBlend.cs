using ImageOps.Sources;
using ImageOps.Sources.Readers;

namespace ImageOps.Blenders
{
    public class BurnBlend : IBlendingMethod
    {
        public PixelColor Blend(PixelColor background, PixelColor foreground)
        {
            if (background.A == 0)
                return background;
            return Burn(background, foreground);
        }

        public IPixelReader OpenBlendingReader(IPixelSource background, IPixelSource foregorund)
        {
            return new BurnBlendReader(background, foregorund);
        }

        private static PixelColor Burn(PixelColor back, PixelColor front)
        {
            if (front.A == 0)
                return back;

            int ratio = Discrete.CalcAlphaRatio(back.A, front.A);
            return new PixelColor(
                back.A,
                Burn(back.R, front.R, ratio),
                Burn(back.G, front.G, ratio),
                Burn(back.B, front.B, ratio));
        }

        private static byte Burn(int backColor, int frontColor, int ratio)
        {
            return Discrete.BlendWithRatio(backColor, Burn(backColor, frontColor), ratio);
        }

        private static int Burn(int backColor, int frontColor)
        {
            if (frontColor == 0)
                return 0;
            return Discrete.MaxColor - Discrete.Clamp(((Discrete.MaxColor - backColor) * Discrete.MaxColor) / frontColor);
        }

        private class BurnBlendReader : BlendingReader
        {
            public BurnBlendReader(IPixelSource background, IPixelSource foregorund) : base(background, foregorund) { }
            public override PixelColor VerifiedGet(int x, int y)
            {
                var back = BackgroundReader.VerifiedGet(x, y);
                if (back.A == 0)
                    return back;

                return Burn(back, ForegroundReader.VerifiedGet(x, y));
            }
        }
    }
}