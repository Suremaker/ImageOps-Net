using System.Collections;
using System.Collections.Generic;

namespace ImageOps.Streaming
{
	public abstract class PixelStream : IPixelStream
	{
		#region IPixelStream Members

		public abstract void Move(int delta);
		public abstract int Position { get; }
		public abstract int ImageWidth { get; }
		public abstract int ImageHeight { get; }
		public bool IsEnd { get { return Position >= TotalLength; } }

		public abstract PixelColor GetCurrent();

		public int TotalLength
		{
			get { return ImageWidth * ImageHeight; }
		}

		public abstract void Dispose();

		public IEnumerator<PixelColor> GetEnumerator()
		{
			while (!IsEnd)
			{
				yield return GetCurrent();
				Move(1);
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}