using System;

namespace ImageOps.Sources.Readers
{
    public interface IVerifiedPixelReader : IDisposable
    {
        /// <summary>
        /// Retrieves pixel color located at position (x,y).
        /// The caller of this function guarantees that position is within source bounds, so additional validation is not required.
        /// If specified position would be out of bounds, an effect is unspecified.
        /// </summary>
        PixelColor VerifiedGet(int x, int y);
        int Width { get; }
        int Height { get; }
    }
}