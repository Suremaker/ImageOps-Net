using ImageOps.Sources;
using ImageOps.Sources.Readers;

namespace ImageOps.Blenders
{
    public class AlphaMaskBlend : IBlendingMethod
    {
        public PixelColor Blend(PixelColor background, PixelColor foreground)
        {
            return Mask(background, foreground);
        }

        private static PixelColor Mask(PixelColor source, PixelColor mask)
        {
            return new PixelColor((byte)Discrete.MulRatio(source.A, mask.A), source.R, source.G, source.B);
        }

        public IPixelReader OpenBlendingReader(IPixelSource background, IPixelSource foregorund)
        {
            return new AlphaMaskBlendReader(background, foregorund);
        }

        private class AlphaMaskBlendReader : BlendingReader
        {
            public AlphaMaskBlendReader(IPixelSource background, IPixelSource foregorund) : base(background, foregorund) { }
            public override PixelColor VerifiedGet(int x, int y)
            {
                var mask = ForegroundReader.VerifiedGet(x, y);
                if (mask.A == 0)
                    return PixelColor.Transparent;
                return Mask(BackgroundReader.VerifiedGet(x, y), mask);
            }
        }
    }

    public class RedMaskBlend : IBlendingMethod
    {
        public PixelColor Blend(PixelColor background, PixelColor foreground)
        {
            return Mask(background, foreground);
        }

        public IPixelReader OpenBlendingReader(IPixelSource background, IPixelSource foregorund)
        {
            return new RedMaskBlendReader(background, foregorund);
        }

        private static PixelColor Mask(PixelColor source, PixelColor mask)
        {
            return new PixelColor((byte)Discrete.MulRatio(source.A, mask.R), source.R, source.G, source.B);
        }

        private class RedMaskBlendReader : BlendingReader
        {
            public RedMaskBlendReader(IPixelSource background, IPixelSource foregorund) : base(background, foregorund) { }
            public override PixelColor VerifiedGet(int x, int y)
            {
                var mask = ForegroundReader.VerifiedGet(x, y);
                if (mask.R == 0)
                    return PixelColor.Transparent;

                return Mask(BackgroundReader.VerifiedGet(x, y), mask);
            }


        }
    }

    public class GreenMaskBlend : IBlendingMethod
    {
        public PixelColor Blend(PixelColor background, PixelColor foreground)
        {
            return Mask(background, foreground);
        }

        public IPixelReader OpenBlendingReader(IPixelSource background, IPixelSource foregorund)
        {
            return new GreenMaskBlendReader(background, foregorund);
        }

        private static PixelColor Mask(PixelColor source, PixelColor mask)
        {
            return new PixelColor((byte)Discrete.MulRatio(source.A, mask.G), source.R, source.G, source.B);
        }

        private class GreenMaskBlendReader : BlendingReader
        {
            public GreenMaskBlendReader(IPixelSource background, IPixelSource foregorund) : base(background, foregorund) { }
            public override PixelColor VerifiedGet(int x, int y)
            {
                var mask = ForegroundReader.VerifiedGet(x, y);
                if (mask.G == 0)
                    return PixelColor.Transparent;

                return Mask(BackgroundReader.VerifiedGet(x, y), mask);
            }
        }
    }

    public class BlueMaskBlend : IBlendingMethod
    {
        public PixelColor Blend(PixelColor background, PixelColor foreground)
        {
            return Mask(background, foreground);
        }
        public IPixelReader OpenBlendingReader(IPixelSource background, IPixelSource foregorund)
        {
            return new BlueMaskBlendReader(background, foregorund);
        }
        private static PixelColor Mask(PixelColor source, PixelColor mask)
        {
            return new PixelColor((byte)Discrete.MulRatio(source.A, mask.B), source.R, source.G, source.B);
        }

        private class BlueMaskBlendReader : BlendingReader
        {
            public BlueMaskBlendReader(IPixelSource background, IPixelSource foregorund) : base(background, foregorund) { }
            public override PixelColor VerifiedGet(int x, int y)
            {
                var mask = ForegroundReader.VerifiedGet(x, y);
                if (mask.B == 0)
                    return PixelColor.Transparent;

                return Mask(BackgroundReader.VerifiedGet(x, y), mask);
            }
        }
    }
}