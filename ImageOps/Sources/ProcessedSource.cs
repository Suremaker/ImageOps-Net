using System;
using ImageOps.Sources.Readers;

namespace ImageOps.Sources
{
    public class ProcessedSource : IPixelSource
    {
        public ProcessedSource(IPixelSource source, Func<PixelColor, PixelColor> colorFunction)
        {
            ImageHeight = source.ImageHeight;
            ImageWidth = source.ImageWidth;
            ColorFunction = colorFunction;
            OriginalSource = source;
        }

        public void Dispose()
        {
            OriginalSource.Dispose();
        }

        public IPixelSource OriginalSource { get; private set; }
        public int ImageWidth { get; private set; }
        public int ImageHeight { get; private set; }
        public Func<PixelColor, PixelColor> ColorFunction { get; private set; }

        public IPixelReader OpenReader()
        {
            return new ProcessingReader(this);
        }
    }
}