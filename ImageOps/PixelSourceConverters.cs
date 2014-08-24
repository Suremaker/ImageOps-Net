using System.Drawing;
using ImageOps.Sources;

namespace ImageOps
{
    public static class PixelSourceConverters
    {
        public static IPixelSource Expand(this IPixelSource source, int leftMargin, int topMargin, int rightMargin,
                                          int bottomMargin)
        {
            return new ExpandedSource(source, leftMargin, topMargin, rightMargin, bottomMargin);
        }

        public static IPixelSource Expand(this IPixelSource source, int horizontalMargin, int verticalMargin)
        {
            return new ExpandedSource(source, horizontalMargin, verticalMargin);
        }

        public static IPixelSource Expand(this IPixelSource source, int margin)
        {
            return new ExpandedSource(source, margin);
        }

        public static IPixelSource Crop(this IPixelSource source, Rectangle rectangle)
        {
            return new CroppedSource(source, rectangle);
        }
    }
}