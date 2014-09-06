using System.Drawing;
using System.Drawing.Imaging;
using ImageOps.Sources;

namespace ImageOps
{
    public static class BitmapWriter
    {
        public static unsafe Bitmap ToBitmap(this IPixelSource source)
        {
            using (var reader = source.OpenReader())
            {
                var bmp = new Bitmap(source.ImageWidth, source.ImageHeight);
                var data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                var buffer = (uint*)data.Scan0.ToPointer();

                var width = reader.Width;
                for (int y = reader.Height - 1; y >= 0; y -= 1)
                    for (int x = width - 1; x >= 0; x -= 1)
                        buffer[y * width + x] = reader.Get(x, y).Argb;

                bmp.UnlockBits(data);
                return bmp;
            }
        }
    }
}