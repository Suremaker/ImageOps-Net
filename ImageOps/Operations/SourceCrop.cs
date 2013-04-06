using System;
using System.Drawing;

namespace ImageOps.Operations
{
	public class SourceCrop : SourceStream
	{
		private readonly IPixelStream _stream;
		private readonly Rectangle _rectangle;
		private readonly int _baseOffset;
		private readonly int _skippedPixels;

		public SourceCrop(IPixelStream stream, Rectangle rectangle)
		{
			if (rectangle.Width <= 0 || rectangle.Height <= 0)
				throw new ArgumentException("Cropped image width and height has to be > 0");

			if (rectangle.X < 0 || rectangle.Y < 0 || rectangle.X + rectangle.Width > stream.ImageWidth || rectangle.Y + rectangle.Height > stream.ImageHeight)
				throw new ArgumentException("Cropped dimensions cannot expand over source dimensions");

			_stream = stream;
			_rectangle = rectangle;
			_baseOffset = _rectangle.Y * _stream.ImageWidth + _rectangle.X;
			_skippedPixels = _stream.ImageWidth - _rectangle.Width;
			_stream.Move(_baseOffset);
		}

		protected override void MoveBy(int i)
		{
			var cropPos = i + Position;
			var expectedPos = _baseOffset + cropPos + (cropPos / _rectangle.Width) * _skippedPixels;
			_stream.Move(expectedPos - _stream.Position);
		}

		protected override PixelColor GetCurrentPixel()
		{
			return _stream.GetCurrent();
		}

		public override int ImageWidth
		{
			get { return _rectangle.Width; }
		}

		public override int ImageHeight
		{
			get { return _rectangle.Height; }
		}

		public override void Dispose()
		{
			_stream.Dispose();
		}
	}
}