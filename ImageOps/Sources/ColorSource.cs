using ImageOps.Sources.Streams;

namespace ImageOps.Sources
{
    public class ColorSource : IPixelSource
    {
        public ColorSource(int width, int height, PixelColor color)
        {
            ImageWidth = width;
            ImageHeight = height;
            Color = color;
        }

        public int ImageWidth { get; private set; }
        public int ImageHeight { get; private set; }
        public PixelColor Color { get; private set; }

        public IPixelStream OpenStream()
        {
            return new ColorStream(this);
        }

        public void Dispose()
        {
        }
    }
}