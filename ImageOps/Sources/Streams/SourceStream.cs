using System;
using System.Collections;
using System.Collections.Generic;

namespace ImageOps.Sources.Streams
{
    public abstract class SourceStream<TSource> : IPixelStream where TSource : IPixelSource
    {
        protected TSource Source { get; private set; }

        protected SourceStream(TSource source)
        {
            Source = source;
            Length = Source.ImageWidth * Source.ImageHeight;
        }

        public abstract void Dispose();

        public void Move(int delta)
        {
            if (Position + delta < 0 || Position + delta > Length)
                throw new ArgumentOutOfRangeException();
            MoveBy(delta);
            Position += delta;
        }

        public abstract void MoveBy(int delta);

        public int Position { get; private set; }

        public int Length { get; private set; }

        public bool IsEnd
        {
            get { return Position == Length; }
        }

        public abstract PixelColor GetCurrent();

        public IEnumerator<PixelColor> GetEnumerator()
        {
            while (!IsEnd)
            {
                yield return GetCurrent();
                Move(1);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}