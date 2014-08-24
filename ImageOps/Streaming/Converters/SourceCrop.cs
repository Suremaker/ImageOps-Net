using System;
using System.Drawing;

namespace ImageOps.Streaming.Converters
{
    public class SourceCrop : SourceStream
    {
        private readonly IPixelStream _stream;
        private readonly Rectangle _rectangle;
        private readonly int _baseOffset;
        private readonly int _skippedPixels;

        public SourceCrop(IPixelStream stream, Rectangle rectangle)
        {
            if (rectangle.Width <= 0 || rectangle.Height <= 0)
                throw new ArgumentException("Cropped image width and height has to be > 0");

            if (rectangle.X < 0 || rectangle.Y < 0 || rectangle.X + rectangle.Width > stream.ImageWidth || rectangle.Y + rectangle.Height > stream.ImageHeight)
                throw new ArgumentException("Cropped dimensions cannot expand over source dimensions");

            _stream = stream;
            _rectangle = rectangle;
            _baseOffset = _rectangle.Y * _stream.ImageWidth + _rectangle.X;
            _skippedPixels = _stream.ImageWidth - _rectangle.Width;
            _stream.Move(_baseOffset);
        }

        protected override void MoveBy(int i)
        {
            var cropPos = i + Position;
            var expectedPos = _baseOffset + cropPos + (cropPos / _rectangle.Width) * _skippedPixels;
            _stream.Move(expectedPos - _stream.Position);
        }

        protected override PixelColor GetCurrentPixel()
        {
            return _stream.GetCurrent();
        }

        public override int ImageWidth
        {
            get { return _rectangle.Width; }
        }

        public override int ImageHeight
        {
            get { return _rectangle.Height; }
        }

        public override void Dispose()
        {
            _stream.Dispose();
        }
    }

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

        public int ImageWidth { get { return Source.ImageWidth; } }
        public int ImageHeight { get { return Source.ImageHeight; } }
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
            _source = Source.OpenStream();
            _source.Move(Source.BaseOffset);
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
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