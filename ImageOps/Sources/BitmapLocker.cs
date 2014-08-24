using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;

namespace ImageOps.Sources
{
    internal class BitmapLocker : IDisposable
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
            return _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.ReadOnly,
                                    _bitmap.PixelFormat);
        }

        private void UnlockBitmap()
        {
            if (_bitmapData == null)
                return;
            _bitmap.UnlockBits(_bitmapData);
            _bitmapData = null;
        }
    }
}