using System.Collections.Generic;
using System.Drawing;
using ImageOps.Streaming.Converters;
using ImageOps.Streaming.Sources;
using ImageOps.UT.Utils;
using NUnit.Framework;

namespace ImageOps.UT.Streams
{
	[TestFixture]
	public class ExpandCanvasStreamTests : PixelStreamTestBase
	{
		[SetUp]
		public void SetUp()
		{
			ExpectedWidth = 3;
			ExpectedHeight = 3;

			ExpectedColors = new List<PixelColor>
			{
				PixelColor.Transparent,
				PixelColor.Transparent,
				PixelColor.Transparent,
				PixelColor.Transparent,
				Color.White,
				PixelColor.Transparent,
				PixelColor.Transparent,
				PixelColor.Transparent,
				PixelColor.Transparent
			};

			Subject = new ExpandCanvas(new ColorSource(1, 1, Color.White), 1);
		}

		[Test]
		public void ShouldExpandCanvasProperly([Values(0, 1, 2)]int left, [Values(0, 1, 2)]int top, [Values(0, 1, 2)]int right, [Values(0, 1, 2)]int bottom)
		{
			var bmp = new ExpandCanvas(new ColorSource(1, 1, Color.White), left, top, right, bottom).ToBitmap();
			Assert.That(bmp.Width, Is.EqualTo(left + right + 1));
			Assert.That(bmp.Height, Is.EqualTo(top + bottom + 1));

			for (int x = 0; x < bmp.Width; ++x)
				for (int y = 0; y < bmp.Height; ++y)
				{
					var color = bmp.GetPixel(x, y);
					var expected = (x - left == 0 && y - top == 0)
						? Color.FromArgb(255, 255, 255, 255) : Color.FromArgb(0, 0, 0, 0);

					Assert.That(color, Is.EqualTo(expected), string.Format("Wrong color at {0}x{1}", x, y));
				}
		}
	}
}
