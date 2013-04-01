using System;
using System.IO;

namespace ImageOps
{
	public abstract class SourceStream : PixelStream
	{
		private int _position;
		public override int Position { get { return _position; } }

		public override PixelColor Read()
		{
			if (IsEnd)
				throw new EndOfStreamException();
			var color = ReadPixel();
			Seek(1, SeekOrigin.Current);
			return color;
		}

		public override void Seek(int position, SeekOrigin origin)
		{
			int delta = GetMoveByDelta(position, origin);
			if (_position + delta < 0 || _position + delta > TotalLength)
				throw new ArgumentOutOfRangeException();
			MoveBy(delta);
			_position += delta;
		}

		protected abstract void MoveBy(int i);
		protected abstract PixelColor ReadPixel();

		private int GetMoveByDelta(int position, SeekOrigin origin)
		{
			switch (origin)
			{
				case SeekOrigin.Begin:
					return position - _position;
				case SeekOrigin.End:
					return TotalLength - 1 - _position + position;
				default:
					return position;
			}
		}
	}
}