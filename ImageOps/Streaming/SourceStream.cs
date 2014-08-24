using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ImageOps.Streaming
{
    public abstract class SourceStream : PixelStream
    {
        private int _position;
        public override int Position { get { return _position; } }

        public override PixelColor GetCurrent()
        {
            if (IsEnd)
                throw new EndOfStreamException();
            return GetCurrentPixel();
        }

        public override void Move(int delta)
        {
            if (_position + delta < 0 || _position + delta > TotalLength)
                throw new ArgumentOutOfRangeException();
            MoveBy(delta);
            _position += delta;
        }

        protected abstract void MoveBy(int i);
        protected abstract PixelColor GetCurrentPixel();
    }

    public abstract class SourceStream2<TSource> : IPixelStream2 where TSource : IPixelSource
    {
        protected TSource Source { get; private set; }

        protected SourceStream2(TSource source)
        {
            Source = source;
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
        public int Length { get { return Source.ImageWidth * Source.ImageHeight; } }
        public bool IsEnd { get { return Position == Length; } }
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