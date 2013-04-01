using System;
using System.Drawing;
using System.Drawing.Imaging;
using ImageOps.BitmapProcessing;

namespace ImageOps.Sources
{
	public class BitmapSource : SourceStream
	{
		private readonly Bitmap _bitmap;
		private readonly BitmapData _bitmapData;
		private bool _disposed;
		private readonly IPixelPointer _pixelPointer;

		public BitmapSource(Bitmap bitmap)
		{
			_bitmap = bitmap;
			_bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
			_pixelPointer = CreatePixelPointer();
		}

		private IPixelPointer CreatePixelPointer()
		{
			switch (_bitmap.PixelFormat)
			{
				case PixelFormat.Format32bppArgb:
					return new Argb32PixelPointer(_bitmapData);
				case PixelFormat.Format32bppRgb:
					return new Rgb32PixelPointer(_bitmapData);
				case PixelFormat.Format24bppRgb:
					return new Rgb24PixelPointer(_bitmapData);
				default:
					throw new NotSupportedException(string.Format("Pixel format {0} is not supported. Use 32bppArgb, 32bppRgb or 24bppRgb.", _bitmap.PixelFormat));
			}
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
