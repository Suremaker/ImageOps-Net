﻿using System.Drawing;
using System.Drawing.Imaging;
using ImageOps.PerformanceTests.Helpers;
using ImageOps.Sources;

namespace ImageOps.PerformanceTests.Scenarios
{
    public class BitmapSource32bppRgb: SourceTestCase
    {
        protected override IPixelSource CreateSource(int width, int height)
        {
            return new Bitmap(width, height, PixelFormat.Format32bppRgb).AsPixelSource();
        }
    }
}