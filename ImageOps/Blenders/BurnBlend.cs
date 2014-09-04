namespace ImageOps.Blenders
{
    public class BurnBlend : StandardBlend
    {
        protected override double Blend(double color1, double color2)
        {
            return Clamp(1.0 - ((1.0 - color1)/color2));
        }
    }
}