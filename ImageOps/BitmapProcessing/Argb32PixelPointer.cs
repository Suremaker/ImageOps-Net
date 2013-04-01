using System.Drawing.Imaging;

namespace ImageOps.BitmapProcessing
{
	public class Argb32PixelPointer : IPixelPointer
	{
		private unsafe uint* _pointer;

		public unsafe Argb32PixelPointer(BitmapData data)
		{
			_pointer = (uint*)data.Scan0.ToPointer();
		}

		public unsafe PixelColor Get()
		{
			return new PixelColor(*_pointer);
		}

		public unsafe void MoveBy(int i)
		{
			_pointer += i;
		}
	}
}