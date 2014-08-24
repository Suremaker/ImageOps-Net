using System.Drawing;

namespace ImageOps.Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var clouds = new Bitmap("clouds.png").AsPixelSource();

            Color.SkyBlue.AsPixelSource(clouds.ImageWidth, clouds.ImageHeight)
                 .Mix(clouds)
                 .ToBitmap()
                 .Save("cloudsOnBlueSky.png");

            Color.Black.AsPixelSource(clouds.ImageWidth, clouds.ImageHeight)
                 .Mix(clouds.Multiply(Color.PaleVioletRed.AsPixelSource(clouds.ImageWidth, clouds.ImageHeight)))
                 .ToBitmap()
                 .Save("redCloudsOnDarkSky.png");
        }
    }
}