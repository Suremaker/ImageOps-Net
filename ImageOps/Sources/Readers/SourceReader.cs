using System;
using System.Collections;
using System.Collections.Generic;

namespace ImageOps.Sources.Readers
{
    public abstract class SourceReader<TSource> : IPixelReader where TSource : IPixelSource
    {
        protected TSource Source { get; private set; }

        protected SourceReader(TSource source)
        {
            Source = source;
            Width = Source.ImageWidth;
            Height = Source.ImageHeight;
        }

        public virtual void Dispose() { }

        public IEnumerator<PixelColor> GetEnumerator()
        {
            for (int y = 0; y < Height; y += 1)
                for (int x = 0; x < Width; x += 1)
                    yield return Get(x, y);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public PixelColor Get(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                throw new ArgumentOutOfRangeException();
            return FastGet(x, y);
        }

        protected abstract PixelColor FastGet(int x, int y);

        public int Width { get; private set; }
        public int Height { get; private set; }
    }
}