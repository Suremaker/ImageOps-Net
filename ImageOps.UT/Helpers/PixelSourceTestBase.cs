using System;
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
            var reader = Subject.OpenReader();
            Assert.That(reader.Width, Is.EqualTo(ExpectedWidth));
            Assert.That(reader.Height, Is.EqualTo(ExpectedHeight));
        }

        [Test]
        public void ShouldOpenStreamAndReturnProperPixels()
        {
            var reader = Subject.OpenReader();
            for (int x = 0; x < ExpectedWidth; ++x)
                for (int y = 0; y < ExpectedHeight; ++y)
                    Assert.That(reader.Get(x, y), Is.EqualTo(ExpectedColors[y * ExpectedWidth + x]));
        }

        [Test]
        public void ShouldOpenStreamAndReadAllPixels()
        {
            Assert.That(Subject.OpenReader().AsEnumerable().ToArray(), Is.EqualTo(ExpectedColors));
        }

        [Test]
        public void ShouldNotAllowToAccessPixelsFromOutsideOfBoundaries()
        {
            var reader = Subject.OpenReader();
            Assert.Throws<ArgumentOutOfRangeException>(() => reader.Get(-1, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => reader.Get(0, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => reader.Get(ExpectedWidth, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => reader.Get(0, ExpectedHeight));
        }

        [TearDown]
        public void PixelStreamTearDown()
        {
            Subject.Dispose();
        }
    }
}