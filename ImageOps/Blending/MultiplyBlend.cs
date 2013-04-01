namespace ImageOps.Blending
{
	public class MultiplyBlend : BlendingStream
	{
		public MultiplyBlend(IPixelStream back, IPixelStream front)
			: base(back, front)
		{
		}
		protected override float Blend(float color1, float color2, float alpha1, float alpha2)
		{
			return color1 * alpha1 * color2 * alpha2 + Comp(color2 * alpha2, alpha1) + Comp(color1 * alpha1, alpha2);
		}
	}
}