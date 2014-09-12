using ImageOps.Sources;
using ImageOps.Sources.Readers;

namespace ImageOps.Blenders
{
    public class GrainMergeBlend : IBlendingMethod
    {
        public PixelColor Blend(PixelColor background, PixelColor foreground)
        {
            if (background.A == 0)
                return background;
            return GrainMerge(background, foreground);
        }

        public IPixelReader OpenBlendingReader(IPixelSource background, IPixelSource foregorund)
        {
            return new GrainMergeBlendReader(background, foregorund);
        }

        private static PixelColor GrainMerge(PixelColor back, PixelColor front)
        {
            if (front.A == 0)
                return back;

            int ratio = Discrete.CalcAlphaRatio(back.A, front.A);
            return new PixelColor(
                back.A,
                GrainMerge(back.R, front.R, ratio),
                GrainMerge(back.G, front.G, ratio),
                GrainMerge(back.B, front.B, ratio));
        }

        private static byte GrainMerge(int backColor, int frontColor, int ratio)
        {
            return Discrete.BlendWithRatio(backColor, GrainMerge(backColor, frontColor), ratio);
        }

        private static int GrainMerge(int backColor, int frontColor)
        {
            return Discrete.Clamp(backColor + frontColor - 128);
        }

        private class GrainMergeBlendReader : BlendingReader
        {
            public GrainMergeBlendReader(IPixelSource background, IPixelSource foregorund) : base(background, foregorund) { }
            public override PixelColor VerifiedGet(int x, int y)
            {
                var back = BackgroundReader.VerifiedGet(x, y);
                if (back.A == 0)
                    return back;

                PixelColor front = ForegroundReader.VerifiedGet(x, y);
                return GrainMerge(back, front);
            }
        }
    }
}