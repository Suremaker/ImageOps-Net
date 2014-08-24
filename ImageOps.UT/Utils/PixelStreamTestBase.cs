using System.Collections.Generic;
using System.Linq;
using ImageOps.Streaming.Sources;
using NUnit.Framework;

namespace ImageOps.UT.Utils
{
    public abstract class PixelStreamTestBase
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
        public void ShouldOpenStreamWithProperLength()
        {
            Assert.That(Subject.OpenStream().Length, Is.EqualTo(ExpectedWidth * ExpectedHeight));
        }

        [Test]
        public void ShouldOpenStreamWith0Position()
        {
            Assert.That(Subject.OpenStream().Position, Is.EqualTo(0));
        }

        [Test]
        public void ShouldOpenStreamWithAbilityToMove()
        {
            var stream = Subject.OpenStream();
            stream.Move(1);
            Assert.That(stream.Position, Is.EqualTo(1));
            stream.Move(1);
            Assert.That(stream.Position, Is.EqualTo(2));
            stream.Move(-1);
            Assert.That(stream.Position, Is.EqualTo(1));
        }

        [Test]
        public void ShouldOpenStreamAndGetFirstPixelAndStayOnTheSamePosition()
        {
            var stream = Subject.OpenStream();
            Assert.That(stream.GetCurrent(), Is.EqualTo(ExpectedColors.First()));
            Assert.That(stream.Position, Is.EqualTo(0));
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

    [TestFixture]
    public class ColorSourceTests : PixelStreamTestBase
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