using ImageOps.Sources;
using ImageOps.Sources.Readers;

namespace ImageOps.Blenders
{
    public class NormalBlend : IBlendingMethod
    {
        public PixelColor Blend(PixelColor background, PixelColor foreground)
        {
            if (foreground.A == Discrete.MaxColor)
                return foreground;
            return Mix(background, foreground);
        }

        public IPixelReader OpenBlendingReader(IPixelSource background, IPixelSource foregorund)
        {
            return new NormalBlendReader(background, foregorund);
        }
        private static PixelColor Mix(PixelColor back, PixelColor front)
        {
            if (front.A == 0)
                return back;

            var outAlpha = (byte)(back.A + front.A - Discrete.MulRatio(back.A, front.A));

            return new PixelColor(
                outAlpha,
                Mix(back.R, front.R, back.A, front.A, outAlpha),
                Mix(back.G, front.G, back.A, front.A, outAlpha),
                Mix(back.B, front.B, back.A, front.A, outAlpha));
        }

        private static byte Mix(int backColor, int frontColor, int backAlpha, int frontAlpha, int outAlpha)
        {
            return (byte)((Discrete.MulRatio(frontColor, frontAlpha) + Discrete.CompRatio(Discrete.MulRatio(backColor, backAlpha), frontAlpha)) * Discrete.MaxColor / outAlpha);
        }
        private class NormalBlendReader : BlendingReader
        {
            public NormalBlendReader(IPixelSource background, IPixelSource foreground) : base(background, foreground) { }

            public override PixelColor VerifiedGet(int x, int y)
            {
                var front = ForegroundReader.VerifiedGet(x, y);
                if (front.A == Discrete.MaxColor)
                    return front;
                return Mix(BackgroundReader.VerifiedGet(x, y), front);
            }
        }
    }
}