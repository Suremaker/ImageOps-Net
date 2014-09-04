namespace ImageOps.Blenders
{
    public class GrainMergeBlend : StandardBlend
    {
        protected override double Blend(double color1, double color2)
        {
            return color1 + color2 - 0.5;
        }
    }
}