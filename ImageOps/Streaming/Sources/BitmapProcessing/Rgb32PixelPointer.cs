using System.Drawing.Imaging;

namespace ImageOps.Streaming.Sources.BitmapProcessing
{
	internal class Rgb32PixelPointer : IPixelPointer
	{
		private unsafe uint* _pointer;

		public unsafe Rgb32PixelPointer(BitmapData data)
		{
			_pointer = (uint*)data.Scan0.ToPointer();
		}

		public unsafe PixelColor Get()
		{
			return new PixelColor(0xff000000 | *_pointer);
		}

		public unsafe void MoveBy(int i)
		{
			_pointer += i;
		}
	}
}