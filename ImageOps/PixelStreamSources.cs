using System.Drawing;
using ImageOps.Streaming.Sources;

namespace ImageOps
{
	public static class PixelStreamSources
	{
		public static IPixelStream AsPixelSource(this Bitmap bitmap)
		{
			return new BitmapSource(bitmap);
		}

		public static IPixelStream AsPixelSource(this PixelColor color, int sourceWidth, int sourceHeight)
		{
			return new ColorSource(sourceWidth, sourceHeight, color);
		}

		public static IPixelStream AsPixelSource(this Color color, int sourceWidth, int sourceHeight)
		{
			return new ColorSource(sourceWidth, sourceHeight, color);
		}
	}
}