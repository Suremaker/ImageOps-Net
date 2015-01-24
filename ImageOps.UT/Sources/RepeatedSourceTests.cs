using System.Collections.Generic;
using System.Drawing;
using ImageOps.Sources;
using ImageOps.UT.Helpers;
using NUnit.Framework;

namespace ImageOps.UT.Sources
{
    [TestFixture]
    public class RepeatedSourceTests : PixelSourceTestBase
    {
        [SetUp]
        public void SetUp()
        {
            ExpectedWidth = 4;
            ExpectedHeight = 4;

            var pixels = new[,]
            {
                {Color.Black, Color.Red},
                {Color.Blue, Color.Yellow}
            };
            ExpectedColors = new List<PixelColor>
            {
                Color.Black, Color.Red, Color.Black, Color.Red,
                Color.Blue, Color.Yellow, Color.Blue, Color.Yellow,
                Color.Black, Color.Red, Color.Black, Color.Red,
                Color.Blue, Color.Yellow, Color.Blue, Color.Yellow
            };

            Subject = new RepeatedSource(BitmapCreator.Create(pixels).AsPixelSource(), ExpectedWidth, ExpectedHeight);
        }
    }
}