using System.Drawing;
using System.Linq;
using ImageOps.Blending;
using ImageOps.Sources;
using NUnit.Framework;

namespace ImageOps.UT
{
	[TestFixture]
	public class BlendTests
	{
		[Test]
		public void ShouldBlendImagesUsingNormalBlending()
		{
			var bmp1 = new Bitmap(3, 1);
			var bmp2 = new Bitmap(3, 1);

			for (int i = 0; i < 3; ++i)
				bmp1.SetPixel(i, 0, Color.White);

			bmp2.SetPixel(0, 0, Color.Black);
			bmp2.SetPixel(1, 0, Color.Transparent);
			bmp2.SetPixel(2, 0, Color.FromArgb(127, Color.Black));

			var result = new NormalBlend(new BitmapSource(bmp1), new BitmapSource(bmp2)).ToArray();
			Assert.That(result, Is.EqualTo(new[]
			{
				PixelColor.FromRgb(0,0,0),
				PixelColor.FromRgb(255,255,255),
				PixelColor.FromRgb(127,127,127)
			}));
		}

		[Test]
		public void ShouldBlendImagesUsingMultiplyBlending()
		{
			var bmp1 = new Bitmap(3, 1);
			var bmp2 = new Bitmap(3, 1);

			for (int i = 0; i < 3; ++i)
				bmp1.SetPixel(i, 0, Color.White);

			bmp2.SetPixel(0, 0, Color.Black);
			bmp2.SetPixel(1, 0, Color.White);
			bmp2.SetPixel(2, 0, Color.FromArgb(10,80,70));

			var result = new MultiplyBlend(new BitmapSource(bmp1), new BitmapSource(bmp2)).ToArray();
			Assert.That(result, Is.EqualTo(new[]
			{
				PixelColor.FromRgb(0,0,0),
				PixelColor.FromRgb(255,255,255),
				PixelColor.FromRgb(10,80,70)
			}));
		}
	}
}
