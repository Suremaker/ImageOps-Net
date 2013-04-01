using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace ImageOps.UT
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
		public void ShouldSeekUsingCurrentOrigin()
		{
			Subject.Seek(1, SeekOrigin.Current);
			Assert.That(Subject.Position, Is.EqualTo(1));
			Subject.Seek(1, SeekOrigin.Current);
			Assert.That(Subject.Position, Is.EqualTo(2));
		}

		[Test]
		public void ShouldSeekUsingEndOrigin()
		{
			Subject.Seek(-1, SeekOrigin.End);
			Assert.That(Subject.Position, Is.EqualTo(ExpectedWidth * ExpectedHeight - 2));
		}

		[Test]
		public void ShouldSeekUsingBeginOrigin()
		{
			Subject.Seek(1, SeekOrigin.Begin);
			Assert.That(Subject.Position, Is.EqualTo(1));
			Subject.Seek(2, SeekOrigin.Begin);
			Assert.That(Subject.Position, Is.EqualTo(2));
		}

		[Test]
		public void ShouldReadFirstPixelAndMoveOnNextPosition()
		{
			Assert.That(Subject.Read(), Is.EqualTo(ExpectedColors.First()));
			Assert.That(Subject.Position, Is.EqualTo(1));
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