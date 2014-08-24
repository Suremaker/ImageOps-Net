using System;
using System.Drawing;
using ImageOps.Sources.Streams;

namespace ImageOps.Sources
{
    public class CroppedSource : IPixelSource
    {
        public IPixelSource OriginalSource { get; private set; }
        public Rectangle CroppedRegion { get; private set; }
        public int BaseOffset { get; private set; }
        public int SkippedPixels { get; private set; }

        public CroppedSource(IPixelSource source, Rectangle rectangle)
        {
            if (rectangle.Width <= 0 || rectangle.Height <= 0)
                throw new ArgumentException("Cropped image width and height has to be > 0");

            if (rectangle.X < 0 || rectangle.Y < 0 || rectangle.X + rectangle.Width > source.ImageWidth ||
                rectangle.Y + rectangle.Height > source.ImageHeight)
                throw new ArgumentException("Cropped dimensions cannot expand over source dimensions");

            OriginalSource = source;
            CroppedRegion = rectangle;
            BaseOffset = CroppedRegion.Y * OriginalSource.ImageWidth + CroppedRegion.X;
            SkippedPixels = OriginalSource.ImageWidth - CroppedRegion.Width;
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

        public IPixelStream OpenStream()
        {
            return new CroppingStream(this);
        }
    }
}