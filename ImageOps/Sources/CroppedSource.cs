using System;
using ImageOps.Sources.Readers;

namespace ImageOps.Sources
{
    public class CroppedSource : IPixelSource
    {
        public IPixelSource OriginalSource { get; private set; }
        public PixelRectangle CroppedRegion { get; private set; }

        public CroppedSource(IPixelSource source, PixelRectangle rectangle)
        {
            if (rectangle.Width <= 0 || rectangle.Height <= 0)
                throw new ArgumentException("Cropped image width and height has to be > 0");

            if (rectangle.X < 0 || rectangle.Y < 0 || rectangle.X + rectangle.Width > source.ImageWidth ||
                rectangle.Y + rectangle.Height > source.ImageHeight)
                throw new ArgumentException("Cropped dimensions cannot expand over source dimensions");

            OriginalSource = source;
            CroppedRegion = rectangle;
        }

        public void Dispose()
        {
            OriginalSource.Dispose();
        }

        public int ImageWidth
        {
            get { return CroppedRegion.Width; }
        }

        public int ImageHeight
        {
            get { return CroppedRegion.Height; }
        }

        public IPixelReader OpenReader()
        {
            return new CroppingReader(this);
        }
    }
}