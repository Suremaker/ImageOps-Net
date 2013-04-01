using System.Drawing;
using System.Drawing.Imaging;

namespace ImageOps
{
	public class BitmapStream : PixelStream
	{
		private readonly Bitmap _bitmap;
		private readonly BitmapData _bitmapData;
		private bool _disposed;
		private readonly IPixelPointer _pixelPointer;

		public BitmapStream(Bitmap bitmap)
		{
			_bitmap = bitmap;
			_bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
			_pixelPointer = new ArgbPixelPointer(_bitmapData.Scan0);
		}

		protected override void MoveBy(int i)
		{
			_pixelPointer.MoveBy(i);
		}

		public override int ImageWidth
		{
			get { return _bitmapData.Width; }
		}

		public override int ImageHeight
		{
			get { return _bitmapData.Height; }
		}

		protected override PixelColor ReadPixel()
		{
			return _pixelPointer.Get();
		}

		public override void Dispose()
		{
			if (!_disposed)
				_bitmap.UnlockBits(_bitmapData);
			_disposed = true;
		}
	}
}
