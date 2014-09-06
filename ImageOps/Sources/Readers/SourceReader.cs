using System;

namespace ImageOps.Sources.Readers
{
    public abstract class SourceReader<TSource> : IPixelReader, IVerifiedPixelReader where TSource : IPixelSource
    {
        protected TSource Source { get; private set; }

        protected SourceReader(TSource source)
        {
            Source = source;
            Width = Source.ImageWidth;
            Height = Source.ImageHeight;
        }

        public virtual void Dispose() { }

        public PixelColor Get(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                throw new ArgumentOutOfRangeException();
            return VerifiedGet(x, y);
        }

        public abstract PixelColor VerifiedGet(int x, int y);

        public int Width { get; private set; }
        public int Height { get; private set; }
    }
}