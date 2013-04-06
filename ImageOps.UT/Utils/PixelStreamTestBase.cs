using System.Collections.Generic;
using System.Linq;
using ImageOps.Sources;
using NUnit.Framework;

namespace ImageOps.UT.Utils
{
	public abstract class PixelStreamTestBase
	{
		protected IPixelStream Subject;
		protected List<PixelColor> ExpectedColors;
		protected int ExpectedWidth;
		protected int ExpectedHeight;

		[Test]
		public void ShouldHaveProperLength()
		{
			Assert.That(Subject.TotalLength, Is.EqualTo(ExpectedWidth * ExpectedHeight));
		}

		[Test]
		public void ShouldHaveProperDimensions()
		{
			Assert.That(Subject.ImageWidth, Is.EqualTo(ExpectedWidth));
			Assert.That(Subject.ImageHeight, Is.EqualTo(ExpectedHeight));
		}

		[Test]
		public void ShouldHave0Position()
		{
			Assert.That(Subject.Position, Is.EqualTo(0));
		}

		[Test]
		public void ShouldMove()
		{
			Subject.Move(1);
			Assert.That(Subject.Position, Is.EqualTo(1));
			Subject.Move(1);
			Assert.That(Subject.Position, Is.EqualTo(2));
			Subject.Move(-1);
			Assert.That(Subject.Position, Is.EqualTo(1));
		}

		[Test]
		public void ShouldGetFirstPixelAndStayOnTheSamePosition()
		{
			Assert.That(Subject.GetCurrent(), Is.EqualTo(ExpectedColors.First()));
			Assert.That(Subject.Position, Is.EqualTo(0));
		}

		[Test]
		public void ShouldReadAllPixels()
		{
			Assert.That(Subject.ToArray(), Is.EqualTo(ExpectedColors));
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