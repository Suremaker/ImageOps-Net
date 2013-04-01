using System.Drawing.Imaging;

namespace ImageOps.BitmapProcessing
{
	internal class Rgb24PixelPointer : IPixelPointer
	{
		private readonly unsafe byte* _pointer;
		private readonly BitmapData _data;
		private int _pixelIndex;

		public unsafe Rgb24PixelPointer(BitmapData data)
		{
			_data = data;
			_pointer = (byte*)data.Scan0.ToPointer();
		}

		public unsafe PixelColor Get()
		{
			var index = GetPointerIndex();
			return PixelColor.FromRgb(_pointer[index + 2], _pointer[index + 1], _pointer[index + 0]);
		}

		private int GetPointerIndex()
		{
			int x = _pixelIndex % _data.Width;
			int y = _pixelIndex / _data.Width;
			return (_data.Stride * y) + x * 3;
		}

		public void MoveBy(int i)
		{
			_pixelIndex += i;
		}
	}
}