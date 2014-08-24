using System.Drawing;
using ImageOps.Sources;

namespace ImageOps
{
    public static class PixelSourceCreator
    {
        public static IPixelSource AsPixelSource(this Bitmap bitmap)
        {
            return new BitmapSource(bitmap);
        }

        public static IPixelSource AsPixelSource(this PixelColor color, int sourceWidth, int sourceHeight)
        {
            return new ColorSource(sourceWidth, sourceHeight, color);
        }

        public static IPixelSource AsPixelSource(this Color color, int sourceWidth, int sourceHeight)
        {
            return new ColorSource(sourceWidth, sourceHeight, color);
        }
    }
}