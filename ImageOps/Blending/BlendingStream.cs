using System;
using System.IO;

namespace ImageOps.Blending
{
	public abstract class BlendingStream : PixelStream
	{
		protected readonly IPixelStream Back;
		protected readonly IPixelStream Front;

		public override int ImageHeight
		{
			get { return Back.ImageHeight; }
		}

		public override int ImageWidth
		{
			get { return Back.ImageWidth; }
		}

		public override int Position
		{
			get { return Back.Position; }
		}

		protected BlendingStream(IPixelStream back, IPixelStream front)
		{
			if (back.ImageWidth != front.ImageWidth || back.ImageHeight != front.ImageHeight)
				throw new ArgumentException("Front and back pixel source image sizes have to match.");
			Back = back;
			Front = front;
		}

		public override void Dispose()
		{
			Back.Dispose();
			Front.Dispose();
		}

		public override PixelColor Read()
		{
			return Blend(Back.Read(), Front.Read());
		}

		public override void Seek(int position, SeekOrigin origin)
		{
			Back.Seek(position, origin);
			Front.Seek(position, origin);
		}

		protected abstract PixelColor Blend(PixelColor back, PixelColor front);

		protected static float Comp(float color, float alpha)
		{
			return color * (1 - alpha);
		}

		protected static float Clamp(float value)
		{
			return Math.Min(1, Math.Max(0, value));
		}
	}
}