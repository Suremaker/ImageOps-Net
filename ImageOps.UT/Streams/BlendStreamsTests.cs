﻿using System.Drawing;
using System.Linq;
using ImageOps.Streaming.Blenders;
using ImageOps.Streaming.Sources;
using NUnit.Framework;

namespace ImageOps.UT.Streams
{
	[TestFixture]
	public class BlendStreamsTests
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
		public void ShouldBlendImagesUsingGrainMergeBlending()
		{
			var back = BitmapUtils.Create(3, 1, Color.White);
			var front = BitmapUtils.Create(new[,]
			{
				{ Color.Black, Color.White, Color.FromArgb(10, 80, 70) }
			});

			var result = new GrainMergeBlend(new BitmapSource(back), new BitmapSource(front)).ToArray();
			Assert.That(result, Is.EqualTo(new[]
			{
				PixelColor.FromRgb(127,127,127),
				PixelColor.FromRgb(255,255,255),
				PixelColor.FromRgb(137,207,197)
			}));
		}

		[Test]
		public void ShouldBlendImagesUsingBurnBlending()
		{
			var back = BitmapUtils.Create(new[,]
			{
				{Color.Red, Color.Green, Color.Blue, Color.Gray}
			});
			var front = BitmapUtils.Create(new[,]
			{
				{Color.Green, Color.Yellow, Color.Gray, Color.Magenta}
			});

			var result = new BurnBlend(new BitmapSource(back), new BitmapSource(front)).ToArray();
			Assert.That(result, Is.EqualTo(new[]
			{
				Color.Black,
				PixelColor.FromRgb(0,128,0),
				PixelColor.FromRgb(0,0,255),
				PixelColor.FromRgb(128,0,128)
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

		[Test]
		public void ShouldBlendImagesUsingAddBlending()
		{
			var back = BitmapUtils.Create(new[,]
			{
				{ Color.Transparent,Color.White,Color.Black,Color.Black,Color.Black, Color.FromArgb(127,120,130,140), Color.FromArgb(127,120,130,140) }
			});

			var front = BitmapUtils.Create(new[,]
			{
				{ Color.White,Color.White,Color.Black,Color.FromArgb(127,127,127),Color.White, Color.FromArgb(10,20,30), Color.FromArgb(100,10,20,30) }
			});

			var result = new AddBlend(new BitmapSource(back), new BitmapSource(front)).ToArray();

			Assert.That(result, Is.EqualTo(new[]
			{
				PixelColor.FromArgb(0,0,0,0),
				Color.White,
				Color.Black,
				PixelColor.FromRgb(127,127,127),
				Color.White,
				PixelColor.FromArgb(127,126,143,159),
				PixelColor.FromArgb(127,125,141,156)
			}));
		}

		[Test]
		public void ShouldApplyAlphaMask()
		{
			var bitmap = BitmapUtils.Create(new[,]
			{
				{ Color.Transparent,Color.White,Color.White, Color.FromArgb(127,Color.White), Color.FromArgb(127,Color.White) }
			});

			var mask = BitmapUtils.Create(new[,]
			{
				{ Color.FromArgb(255,0,0,0),Color.FromArgb(255,0,0,0),Color.FromArgb(0,0,0,0),Color.FromArgb(255,0,0,0),Color.FromArgb(127,0,0,0) }
			});

			var result = new AlphaMaskBlend(new BitmapSource(bitmap), new BitmapSource(mask), ColorChannel.Alpha).ToArray();

			Assert.That(result, Is.EqualTo(new[]
			{
				PixelColor.FromArgb(0,255,255,255),
				PixelColor.FromArgb(255,255,255,255),
				PixelColor.FromArgb(0,255,255,255),
				PixelColor.FromArgb(127,255,255,255),
				PixelColor.FromArgb(63,255,255,255)
			}));
		}

		[Test]
		[TestCase(ColorChannel.Alpha, 50)]
		[TestCase(ColorChannel.Red, 100)]
		[TestCase(ColorChannel.Green, 150)]
		[TestCase(ColorChannel.Blue, 200)]
		public void ShoukdApplyAlphaMaskUsingProperChannel(ColorChannel channel, byte expectedAlpha)
		{
			var color = PixelColor.FromRgb(100, 100, 100);
			var mask = PixelColor.FromArgb(50, 100, 150, 200);
			var result = new AlphaMaskBlend(new ColorSource(1, 1, color), new ColorSource(1, 1, mask), channel);
			Assert.That(result.Single().A, Is.EqualTo(expectedAlpha));
		}
	}
}
