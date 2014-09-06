using System;
using ImageOps.Sources.Readers;

namespace ImageOps.Sources
{
    public interface IPixelSource : IDisposable
    {
        int ImageWidth { get; }
        int ImageHeight { get; }
        IPixelReader OpenReader();
    }
}