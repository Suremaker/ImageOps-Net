﻿namespace ImageOps.Streaming.Blenders
{
	public class AddBlend : StandardBlend
	{
		public AddBlend(IPixelStream back, IPixelStream front)
			: base(back, front)
		{
		}

		protected override float Blend(float color1, float color2)
		{
			return color1 + color2;
		}
	}
}