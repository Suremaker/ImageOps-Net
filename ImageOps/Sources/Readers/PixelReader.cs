using System;

namespace ImageOps.Sources.Readers
{
    public abstract class PixelReader : IPixelReader, IVerifiedPixelReader
    {
        protected PixelReader(int width, int height)
        {
            Height = height;
            Width = width;
        }

        public PixelColor Get(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                throw new ArgumentOutOfRangeException();
            return VerifiedGet(x, y);
        }

        public abstract PixelColor VerifiedGet(int x, int y);

        public int Width { get; private set; }
        public int Height { get; private set; }
        public virtual void Dispose() { }
    }
}