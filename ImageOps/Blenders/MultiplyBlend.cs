namespace ImageOps.Blenders
{
    /// <summary>
    /// Multiply blend algorithm basing on GIMP multiply mode
    /// </summary>
    public class MultiplyBlend : StandardBlend
    {
        protected override double Blend(double color1, double color2)
        {
            return color1*color2;
        }
    }
}