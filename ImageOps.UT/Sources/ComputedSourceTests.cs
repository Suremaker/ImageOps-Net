using System.Collections.Generic;
using System.Linq;
using ImageOps.Sources;
using ImageOps.UT.Helpers;
using NUnit.Framework;

namespace ImageOps.UT.Sources
{
    [TestFixture]
    public class ComputedSourceTests : PixelSourceTestBase
    {
        [SetUp]
        public void SetUp()
        {
            ExpectedWidth = 2;
            ExpectedHeight = 3;
            ExpectedColors = new List<PixelColor>
            {
                PixelColor.FromRgb(0, 0, 0),
                PixelColor.FromRgb(1, 0, 0),

                PixelColor.FromRgb(0, 1, 0),
                PixelColor.FromRgb(1, 1, 0),

                PixelColor.FromRgb(0, 2, 0),
                PixelColor.FromRgb(1, 2, 0)
            };
            Subject = new ComputedSource(ExpectedWidth, ExpectedHeight, (x, y) => PixelColor.FromRgb((byte)x, (byte)y, 0));
        }
    }
}