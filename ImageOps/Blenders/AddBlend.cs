namespace ImageOps.Blenders
{
    public class AddBlend : StandardBlend
    {
        protected override float Blend(float color1, float color2)
        {
            return color1 + color2;
        }
    }
}