using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ImageOps.Sources;
using NUnit.Framework;

namespace ImageOps.UT.Converters
{
    [TestFixture]
    public class SourceConverterTests
    {
        [Test]
        public void Should_invert_colors()
        {
            var actual = BitmapCreator.Create(new[,] { { Color.Red, Color.Blue, Color.Magenta, Color.FromArgb(100, 200, 250) } })
                .AsPixelSource()
                .InvertColors()
                .OpenReader()
                .AsEnumerable()
                .ToArray();
            Assert.That(actual, Is.EqualTo(new PixelColor[] { Color.Cyan, Color.Yellow, Color.FromArgb(0, 255, 0), Color.FromArgb(155, 55, 5) }));
        }
    }
}
