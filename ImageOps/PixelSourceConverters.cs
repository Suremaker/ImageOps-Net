using ImageOps.Sources;

namespace ImageOps
{
    public static class PixelSourceConverters
    {
        public static IPixelSource Expand(this IPixelSource source, int leftMargin, int topMargin, int rightMargin, int bottomMargin)
        {
            return new ExpandedSource(source, leftMargin, topMargin, rightMargin, bottomMargin);
        }

        public static IPixelSource Expand(this IPixelSource source, int horizontalMargin, int verticalMargin)
        {
            return new ExpandedSource(source, horizontalMargin, verticalMargin, horizontalMargin, verticalMargin);
        }

        public static IPixelSource Expand(this IPixelSource source, int margin)
        {
            return new ExpandedSource(source, margin, margin, margin, margin);
        }

        public static IPixelSource Expand(this IPixelSource source, int leftMargin, int topMargin, int rightMargin, int bottomMargin, PixelColor expandedColor)
        {
            return new ExpandedSource(source, leftMargin, topMargin, rightMargin, bottomMargin, expandedColor);
        }

        public static IPixelSource Expand(this IPixelSource source, int horizontalMargin, int verticalMargin, PixelColor expandedColor)
        {
            return new ExpandedSource(source, horizontalMargin, verticalMargin, horizontalMargin, verticalMargin, expandedColor);
        }

        public static IPixelSource Expand(this IPixelSource source, int margin, PixelColor expandedColor)
        {
            return new ExpandedSource(source, margin, margin, margin, margin, expandedColor);
        }

        public static IPixelSource Crop(this IPixelSource source, PixelRectangle rectangle)
        {
            return new CroppedSource(source, rectangle);
        }
    }
}