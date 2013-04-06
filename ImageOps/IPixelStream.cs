using System;
using System.Collections.Generic;

namespace ImageOps
{
	public interface IPixelStream : IDisposable, IEnumerable<PixelColor>
	{
		void Move(int delta);
		int Position { get; }
		int TotalLength { get; }

		int ImageWidth { get; }
		int ImageHeight { get; }

		bool IsEnd { get; }

		PixelColor GetCurrent();
	}
}
