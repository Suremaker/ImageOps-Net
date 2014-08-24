using System;
using ImageOps.Streaming.Blenders;

namespace ImageOps.Streaming.Sources
{
    public class ColorSource : IPixelSource
    {
        private readonly int _width;
        private readonly int _height;

        public ColorSource(int width, int height, PixelColor color)
        {
            _width = width;
            _height = height;
            Color = color;
        }

        public int ImageWidth
        {
            get { return _width; }
        }

        public int ImageHeight
        {
            get { return _height; }
        }

        public PixelColor Color { get; private set; }

        public IPixelStream2 OpenStream()
        {
            return new ColorStream(this);
        }

        public void Dispose()
        {
        }
    }

    public class ColorStream : SourceStream2<ColorSource>
    {
        public ColorStream(ColorSource source)
            : base(source)
        {
        }

        public override void Dispose()
        {

        }

        public override void MoveBy(int delta)
        {

        }

        public override PixelColor GetCurrent()
        {
            return Source.Color;
        }
    }

    public class BlendedSource : IPixelSource
    {
        public IBlendingMethod BlendingMethod { get; private set; }
        public IPixelSource Background { get; private set; }
        public IPixelSource Foreground { get; private set; }

        public BlendedSource(IBlendingMethod blendingMethod, IPixelSource background, IPixelSource foreground)
        {
            if (background.ImageHeight != foreground.ImageHeight || background.ImageWidth != foreground.ImageWidth)
                throw new ArgumentException(string.Format("Background layer size does not match size of foreground layer: Background={0}, Foreground={1}", background, foreground));
            BlendingMethod = blendingMethod;
            Background = background;
            Foreground = foreground;
        }

        public int ImageWidth { get { return Background.ImageWidth; } }
        public int ImageHeight { get { return Background.ImageHeight; } }

        public IPixelStream2 OpenStream()
        {
            return new BlendingStream2(this);
        }

        public void Dispose()
        {
            Background.Dispose();
            Foreground.Dispose();
        }
    }

    public class BlendingStream2 : SourceStream2<BlendedSource>
    {
        private readonly IPixelStream2 _foreground;
        private readonly IPixelStream2 _background;

        public BlendingStream2(BlendedSource source)
            : base(source)
        {
            _foreground = source.Foreground.OpenStream();
            _background = source.Background.OpenStream();
        }

        public override void Dispose()
        {
            _foreground.Dispose();
            _background.Dispose();
        }

        public override void MoveBy(int delta)
        {
            _foreground.Move(delta);
            _background.Move(delta);
        }

        public override PixelColor GetCurrent()
        {
            return Source.BlendingMethod.Blend(_background.GetCurrent(), _foreground.GetCurrent());
        }
    }
}