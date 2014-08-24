using System.Drawing.Imaging;

namespace ImageOps.Streaming.Sources.BitmapProcessing
{
    public class Argb32PixelPointer : SourceStream2<IPixelSource>
    {
        private readonly BitmapLocker _locker;
        private unsafe uint* _pointer;

        public unsafe Argb32PixelPointer(IPixelSource source, BitmapLocker locker)
            : base(source)
        {
            _locker = locker;
            _pointer = (uint*)locker.Lock().Scan0.ToPointer();
        }

        public override unsafe void MoveBy(int i)
        {
            _pointer += i;
        }

        public override unsafe void Dispose()
        {
            _locker.Unlock();
            _pointer = null;
        }
        public override unsafe PixelColor GetCurrent()
        {
            return new PixelColor(*_pointer);
        }
    }
}