using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace ImageOps.UT
{
	[TestFixture]
	public class ArgbBitmapStreamTests
	{
		private BitmapStream _subject;
		private Bitmap _bitmap;

		[SetUp]
		public void SetUp()
		{
			_bitmap = new Bitmap(2, 3, PixelFormat.Format32bppArgb);
			for (int x = 0; x < _bitmap.Width; ++x)
				for (int y = 0; y < _bitmap.Height; ++y)
					_bitmap.SetPixel(x, y, Color.FromArgb(x, y, x + 1, y + 1));

			_subject = new BitmapStream(_bitmap);
		}

		[TearDown]
		public void TearDown()
		{
			_subject.Dispose();
			_bitmap.Dispose();
		}

		[Test]
		public void ShouldHaveProperLength()
		{
			Assert.That(_subject.TotalLength, Is.EqualTo(_bitmap.Width * _bitmap.Height));
		}

		[Test]
		public void ShouldHaveProperDimensions()
		{
			Assert.That(_subject.ImageWidth, Is.EqualTo(_bitmap.Width));
			Assert.That(_subject.ImageHeight, Is.EqualTo(_bitmap.Height));
		}

		[Test]
		public void ShouldHave0Position()
		{
			Assert.That(_subject.Position, Is.EqualTo(0));
		}

		[Test]
		public void ShouldSeekUsingCurrentOrigin()
		{
			_subject.Seek(1, SeekOrigin.Current);
			Assert.That(_subject.Position, Is.EqualTo(1));
			_subject.Seek(1, SeekOrigin.Current);
			Assert.That(_subject.Position, Is.EqualTo(2));
		}

		[Test]
		public void ShouldSeekUsingEndOrigin()
		{
			_subject.Seek(-1, SeekOrigin.End);
			Assert.That(_subject.Position, Is.EqualTo(_bitmap.Width * _bitmap.Height - 2));
		}

		[Test]
		public void ShouldSeekUsingBeginOrigin()
		{
			_subject.Seek(1, SeekOrigin.Begin);
			Assert.That(_subject.Position, Is.EqualTo(1));
			_subject.Seek(2, SeekOrigin.Begin);
			Assert.That(_subject.Position, Is.EqualTo(2));
		}

		[Test]
		public void ShouldReadFirstPixelAndMoveOnNextPosition()
		{
			Assert.That(_subject.Read(), Is.EqualTo(new PixelColor(0, 0, 1, 1)));
			Assert.That(_subject.Position, Is.EqualTo(1));
		}

		[Test]
		public void ShouldReadAllPixels()
		{
			Assert.That(_subject.ToArray(), Is.EqualTo(new[]
				{
					new PixelColor(0,0,1,1),
					new PixelColor(1,0,2,1),
					new PixelColor(0,1,1,2),
					new PixelColor(1,1,2,2),
					new PixelColor(0,2,1,3),
					new PixelColor(1,2,2,3)
				}));
		}
	}
}
