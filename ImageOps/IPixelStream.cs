using System;
using System.Collections.Generic;
using System.IO;

namespace ImageOps
{
	public interface IPixelStream : IDisposable, IEnumerable<PixelColor>
	{
		void Seek(int position, SeekOrigin origin);
		int Position { get; }
		int TotalLength { get; }

		int ImageWidth { get; }
		int ImageHeight { get; }

		bool IsEnd { get; }

		PixelColor Read();
	}
}
