using System.Drawing;
using NUnit.Framework;

namespace ImageOps.UT
{
    [TestFixture]
    public class PixelColorTests
    {
        [Test]
        public void ShouldSetColor()
        {
            var expected = Color.FromArgb(10, 20, 30, 40);
            var pixelColor = new PixelColor(expected);
            Assert.That(pixelColor.Color, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldSetArgbValues()
        {
            var pixelColor = new PixelColor(Color.FromArgb(10, 20, 30, 40));
            Assert.That(pixelColor.A, Is.EqualTo(10));
            Assert.That(pixelColor.R, Is.EqualTo(20));
            Assert.That(pixelColor.G, Is.EqualTo(30));
            Assert.That(pixelColor.B, Is.EqualTo(40));
        }

        [Test]
        public void ShouldSetFloatValues()
        {
            const float alpha = 0.2f;
            const float red = 0.4f;
            const float green = 0.6f;
            const float blue = 0.8f;
            var pixelColor = PixelColor.FromFargb(alpha, red, green, blue);
            Assert.That(pixelColor.GetAlpha(), Is.EqualTo(alpha));
            Assert.That(pixelColor.GetRed(), Is.EqualTo(red));
            Assert.That(pixelColor.GetGreen(), Is.EqualTo(green));
            Assert.That(pixelColor.GetBlue(), Is.EqualTo(blue));
        }

        [Test]
        public void ShouldConvertRgbaToFloat()
        {
            const float red = 0.2f;
            const float green = 0.3f;
            var pixelColor = PixelColor.FromFargb(0f, red, green, 1f);
            Assert.That(pixelColor.A, Is.EqualTo(0));
            Assert.That(pixelColor.R, Is.EqualTo((byte) (red*255)));
            Assert.That(pixelColor.G, Is.EqualTo((byte) (green*255)));
            Assert.That(pixelColor.B, Is.EqualTo(255));
        }
    }
}