using System;
using System.Drawing;

namespace ImageOps.Streaming.Converters
{
    public class SourceCrop2 : IPixelSource
    {
        public IPixelSource Source { get; private set; }
        public Rectangle Rectangle { get; private set; }
        public int BaseOffset { get; private set; }
        public int SkippedPixels { get; private set; }

        public SourceCrop2(IPixelSource source, Rectangle rectangle)
        {
            if (rectangle.Width <= 0 || rectangle.Height <= 0)
                throw new ArgumentException("Cropped image width and height has to be > 0");

            if (rectangle.X < 0 || rectangle.Y < 0 || rectangle.X + rectangle.Width > source.ImageWidth || rectangle.Y + rectangle.Height > source.ImageHeight)
                throw new ArgumentException("Cropped dimensions cannot expand over source dimensions");

            Source = source;
            Rectangle = rectangle;
            BaseOffset = Rectangle.Y * Source.ImageWidth + Rectangle.X;
            SkippedPixels = Source.ImageWidth - Rectangle.Width;

        }

        public void Dispose()
        {
            Source.Dispose();
        }

        public int ImageWidth { get { return Rectangle.Width; } }
        public int ImageHeight { get { return Rectangle.Height; } }
        public IPixelStream2 OpenStream()
        {
            return new CropStream(this);
        }
    }

    public class CropStream : SourceStream2<SourceCrop2>
    {
        private IPixelStream2 _source;

        public CropStream(SourceCrop2 sourceCrop2)
            : base(sourceCrop2)
        {
            _source = Source.Source.OpenStream();
            _source.Move(Source.BaseOffset);
        }

        public override void Dispose()
        {
            _source.Dispose();
        }

        public override void MoveBy(int delta)
        {
            var cropPos = delta + Position;
            var expectedPos = Source.BaseOffset + cropPos + (cropPos / Source.Rectangle.Width) * Source.SkippedPixels;
            _source.Move(expectedPos - _source.Position);
        }

        public override PixelColor GetCurrent()
        {
            return _source.GetCurrent();
        }
    }
}