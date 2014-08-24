using System;
using System.Collections.Generic;

namespace ImageOps.Sources.Streams
{
    public interface IPixelStream : IDisposable, IEnumerable<PixelColor>
    {
        void Move(int delta);
        int Position { get; }
        int Length { get; }
        bool IsEnd { get; }
        PixelColor GetCurrent();
    }
}