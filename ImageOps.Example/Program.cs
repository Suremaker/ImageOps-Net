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
				new ColorSource(clouds.Width, clouds.Height, new PixelColor(Color.SkyBlue)), 
				new BitmapSource(clouds)))
			{
				pixelStream.ToBitmap().Save("cloudsOnBlueSky.png");
			}
		}
	}
}
