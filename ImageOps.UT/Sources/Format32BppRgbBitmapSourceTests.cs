using System.Drawing.Imaging;
using ImageOps.UT.Helpers;
using NUnit.Framework;

namespace ImageOps.UT.Sources
{
    [TestFixture]
    public class Format32BppRgbBitmapSourceTests : BitmapSourceTestBase
    {
        [SetUp]
        public void SetUp()
        {
            SetUpBitmap(2, 3, PixelFormat.Format32bppRgb, (x, y) => PixelColor.FromArgb(255, y, (byte)(x + 1), (byte)(y + 1)));
        }
    }
}