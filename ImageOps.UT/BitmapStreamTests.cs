using System.Drawing.Imaging;
using NUnit.Framework;

namespace ImageOps.UT
{
	[TestFixture]
	public class Format32BppArgbBitmapStreamTests : BitmapStreamTestBase
	{
		[SetUp]
		public void SetUp()
		{
			SetUpBitmap(2, 3, PixelFormat.Format32bppArgb, (x, y) => PixelColor.FromArgb(x, y, (byte) (x + 1), (byte) (y + 1)));
		}
	}

	[TestFixture]
	public class Format32BppRgbBitmapStreamTests : BitmapStreamTestBase
	{
		[SetUp]
		public void SetUp()
		{
			SetUpBitmap(2, 3, PixelFormat.Format32bppRgb, (x, y) => PixelColor.FromArgb(255, y, (byte) (x + 1), (byte) (y + 1)));
		}
	}

	[TestFixture]
	public class Format24BppRgbBitmapStreamTests : BitmapStreamTestBase
	{
		[SetUp]
		public void SetUp()
		{
			SetUpBitmap(2, 3, PixelFormat.Format24bppRgb, (x, y) => PixelColor.FromArgb(255, y, (byte) (x + 1), (byte) (y + 1)));
		}
	}
	
}