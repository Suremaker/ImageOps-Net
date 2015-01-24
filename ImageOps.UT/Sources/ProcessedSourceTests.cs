using System.Collections.Generic;
using System.Drawing;
using ImageOps.Sources;
using ImageOps.UT.Helpers;
using NUnit.Framework;

namespace ImageOps.UT.Sources
{
    [TestFixture]
    public class ProcessedSourceTests : PixelSourceTestBase
    {
        [SetUp]
        public void SetUp()
        {
            ExpectedWidth = 3;
            ExpectedHeight = 1;

            var pixels = new[,]
            {
                {Color.Black, Color.FromArgb(0,255,0),Color.Blue}
            };
            ExpectedColors = new List<PixelColor>
            {
                Color.Red,
                Color.Yellow,
                Color.Magenta
            };

            Subject = new ProcessedSource(BitmapCreator.Create(pixels).AsPixelSource(), px => PixelColor.FromRgb(255, px.G, px.B));
        }
    }
}