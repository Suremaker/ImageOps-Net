namespace ImageOps.Blenders
{
    public interface IBlendingMethod
    {
        PixelColor Blend(PixelColor back, PixelColor front);
    }
}