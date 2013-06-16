using System;

namespace ImageOps.Streaming.Blenders
{
	/// <summary>
	/// Multiply blend algorithm basing on GIMP multiply mode
	/// </summary>
	public class MultiplyBlend : StandardBlend
	{
		public MultiplyBlend(IPixelStream back, IPixelStream front)
			: base(back, front)
		{
		}

		protected override float Blend(float color1, float color2)
		{
			return color1 * color2;
		}
	}
}