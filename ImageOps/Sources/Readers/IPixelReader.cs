using System;

namespace ImageOps.Sources.Readers
{
    public interface IPixelReader : IDisposable
    {
        /// <summary>
        /// Retrieves pixel color located at position (x,y).
        /// If specified position is out of source bounds, a ArgumentOutOfRangeException would be thrown
        /// </summary>
        PixelColor Get(int x, int y);
        int Width { get; }
        int Height { get; }
    }
}