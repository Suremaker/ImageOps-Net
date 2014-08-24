using System.Drawing;
using System.Drawing.Imaging;
using ImageOps.Sources;

namespace ImageOps
{
    public static class BitmapWriter
    {
        public static unsafe Bitmap ToBitmap(this IPixelSource source)
        {
            using (var stream = source.OpenStream())
            {
                var bmp = new Bitmap(source.ImageWidth, source.ImageHeight);
                var data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                var buffer = (uint*)data.Scan0.ToPointer();

                foreach (var color in stream)
                    *(buffer++) = color.Argb;

                bmp.UnlockBits(data);
                return bmp;
            }
        }
    }
}