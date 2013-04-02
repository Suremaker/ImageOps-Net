using System.Drawing;
using ImageOps.Blending;
using ImageOps.Sources;

namespace ImageOps.Example
{
	class Program
	{
		static void Main(string[] args)
		{
			var clouds = new Bitmap("clouds.png");

			using (var pixelStream = new NormalBlend(
				new ColorSource(clouds.Width, clouds.Height, Color.SkyBlue),
				new BitmapSource(clouds)))
			{
				pixelStream.ToBitmap().Save("cloudsOnBlueSky.png");
			}

			using (var pixelStream = new NormalBlend(
				new ColorSource(clouds.Width, clouds.Height, Color.Black),
				new MultiplyBlend(
					new BitmapSource(clouds),
					new ColorSource(clouds.Width, clouds.Height, Color.PaleVioletRed))))
			{
				pixelStream.ToBitmap().Save("redCloudsOnDarkSky.png");
			}
		}
	}
}
