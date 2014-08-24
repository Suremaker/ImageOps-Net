using System.Collections.Generic;
using System.Drawing;
using ImageOps.Sources;
using ImageOps.UT.Helpers;
using NUnit.Framework;

namespace ImageOps.UT.Sources
{
    [TestFixture]
    public class ExpandedSourceTests : PixelSourceTestBase
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

            Subject = new ColorSource(1, 1, Color.White).Expand(1);
        }

        [Test]
        public void ShouldExpandCanvasProperly([Values(0, 1, 10)] int left, [Values(0, 1, 10)] int top,
                                               [Values(0, 1, 10)] int right, [Values(0, 1, 10)] int bottom)
        {
            var bmp = new ExpandedSource(new ColorSource(2, 2, Color.White), left, top, right, bottom).ToBitmap();
            Assert.That(bmp.Width, Is.EqualTo(left + right + 2));
            Assert.That(bmp.Height, Is.EqualTo(top + bottom + 2));

            for (int x = 0; x < bmp.Width; ++x)
                for (int y = 0; y < bmp.Height; ++y)
                {
                    var color = bmp.GetPixel(x, y);

                    var sx = x - left;
                    var sy = y - top;
                    var expected = ((sx == 0 || sx == 1) && (sy == 0 || sy == 1))
                                       ? Color.FromArgb(255, 255, 255, 255)
                                       : Color.FromArgb(0, 255, 255, 255);

                    Assert.That(color, Is.EqualTo(expected), string.Format("Wrong color at {0}x{1}", x, y));
                }
        }
    }
}