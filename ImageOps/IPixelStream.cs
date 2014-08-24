using System;
using System.Collections.Generic;

namespace ImageOps
{
    public interface IPixelStream : IDisposable, IEnumerable<PixelColor>
    {
        void Move(int delta);
        int Position { get; }
        int TotalLength { get; }

        int ImageWidth { get; }
        int ImageHeight { get; }

        bool IsEnd { get; }

        PixelColor GetCurrent();
    }

    public interface IPixelStream2 : IDisposable, IEnumerable<PixelColor>
    {
        void Move(int delta);
        int Position { get; }
        int Length { get; }
        bool IsEnd { get; }
        PixelColor GetCurrent();
    }

    public interface IPixelSource : IDisposable
    {
        int ImageWidth { get; }
        int ImageHeight { get; }
        IPixelStream2 OpenStream();
    }
}
