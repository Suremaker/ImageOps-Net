namespace ImageOps.Blenders
{
    public class AlphaMaskBlend : IBlendingMethod
    {
        public PixelColor Blend(PixelColor source, PixelColor mask)
        {
            return new PixelColor((byte)Discrete.MulRatio(source.A, mask.A), source.R, source.G, source.B);
        }
    }

    public class RedMaskBlend : IBlendingMethod
    {
        public PixelColor Blend(PixelColor source, PixelColor mask)
        {
            return new PixelColor((byte)Discrete.MulRatio(source.A, mask.R), source.R, source.G, source.B);
        }
    }

    public class GreenMaskBlend : IBlendingMethod
    {
        public PixelColor Blend(PixelColor source, PixelColor mask)
        {
            return new PixelColor((byte)Discrete.MulRatio(source.A, mask.G), source.R, source.G, source.B);
        }
    }

    public class BlueMaskBlend : IBlendingMethod
    {
        public PixelColor Blend(PixelColor source, PixelColor mask)
        {
            return new PixelColor((byte)Discrete.MulRatio(source.A, mask.B), source.R, source.G, source.B);
        }
    }
}