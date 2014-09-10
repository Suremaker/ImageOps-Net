using System.Drawing;
using System.Linq;
using ImageOps.Sources;
using ImageOps.UT.Helpers;
using NUnit.Framework;

namespace ImageOps.UT.Sources
{
    [TestFixture]
    public class RegionBlendedSourceTests : PixelSourceTestBase
    {
        private readonly PixelColor b = Color.Black;
        private readonly PixelColor w = Color.White;
        private readonly PixelColor r = Color.Red;
        private readonly PixelColor y = Color.Yellow;
        private readonly PixelColor g = Color.Green;

        [SetUp]
        public void SetUp()
        {
            ExpectedWidth = 6;
            ExpectedHeight = 6;
            Subject = new RegionBlendedSource(b.AsPixelSource(ExpectedWidth, ExpectedHeight))
                .AddRegion(
                    Regions.Polygon(new Point(3, 1), new Point(1, 4), new Point(4, 4)),
                    w.AsPixelSource(4, 4),
                    BlendingMethods.Normal);

            ExpectedColors = new[,]
            {
                {b, b, b, b, b, b},
                {b, b, b, w, b, b},
                {b, b, b, w, b, b},
                {b, b, w, w, b, b},
                {b, w, w, w, w, b},
                {b, b, b, b, b, b}
            }.Cast<PixelColor>().ToList();
        }

        [Test]
        public void ShouldMixRegionsInProperOrder()
        {
            Subject
                .BlendRegion(
                    Regions.Rectangle(1, 1, 3, 3),
                    r.AsPixelSource(3, 3),
                    BlendingMethods.Normal)
                .BlendRegion(
                    Regions.Polygon(new Point(4, 1)),
                    y.AsPixelSource(1, 1),
                    BlendingMethods.Normal)
                .BlendRegion(
                    Regions.Polygon(new Point(2, 2), new Point(4, 4)),
                    g.AsPixelSource(3, 3),
                    BlendingMethods.Normal);

            var expected = new[,]
            {
                {b, b, b, b, b, b},
                {b, r, r, r, y, b},
                {b, r, g, r, b, b},
                {b, r, r, g, b, b},
                {b, w, w, w, g, b},
                {b, b, b, b, b, b}
            }.Cast<PixelColor>().ToList();

            Assert.That(Subject.OpenReader().AsEnumerable().ToList(), Is.EqualTo(expected));
        }
    }
}