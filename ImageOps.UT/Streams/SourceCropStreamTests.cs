using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ImageOps.Operations;
using ImageOps.Sources;
using ImageOps.UT.Utils;
using NUnit.Framework;

namespace ImageOps.UT.Streams
{
	[TestFixture]
	public class SourceCropStreamTests : PixelStreamTestBase
	{
		[SetUp]
		public void SetUp()
		{
			ExpectedWidth = 2;
			ExpectedHeight = 3;

			ExpectedColors = new List<PixelColor>();
			var colors = new Color[5, 4];

			for (int y = 0; y < colors.GetLength(0); ++y)
				for (int x = 0; x < colors.GetLength(1); ++x)
				{
					colors[y, x] = Color.FromArgb(x, y, x, y);
					if (x > 0 && y > 0 && x < colors.GetLength(1) - 1 && y < colors.GetLength(0) - 1)
						ExpectedColors.Add(colors[y, x]);
				}

			Subject = new SourceCrop(new BitmapSource(BitmapUtils.Create(colors)), new Rectangle(1, 1, 2, 3));
		}

		[Test]
		[TestCase(10, 10, -1, 0, 2, 2)]
		[TestCase(10, 10, 0, -1, 2, 2)]
		[TestCase(10, 10, 0, 0, 11, 2)]
		[TestCase(10, 10, 0, 0, 2, 11)]
		[TestCase(10, 10, 10, 0, 1, 2)]
		[TestCase(10, 10, 0, 10, 1, 2)]
		[TestCase(10, 10, 5, 5, 6, 6)]
		public void ShouldThrowIfCroppedRectangleExpandsOverSource(int width, int height, int cropX, int cropY, int cropWidth, int cropHeight)
		{
			var ex = Assert.Throws<ArgumentException>(() => new SourceCrop(new ColorSource(width, height, Color.Red), new Rectangle(cropX, cropY, cropWidth, cropHeight)));
			Assert.That(ex.Message, Is.EqualTo("Cropped dimensions cannot expand over source dimensions"));
		}

		[Test]
		[TestCase(0, 2)]
		[TestCase(1, 0)]
		[TestCase(1, -2)]
		[TestCase(-1, 1)]
		public void ShouldThrowIfCroppedRectangleSizeIsInvalid( int cropWidth, int cropHeight)
		{
			var ex = Assert.Throws<ArgumentException>(() => new SourceCrop(new ColorSource(100, 100, Color.Red), new Rectangle(0, 0, cropWidth, cropHeight)));
			Assert.That(ex.Message, Is.EqualTo("Cropped image width and height has to be > 0"));
		}
	}
}
