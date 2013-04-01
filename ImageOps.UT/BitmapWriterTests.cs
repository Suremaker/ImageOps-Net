using System.Drawing;
using ImageOps.Sources;
using NUnit.Framework;

namespace ImageOps.UT
{
	[TestFixture]
	public class BitmapWriterTests
	{
		[Test]
		public void ShouldWriteBitmap()
		{
			var source = new Bitmap(5, 10);
			for (int x = 0; x < source.Width; ++x)
				for (int y = 0; y < source.Height; ++y)
					source.SetPixel(x, y, Color.FromArgb(x, y, x, y));

			Bitmap dest;
			using (var pixelStream = new BitmapSource(source))
				dest = pixelStream.ToBitmap();

			for (int x = 0; x < source.Width; ++x)
				for (int y = 0; y < source.Height; ++y)
					Assert.That(dest.GetPixel(x, y), Is.EqualTo(source.GetPixel(x, y)));
		}
	}
}
