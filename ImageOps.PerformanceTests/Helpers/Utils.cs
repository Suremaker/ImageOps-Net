using System.Drawing;
using ImageOps.Sources;

namespace ImageOps.PerformanceTests.Helpers
{
    public class Utils
    {
        public static Bitmap CreateStandardBitmapWithTransparency(int width, int height)
        {
            return BitmapCreator.Create(width, height, (x, y) => Color.FromArgb(x * y % 256, (x + y) % 256, x % 256, y % 256));
        }

        public static Bitmap CreateStandardBitmap(int width, int height)
        {
            return BitmapCreator.Create(width, height, (x, y) => Color.FromArgb((x + y) % 256, x % 256, y % 256));
        }

        public static IPixelSource CreateStandardSourceWithTransparency(int width, int height)
        {
            return CreateStandardBitmapWithTransparency(width, height).AsPixelSource();
        }

        public static IPixelSource CreateStandardSource(int width, int height)
        {
            return CreateStandardBitmap(width, height).AsPixelSource();
        }

        public static IPixelSource CreateColorSource(int width, int height)
        {
            return CreateColorSource(width, height, Color.Yellow);
        }

        public static IPixelSource CreateColorSource(int width, int height, Color color)
        {
            return color.AsPixelSource(width, height);
        }
    }
}