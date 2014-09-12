using System;

namespace ImageOps.Sources.Readers
{
    public abstract class BlendingReader : PixelReader
    {
        protected IVerifiedPixelReader ForegroundReader { get; private set; }
        protected IVerifiedPixelReader BackgroundReader { get; private set; }

        protected BlendingReader(IPixelSource background, IPixelSource foreground)
            : base(background.ImageWidth, background.ImageHeight)
        {
            if (background.ImageWidth != foreground.ImageWidth || background.ImageHeight != foreground.ImageHeight)
                throw new ArgumentException(string.Format("Background layer size does not match size of foreground layer: Background={0}, Foreground={1}", background, foreground));
            BackgroundReader = background.OpenReader().InVerifiedContext();
            ForegroundReader = foreground.OpenReader().InVerifiedContext();
        }

        public override void Dispose()
        {
            ForegroundReader.Dispose();
            BackgroundReader.Dispose();
        }
    }
}