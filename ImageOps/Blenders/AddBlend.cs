using ImageOps.Sources;
using ImageOps.Sources.Readers;

namespace ImageOps.Blenders
{
    public class AddBlend : IBlendingMethod
    {
        public PixelColor Blend(PixelColor background, PixelColor foreground)
        {
            return (background.A == 0)
                ? background
                : Add(background, foreground);
        }

        public IPixelReader OpenBlendingReader(IPixelSource background, IPixelSource foregorund)
        {
            return new AddBlendReader(background, foregorund);
        }

        private static PixelColor Add(PixelColor back, PixelColor front)
        {
            if (front.A == 0)
                return back;
            int ratio = Discrete.CalcAlphaRatio(back.A, front.A);
            return new PixelColor(
                back.A,
                Add(back.R, front.R, ratio),
                Add(back.G, front.G, ratio),
                Add(back.B, front.B, ratio));
        }

        private static byte Add(int backColor, int frontColor, int ratio)
        {
            return Discrete.BlendWithRatio(backColor, Add(backColor, frontColor), ratio);
        }

        private static int Add(int backColor, int frontColor)
        {
            return Discrete.Clamp(backColor + frontColor);
        }

        private class AddBlendReader : BlendingReader
        {
            public AddBlendReader(IPixelSource background, IPixelSource foregorund) : base(background, foregorund) { }
            public override PixelColor VerifiedGet(int x, int y)
            {
                var back = BackgroundReader.VerifiedGet(x, y);
                if (back.A == 0)
                    return back;
                return Add(back, ForegroundReader.VerifiedGet(x, y));
            }
        }
    }
}