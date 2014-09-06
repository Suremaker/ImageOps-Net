using System;
using System.Drawing;
using System.Drawing.Imaging;
using ImageOps.Sources.Readers;

namespace ImageOps.Sources
{
    public class BitmapSource : IPixelSource
    {
        public Bitmap Bitmap { get; private set; }
        private readonly BitmapLocker _bitmapLocker;
        private bool _disposed;

        public BitmapSource(Bitmap bitmap)
        {
            Bitmap = bitmap;
            ImageWidth = bitmap.Width;
            ImageHeight = bitmap.Height;
            _bitmapLocker = new BitmapLocker(bitmap);
        }

        public int ImageWidth { get; private set; }
        public int ImageHeight { get; private set; }

        public IPixelReader OpenReader()
        {
            switch (Bitmap.PixelFormat)
            {
                case PixelFormat.Format32bppArgb:
                    return new BitmapReaderArgb32(this, _bitmapLocker);
                case PixelFormat.Format32bppRgb:
                    return new BitmapReaderRgb32(this, _bitmapLocker);
                case PixelFormat.Format24bppRgb:
                    return new BitmapReaderRgb24(this, _bitmapLocker);
                default:
                    throw new NotSupportedException(
                        string.Format("Pixel format {0} is not supported. Use 32bppArgb, 32bppRgb or 24bppRgb.",
                                      Bitmap.PixelFormat));
            }
        }

        public void Dispose()
        {
            if (!_disposed)
                _bitmapLocker.Dispose();
            _disposed = true;
        }
    }
}