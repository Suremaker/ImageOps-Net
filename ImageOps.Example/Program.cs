using System;
using System.Diagnostics;
using System.Drawing;
using ImageOps.Blenders;
using ImageOps.Sources.Regions;

namespace ImageOps.Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var clouds = new Bitmap("clouds.png").AsPixelSource();

            var width = clouds.ImageWidth;
            var height = clouds.ImageHeight;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Color.SkyBlue.AsPixelSource(width, height)
                 .Mix(clouds)
                 .ToBitmap()
                 .Save("cloudsOnBlueSky.png");
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            sw.Start();
            Color.Black.AsPixelSource(width, height)
                 .Mix(clouds.Multiply(Color.PaleVioletRed.AsPixelSource(width, height)))
                 .ToBitmap()
                 .Save("redCloudsOnDarkSky.png");
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            sw.Start();
            clouds.Multiply(
                Color.Red.AsPixelSource(width / 2, height / 2).Expand(0, 0, width / 2, height / 2, Color.White),
                Color.Green.AsPixelSource(width / 2, height / 2).Expand(width / 2, 0, 0, height / 2, Color.White),
                Color.Blue.AsPixelSource(width / 2, height / 2).Expand(0, height / 2, width / 2, 0, Color.White),
                Color.Yellow.AsPixelSource(width / 2, height / 2).Expand(width / 2, height / 2, 0, 0, Color.White),
                Color.Magenta.AsPixelSource(width / 2, height / 2).Expand(width / 4, height / 4, width / 4, height / 4, Color.White))
                .ToBitmap()
                .Save("skyWithRectangles.png");
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            sw.Start();
            clouds.BlendRegion(Regions.Rectangle(0, 0, width / 2, height / 2), Color.Red.AsPixelSource(width / 2, height / 2), BlendingMethods.Multiply)
                .BlendRegion(Regions.Rectangle(width / 2, 0, width / 2, height / 2), Color.Green.AsPixelSource(width / 2, height / 2), BlendingMethods.Multiply)
                .BlendRegion(Regions.Rectangle(0, height / 2, width / 2, height / 2), Color.Blue.AsPixelSource(width / 2, height / 2), BlendingMethods.Multiply)
                .BlendRegion(Regions.Rectangle(width / 2, height / 2, width / 2, height / 2), Color.Yellow.AsPixelSource(width / 2, height / 2), BlendingMethods.Multiply)
                .BlendRegion(Regions.Rectangle(width / 4, height / 4, width / 2, height / 2), Color.Magenta.AsPixelSource(width / 2, height / 2), BlendingMethods.Multiply)
                .ToBitmap()
                .Save("skyWithRectangles2.png");
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            sw.Start();
            clouds.BlendRegion(new PolygonRegion(new Point(0, 0), new Point(width / 2 - 1, 0), new Point(width / 2 - 1, height / 2 - 1), new Point(width / 2 - 15, height / 2 - 25)), Color.Red.AsPixelSource(width / 2, height / 2), BlendingMethods.Multiply)
                .BlendRegion(new PolygonRegion(new Point(width / 2, 0), new Point(width - 1, 0), new Point(width - 1, height / 2 - 1)), Color.Green.AsPixelSource(width / 2, height / 2), BlendingMethods.Multiply)
                .BlendRegion(new PolygonRegion(new Point(0, height / 2), new Point(width / 2 - 1, height / 2), new Point(width / 2 - 1, height - 1)), Color.Blue.AsPixelSource(width / 2, height / 2), BlendingMethods.Multiply)
                .BlendRegion(new PolygonRegion(new Point(width / 2, height / 2), new Point(width - 1, height / 2), new Point(width - 1, height - 1)), Color.Yellow.AsPixelSource(width / 2, height / 2), BlendingMethods.Multiply)
                .BlendRegion(new PolygonRegion(new Point(width / 4, height / 4), new Point(3 * width / 4 - 1, height / 4), new Point(3 * width / 4 - 1, 3 * height / 4 - 1)), Color.Magenta.AsPixelSource(width / 2, height / 2), BlendingMethods.Multiply)
                .ToBitmap()
                .Save("skyWithRectangles3.png");
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            sw.Start();
            clouds.BlendRegion(Regions.Rectangle(0, 0, width / 2, height / 2), Color.Red.AsPixelSource(width / 2, height / 2), BlendingMethods.Multiply)
                .BlendRegion(Regions.Rectangle(width / 2, 0, width / 2, height / 2), Color.Green.AsPixelSource(width / 2, height / 2), BlendingMethods.Multiply)
                .BlendRegion(Regions.Rectangle(0, height / 2, width / 2, height / 2), Color.Blue.AsPixelSource(width / 2, height / 2), BlendingMethods.Multiply)
                .BlendRegion(Regions.Rectangle(width / 2, height / 2, width / 2, height / 2), Color.Yellow.AsPixelSource(width / 2, height / 2), BlendingMethods.Multiply)
                .BlendRegion(Regions.Rectangle(width / 4, height / 4, width / 2, height / 2), Color.Magenta.AsPixelSource(width / 2, height / 2), BlendingMethods.Multiply)
                .RepeatSource(width * 2, height * 3)
                .ToBitmap()
                .Save("skyWithRectangles4.png");
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            Console.ReadLine();
        }
    }
}