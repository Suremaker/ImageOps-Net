using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using ImageOps.Streaming.Sources.BitmapProcessing;

namespace ImageOps.Streaming.Sources
{
    public class BitmapLocker : IDisposable
    {
        private readonly Bitmap _bitmap;
        private BitmapData _bitmapData;
        private decimal _lockCounter;

        public BitmapLocker(Bitmap bitmap)
        {
            _bitmap = bitmap;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BitmapData Lock()
        {
            return (_lockCounter++) == 0 
                ? (_bitmapData = LockBitmap()) 
                : _bitmapData;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Unlock()
        {
            if (--_lockCounter == 0)
                UnlockBitmap();
        }

        public void Dispose()
        {
            UnlockBitmap();
        }

        private BitmapData LockBitmap()
        {
            return _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.ReadOnly, _bitmap.PixelFormat);
        }

        private void UnlockBitmap()
        {
            if (_bitmapData == null)
                return;
            _bitmap.UnlockBits(_bitmapData);
            _bitmapData = null;
        }
    }
    public class BitmapSource : SourceStream, IPixelSource
    {
        private readonly Bitmap _bitmap;
        private readonly BitmapLocker _bitmapLocker;
        private bool _disposed;
        private readonly IPixelPointer _pixelPointer;

        public BitmapSource(Bitmap bitmap)
        {
            _bitmap = bitmap;
            _bitmapLocker=new BitmapLocker(bitmap);
            _pixelPointer = CreatePixelPointer();
        }

        private IPixelPointer CreatePixelPointer()
        {
            switch (_bitmap.PixelFormat)
            {
                case PixelFormat.Format32bppArgb:
                    return new Argb32PixelPointer(this,_bitmapLocker);
                case PixelFormat.Format32bppRgb:
                    return new Rgb32PixelPointer(this, _bitmapLocker);
                case PixelFormat.Format24bppRgb:
                    return new Rgb24PixelPointer(this, _bitmapLocker);
                default:
                    throw new NotSupportedException(string.Format("Pixel format {0} is not supported. Use 32bppArgb, 32bppRgb or 24bppRgb.", _bitmap.PixelFormat));
            }
        }

        protected override void MoveBy(int i)
        {
            _pixelPointer.MoveBy(i);
        }

        public override int ImageWidth
        {
            get { return _bitmap.Width; }
        }

        public override int ImageHeight
        {
            get { return _bitmap.Height; }
        }

        public IPixelStream2 OpenStream()
        {
            return CreatePixelPointer();
        }

        protected override PixelColor GetCurrentPixel()
        {
            return _pixelPointer.Get();
        }

        public override void Dispose()
        {
            if (!_disposed)
                _bitmapLocker.Dispose();
            _disposed = true;
        }
    }
}
