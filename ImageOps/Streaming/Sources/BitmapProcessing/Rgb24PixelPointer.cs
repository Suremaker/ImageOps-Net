using System.Drawing.Imaging;

namespace ImageOps.Streaming.Sources.BitmapProcessing
{
    internal class Rgb24PixelPointer : SourceStream2<IPixelSource>
    {
        private readonly BitmapLocker _locker;
        private unsafe byte* _pointer;
        private readonly BitmapData _data;
        private int _pixelIndex;

        public unsafe Rgb24PixelPointer(IPixelSource source, BitmapLocker locker)
            : base(source)
        {
            _locker = locker;
            _data = locker.Lock();
            _pointer = (byte*)_data.Scan0.ToPointer();
        }

        private int GetPointerIndex()
        {
            int x = _pixelIndex % _data.Width;
            int y = _pixelIndex / _data.Width;
            return (_data.Stride * y) + x * 3;
        }

        public override unsafe void Dispose()
        {
            _locker.Unlock();
            _pointer = null;
        }

        public override void MoveBy(int i)
        {
            _pixelIndex += i;
        }

        public override unsafe PixelColor GetCurrent()
        {
            var index = GetPointerIndex();
            return PixelColor.FromRgb(_pointer[index + 2], _pointer[index + 1], _pointer[index + 0]);
        }
    }
}