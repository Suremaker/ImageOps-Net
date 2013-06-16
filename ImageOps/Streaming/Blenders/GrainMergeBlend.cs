namespace ImageOps.Streaming.Blenders
{
	public class GrainMergeBlend : StandardBlend
	{
		public GrainMergeBlend(IPixelStream back, IPixelStream front)
			: base(back, front)
		{
		}

		protected override float Blend(float color1, float color2)
		{
			return color1 + color2 - 0.5f;
		}
	}
}
