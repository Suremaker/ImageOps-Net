using ImageOps.Sources.Readers;

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

        public IPixelReader OpenReader()
        {
            return new ColorReader(this);
        }

        public void Dispose()
        {
        }
    }
}