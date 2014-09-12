using ImageOps.Sources;
using ImageOps.Sources.Readers;

namespace ImageOps.Blenders
{
    /// <summary>
    /// Multiply blend algorithm basing on GIMP multiply mode
    /// </summary>
    public class MultiplyBlend : IBlendingMethod
    {
        public PixelColor Blend(PixelColor background, PixelColor foreground)
        {
            if (background.A == 0)
                return background;
            return Multiply(background, foreground);
        }

        public IPixelReader OpenBlendingReader(IPixelSource background, IPixelSource foregorund)
        {
            return new MultiplyBlendReader(background, foregorund);
        }

        private static PixelColor Multiply(PixelColor back, PixelColor front)
        {
            if (front.A == 0)
                return new PixelColor(back.Argb);

            int ratio = Discrete.CalcAlphaRatio(back.A, front.A);
            return new PixelColor(
                back.A,
                Multiply(back.R, front.R, ratio),
                Multiply(back.G, front.G, ratio),
                Multiply(back.B, front.B, ratio));
        }

        private static byte Multiply(int backColor, int frontColor, int ratio)
        {
            return Discrete.BlendWithRatio(backColor, Multiply(backColor, frontColor), ratio);
        }

        private static int Multiply(int backColor, int frontColor)
        {
            return Discrete.DivBy255(backColor * frontColor);
        }

        private class MultiplyBlendReader : BlendingReader
        {
            public MultiplyBlendReader(IPixelSource background, IPixelSource foregorund) : base(background, foregorund) { }

            public override PixelColor VerifiedGet(int x, int y)
            {
                var back = BackgroundReader.VerifiedGet(x, y);
                if (back.A == 0)
                    return back;

                return Multiply(back, ForegroundReader.VerifiedGet(x, y));
            }
        }
    }
}