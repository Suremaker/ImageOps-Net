using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace ImageOps
{
	public abstract class PixelStream : IPixelStream
	{
		#region IPixelStream Members

		public abstract void Seek(int position, SeekOrigin origin);
		public abstract int Position { get; }
		public abstract int ImageWidth { get; }
		public abstract int ImageHeight { get; }
		public bool IsEnd { get { return Position >= TotalLength; } }

		public abstract PixelColor Read();

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
	}
}