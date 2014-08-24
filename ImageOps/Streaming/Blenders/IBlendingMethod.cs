namespace ImageOps.Streaming.Blenders
{
    public interface IBlendingMethod
    {
        PixelColor Blend(PixelColor back, PixelColor front);
    }
}