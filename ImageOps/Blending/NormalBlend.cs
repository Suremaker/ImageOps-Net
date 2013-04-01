namespace ImageOps.Blending
{
	public class NormalBlend : BlendingStream
	{
		public NormalBlend(IPixelStream back, IPixelStream front)
			: base(back, front)
		{
		}
		protected override float Blend(float color1, float color2, float alpha1, float alpha2)
		{
			return color2 * alpha2 + Comp(color1 * alpha1, alpha2);
		}
	}
}
