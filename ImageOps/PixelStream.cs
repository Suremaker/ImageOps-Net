using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ImageOps
{
	public abstract class PixelStream : IPixelStream
	{
		#region IPixelStream Members

		public void Seek(int position, SeekOrigin origin)
		{
			int delta = GetMoveByDelta(position, origin);
			if (Position + delta < 0 || Position + delta > TotalLength)
				throw new ArgumentOutOfRangeException();
			MoveBy(delta);
			Position += delta;
		}

		public int Position { get; private set; }
		public abstract int ImageWidth { get; }
		public abstract int ImageHeight { get; }

		public bool IsEnd
		{
			get { return Position >= TotalLength; }
		}

		public PixelColor Read()
		{
			if (IsEnd)
				throw new EndOfStreamException();
			PixelColor color = ReadPixel();
			Seek(1, SeekOrigin.Current);
			return color;
		}

		public int TotalLength
		{
			get { return ImageWidth * ImageHeight; }
		}

		public abstract void Dispose();

		public IEnumerator<PixelColor> GetEnumerator()
		{
			while (!IsEnd)
				yield return Read();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		protected abstract void MoveBy(int i);
		protected abstract PixelColor ReadPixel();

		private int GetMoveByDelta(int position, SeekOrigin origin)
		{
			switch (origin)
			{
				case SeekOrigin.Begin:
					return position - Position;
				case SeekOrigin.End:
					return TotalLength - 1 - Position + position;
				default:
					return position;
			}
		}
	}
}