using System;
using ImageOps.Sources.Streams;

namespace ImageOps.Sources
{
    public class ComputedSource : IPixelSource
    {
        public ComputedSource(int imageWidth, int imageHeight, Func<int, int, PixelColor> colorFunction)
        {
            ImageHeight = imageHeight;
            ColorFunction = colorFunction;
            ImageWidth = imageWidth;
        }

        public void Dispose()
        {
        }

        public int ImageWidth { get; private set; }
        public int ImageHeight { get; private set; }
        public Func<int, int, PixelColor> ColorFunction { get; private set; }

        public IPixelStream OpenStream()
        {
            return new ComputingStream(this);
        }
    }
}