using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageOps
{
    public static class BitmapCreator
    {
        public static unsafe Bitmap Create(int width, int height, Func<int, int, Color> colorFn)
        {
            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData data = null;
            try
            {
                data = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bmp.PixelFormat);
                var buf = (int*) data.Scan0.ToPointer();
                for (int y = 0; y < height; ++y)
                    for (int x = 0; x < width; ++x)
                        *(buf++) = colorFn(x, y).ToArgb();
            }
            finally
            {
                if (data != null)
                    bmp.UnlockBits(data);
            }
            return bmp;
        }

        public static Bitmap Create(int width, int height, Color initialColor)
        {
            return Create(width, height, (x, y) => initialColor);
        }

        public static Bitmap Create(Color[,] pixels)
        {
            return Create(pixels.GetLength(1), pixels.GetLength(0), (x, y) => pixels[y, x]);
        }
    }
}