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
			var back = BitmapUtils.Create(3, 1, Color.White);
			var front = BitmapUtils.Create(new[,]
			{
				{ Color.Black, Color.Transparent, Color.FromArgb(127, Color.Black) }
			});

			var result = new NormalBlend(new BitmapSource(back), new BitmapSource(front)).ToArray();
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
			var back = BitmapUtils.Create(3, 1, Color.White);
			var front = BitmapUtils.Create(new[,]
			{
				{ Color.Black, Color.White, Color.FromArgb(10, 80, 70) }
			});

			var result = new MultiplyBlend(new BitmapSource(back), new BitmapSource(front)).ToArray();
			Assert.That(result, Is.EqualTo(new[]
			{
				PixelColor.FromRgb(0,0,0),
				PixelColor.FromRgb(255,255,255),
				PixelColor.FromRgb(10,80,70)
			}));
		}

		[Test]
		public void ShouldBlendTransparentImagesUsingMultiplyBlending()
		{
			var back = BitmapUtils.Create(new[,]
			{
				{ Color.Transparent,Color.White, Color.FromArgb(127,Color.White), Color.FromArgb(127,Color.White) }
			});

			var front = BitmapUtils.Create(new[,]
			{
				{ Color.Red,Color.Red, Color.Red, Color.FromArgb(200,Color.Red) }
			});

			var result = new MultiplyBlend(new BitmapSource(back), new BitmapSource(front)).ToArray();

			Assert.That(result, Is.EqualTo(new[]
			{
				PixelColor.FromArgb(0,0,0,0),
				PixelColor.FromArgb(255,255,0,0),
				PixelColor.FromArgb(127,255,85,85),
				PixelColor.FromArgb(127,255,85,85)
			}));
		}
	}
}
