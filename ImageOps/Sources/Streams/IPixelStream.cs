using System;
using System.Collections.Generic;

namespace ImageOps.Sources.Streams
{
    public interface IPixelStream : IDisposable, IEnumerable<PixelColor>
    {
        PixelColor Get(int x, int y);
        int Width { get; }
        int Height { get; }
    }
}