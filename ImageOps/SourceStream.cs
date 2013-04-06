using System;
using System.IO;

namespace ImageOps
{
	public abstract class SourceStream : PixelStream
	{
		private int _position;
		public override int Position { get { return _position; } }

		public override PixelColor GetCurrent()
		{
			if (IsEnd)
				throw new EndOfStreamException();
			return GetCurrentPixel();
		}

		public override void Move(int delta)
		{
			if (_position + delta < 0 || _position + delta > TotalLength)
				throw new ArgumentOutOfRangeException();
			MoveBy(delta);
			_position += delta;
		}

		protected abstract void MoveBy(int i);
		protected abstract PixelColor GetCurrentPixel();
	}
}