using System.Collections.Generic;
using System.Linq;
using ImageOps.Sources;
using NUnit.Framework;

namespace ImageOps.UT.Helpers
{
    public abstract class PixelSourceTestBase
    {
        protected IPixelSource Subject;
        protected List<PixelColor> ExpectedColors;
        protected int ExpectedWidth;
        protected int ExpectedHeight;

        [Test]
        public void ShouldHaveProperDimensions()
        {
            Assert.That(Subject.ImageWidth, Is.EqualTo(ExpectedWidth));
            Assert.That(Subject.ImageHeight, Is.EqualTo(ExpectedHeight));
        }

        [Test]
        public void ShouldOpenStreamWithProperSize()
        {
            var stream = Subject.OpenStream();
            Assert.That(stream.Width, Is.EqualTo(ExpectedWidth));
            Assert.That(stream.Height, Is.EqualTo(ExpectedHeight));
        }

        [Test]
        public void ShouldOpenStreamAndReturnProperPixels()
        {
            var stream = Subject.OpenStream();
            for (int x = 0; x < ExpectedWidth; ++x)
                for (int y = 0; y < ExpectedHeight; ++y)
                    Assert.That(stream.Get(x, y), Is.EqualTo(ExpectedColors[y * ExpectedWidth + x]));
        }

        [Test]
        public void ShouldOpenStreamAndReadAllPixels()
        {
            Assert.That(Subject.OpenStream().ToArray(), Is.EqualTo(ExpectedColors));
        }

        [TearDown]
        public void PixelStreamTearDown()
        {
            Subject.Dispose();
        }
    }
}