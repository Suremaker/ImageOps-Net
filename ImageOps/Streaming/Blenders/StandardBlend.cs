using System;

namespace ImageOps.Streaming.Blenders
{
	/// <summary>
	/// Generic blend algorithm basing on GIMP multiply mode
	/// </summary>
	public abstract class StandardBlend : BlendingStream
	{
		protected StandardBlend(IPixelStream back, IPixelStream front)
			: base(back, front)
		{
		}

	    public override PixelColor Blend(PixelColor back, PixelColor front)
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
			return Clamp(Blend(color1, color2)) * ratio + Comp(color1, ratio);
		}

		protected abstract float Blend(float color1, float color2);

		private float CalcRatio(float backAlpha, float frontAlpha)
		{
			var minAlpha = Math.Min(backAlpha, frontAlpha);
			var newAlpha = backAlpha + Comp(minAlpha, backAlpha);
			return newAlpha != 0.0f ? minAlpha / newAlpha : 0;
		}
	}

    public interface IBlendingMethod
    {
        PixelColor Blend(PixelColor back, PixelColor front);
    }
}
