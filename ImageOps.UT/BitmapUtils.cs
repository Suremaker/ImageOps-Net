using System.Drawing;

namespace ImageOps.UT
{
	public static class BitmapUtils
	{
		public static Bitmap Create(int width, int height, Color initialColor)
		{
			var bmp = new Bitmap(width, height);

			for (int x = 0; x < width; ++x)
				for (int y = 0; y < height; ++y)
					bmp.SetPixel(x, y, initialColor);
			return bmp;
		}

		public static Bitmap Create(Color[,] pixels)
		{
			var width = pixels.GetLength(1);
			var height = pixels.GetLength(0);
			var bmp = new Bitmap(width, height);
			for (int x = 0; x < width; ++x)
				for (int y = 0; y < height; ++y)
					bmp.SetPixel(x, y, pixels[y, x]);
			return bmp;
		}
	}
}
