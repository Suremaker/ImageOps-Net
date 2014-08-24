using System.Drawing.Imaging;
using ImageOps.UT.Helpers;
using NUnit.Framework;

namespace ImageOps.UT.Sources
{
    [TestFixture]
    public class Format32BppArgbBitmapSourceTests : BitmapSourceTestBase
    {
        [SetUp]
        public void SetUp()
        {
            SetUpBitmap(2, 3, PixelFormat.Format32bppArgb, (x, y) => PixelColor.FromArgb(x, y, (byte)(x + 1), (byte)(y + 1)));
        }
    }
}