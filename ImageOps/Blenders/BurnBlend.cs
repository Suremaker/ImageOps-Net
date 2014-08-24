namespace ImageOps.Blenders
{
    public class BurnBlend : StandardBlend
    {
        protected override float Blend(float color1, float color2)
        {
            return Clamp(1.0f - ((1.0f - color1)/color2));
        }
    }
}