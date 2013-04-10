using System.Drawing;
using ImageOps.Streaming.Converters;

namespace ImageOps
{
	public static class PixelStreamConverters
	{
		public static IPixelStream ExpandCanvas(this IPixelStream source, int leftMargin, int topMargin, int rightMargin, int bottomMargin)
		{
			return new ExpandCanvas(source, leftMargin, topMargin, rightMargin, bottomMargin);
		}

		public static IPixelStream ExpandCanvas(this IPixelStream source, int horizontalMargin, int verticalMargin)
		{
			return new ExpandCanvas(source, horizontalMargin, verticalMargin);
		}

		public static IPixelStream ExpandCanvas(this IPixelStream source, int margin)
		{
			return new ExpandCanvas(source, margin);
		}

		public static IPixelStream Crop(this IPixelStream source, Rectangle rectangle)
		{
			return new SourceCrop(source, rectangle);
		}
	}
}