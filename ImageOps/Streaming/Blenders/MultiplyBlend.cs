using System;

namespace ImageOps.Streaming.Blenders
{
	/// <summary>
	/// Multiply blend algorithm bases on GIMP multiply mode
	/// </summary>
	public class MultiplyBlend : BlendingStream
	{
		public MultiplyBlend(IPixelStream back, IPixelStream front)
			: base(back, front)
		{
		}

		protected override PixelColor Blend(PixelColor back, PixelColor front)
		{
			float ratio = CalcRatio(back.GetAlpha(), front.GetAlpha());

			if (ratio == 0.0f)
				return PixelColor.FromArgb(0, 0, 0, 0);

			return PixelColor.FromFargb(
				back.GetAlpha(),
				Blend(back.GetRed(), front.GetRed(), ratio),
				Blend(back.GetGreen(), front.GetGreen(), ratio),
				Blend(back.GetBlue(), front.GetBlue(), ratio));
		}

		private float Blend(float color1, float color2, float ratio)
		{
			return Clamp(color1 * color2) * ratio + Comp(color1, ratio);
		}

		private float CalcRatio(float backAlpha, float frontAlpha)
		{
			var minAlpha = Math.Min(backAlpha, frontAlpha);
			var newAlpha = backAlpha + Comp(minAlpha, backAlpha);
			return newAlpha != 0.0f ? minAlpha / newAlpha : 0;
		}
	}
}