using System.Drawing;
using System.Drawing.Imaging;

namespace ImageOps
{
	public static class BitmapWriter
	{
		public static unsafe Bitmap ToBitmap(this IPixelStream stream)
		{
			using (stream)
			{
				var bmp = new Bitmap(stream.ImageWidth, stream.ImageHeight);
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
