using System.Linq;
using ImageOps.Sources;
using ImageOps.UT.Helpers;
using NUnit.Framework;

namespace ImageOps.UT.Sources
{
    [TestFixture]
    public class ColorSourceTests : PixelSourceTestBase
    {
        [SetUp]
        public void SetUp()
        {
            ExpectedWidth = 2;
            ExpectedHeight = 3;
            PixelColor color = PixelColor.FromArgb(10, 20, 30, 40);
            ExpectedColors = Enumerable.Repeat(color, 6).ToList();
            Subject = new ColorSource(ExpectedWidth, ExpectedHeight, color);
        }
    }
}