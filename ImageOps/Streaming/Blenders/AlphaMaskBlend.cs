using System;

namespace ImageOps.Streaming.Blenders
{
	public class AlphaMaskBlend : BlendingStream
	{
		private readonly Func<PixelColor, float> _alphaSelector;

		public AlphaMaskBlend(IPixelStream source, IPixelStream mask, ColorChannel maskChannel)
			: base(source, mask)
		{
			_alphaSelector = CreateSelector(maskChannel);
		}

		private Func<PixelColor, float> CreateSelector(ColorChannel maskChannel)
		{
			switch (maskChannel)
			{
				case ColorChannel.Red:
					return c => c.GetRed();
				case ColorChannel.Green:
					return c => c.GetGreen();
				case ColorChannel.Blue:
					return c => c.GetBlue();
				default:
					return c => c.GetAlpha();
			}
		}

		protected override PixelColor Blend(PixelColor source, PixelColor mask)
		{
			float alpha = CalculateAlpha(source.GetAlpha(), _alphaSelector(mask));
			return PixelColor.FromFargb(alpha, source.GetRed(), source.GetGreen(), source.GetBlue());
		}

		private float CalculateAlpha(float sourceAlpha, float maskAlpha)
		{
			return sourceAlpha * maskAlpha;
		}
	}
}
