using System.Drawing;

namespace ImageOps.Example
{
	class Program
	{
		static void Main(string[] args)
		{
			var clouds = new Bitmap("clouds.png");

			using (var pixelStream = Color.SkyBlue
				.AsPixelSource(clouds.Width, clouds.Height)
				.Mix(clouds.AsPixelSource()))
			{
				pixelStream.ToBitmap().Save("cloudsOnBlueSky.png");
			}

			using (var pixelStream = Color.Black
				.AsPixelSource(clouds.Width, clouds.Height)
				.Mix(clouds.AsPixelSource()
					.Multiply(Color.PaleVioletRed.AsPixelSource(clouds.Width, clouds.Height))))
			{
				pixelStream.ToBitmap().Save("redCloudsOnDarkSky.png");
			}
		}
	}
}
