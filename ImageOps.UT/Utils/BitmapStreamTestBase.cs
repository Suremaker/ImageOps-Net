using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using ImageOps.Sources;

namespace ImageOps.UT.Utils
{
	public abstract class BitmapStreamTestBase : PixelStreamTestBase
	{
		protected void SetUpBitmap(int width, int height, PixelFormat format, Func<byte, byte, PixelColor> createColor)
		{
			var bitmap = new Bitmap(width, height, format);
			ExpectedColors = new List<PixelColor>();
			for (int y = 0; y < bitmap.Height; ++y)
				for (int x = 0; x < bitmap.Width; ++x)
				{
					var pixelColor = createColor((byte)x, (byte)y);
					bitmap.SetPixel(x, y, pixelColor.Color);
					ExpectedColors.Add(pixelColor);
				}

			Subject = new BitmapSource(bitmap);
			ExpectedWidth = width;
			ExpectedHeight = height;
		}
	}
}