using System;
using System.Diagnostics;
using System.Drawing;

namespace ImageOps.Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var clouds = new Bitmap("clouds.png").AsPixelSource();

            var width = clouds.ImageWidth;
            var height = clouds.ImageHeight;

            Color.SkyBlue.AsPixelSource(width, height)
                 .Mix(clouds)
                 .ToBitmap()
                 .Save("cloudsOnBlueSky.png");

            Color.Black.AsPixelSource(width, height)
                 .Mix(clouds.Multiply(Color.PaleVioletRed.AsPixelSource(width, height)))
                 .ToBitmap()
                 .Save("redCloudsOnDarkSky.png");

            clouds.Multiply(
                Color.Red.AsPixelSource(width / 2, height / 2).Expand(0, 0, width / 2, height / 2, Color.White),
                Color.Green.AsPixelSource(width / 2, height / 2).Expand(width / 2, 0, 0, height / 2, Color.White),
                Color.Blue.AsPixelSource(width / 2, height / 2).Expand(0, height / 2, width / 2, 0, Color.White),
                Color.Yellow.AsPixelSource(width / 2, height / 2).Expand(width / 2, height / 2, 0, 0, Color.White),
                Color.Magenta.AsPixelSource(width / 2, height / 2).Expand(width / 4, height / 4, width / 4, height / 4, Color.White))
                .ToBitmap()
                .Save("skyWithRectangles.png");
        }
    }
}