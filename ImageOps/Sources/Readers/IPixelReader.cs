using System;
using System.Collections.Generic;

namespace ImageOps.Sources.Readers
{
    public interface IPixelReader : IDisposable, IEnumerable<PixelColor>
    {
        PixelColor Get(int x, int y);
        int Width { get; }
        int Height { get; }
    }
}