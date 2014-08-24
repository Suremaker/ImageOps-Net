using System;
using ImageOps.Sources.Streams;

namespace ImageOps.Sources
{
    public interface IPixelSource : IDisposable
    {
        int ImageWidth { get; }
        int ImageHeight { get; }
        IPixelStream OpenStream();
    }
}