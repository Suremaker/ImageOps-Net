using System.Drawing;
using ImageOps.Streaming.Converters;

namespace ImageOps
{
	public static class PixelStreamConverters
	{
        public static IPixelSource ExpandCanvas(this IPixelSource source, int leftMargin, int topMargin, int rightMargin, int bottomMargin)
		{
			return new ExpandCanvas2(source, leftMargin, topMargin, rightMargin, bottomMargin);
		}

        public static IPixelSource ExpandCanvas(this IPixelSource source, int horizontalMargin, int verticalMargin)
		{
			return new ExpandCanvas2(source, horizontalMargin, verticalMargin);
		}

        public static IPixelSource ExpandCanvas(this IPixelSource source, int margin)
		{
			return new ExpandCanvas2(source, margin);
		}

        public static IPixelSource Crop(this IPixelSource source, Rectangle rectangle)
		{
			return new SourceCrop2(source, rectangle);
		}
	}
}