using System;
using ImageOps.Blenders;
using ImageOps.Sources.Readers;

namespace ImageOps.Sources
{
    public class BlendedSource : IPixelSource
    {
        public IBlendingMethod BlendingMethod { get; private set; }
        public IPixelSource BackgroundSource { get; private set; }
        public IPixelSource ForegroundSource { get; private set; }

        public BlendedSource(IBlendingMethod blendingMethod, IPixelSource background, IPixelSource foreground)
        {
            if (background.ImageHeight != foreground.ImageHeight || background.ImageWidth != foreground.ImageWidth)
                throw new ArgumentException(string.Format("Background layer size does not match size of foreground layer: Background={0}, Foreground={1}", background, foreground));
            BlendingMethod = blendingMethod;
            BackgroundSource = background;
            ForegroundSource = foreground;
            ImageWidth = background.ImageWidth;
            ImageHeight = background.ImageHeight;
        }

        public int ImageWidth { get; private set; }

        public int ImageHeight { get; private set; }

        public IPixelReader OpenReader()
        {
            return BlendingMethod.OpenBlendingReader(BackgroundSource, ForegroundSource);
        }

        public void Dispose()
        {
            BackgroundSource.Dispose();
            ForegroundSource.Dispose();
        }
    }
}