using System.Drawing.Imaging;

namespace ImageOps.Streaming.Sources.BitmapProcessing
{
    internal class Rgb32PixelPointer : SourceStream2<IPixelSource>, IPixelPointer
	{
        private readonly BitmapLocker _locker;
        private unsafe uint* _pointer;

        public unsafe Rgb32PixelPointer(IPixelSource source, BitmapLocker locker):base(source)
        {
            _locker = locker;
            _pointer = (uint*)locker.Lock().Scan0.ToPointer();
        }

        public unsafe PixelColor Get()
		{
			return new PixelColor(0xff000000 | *_pointer);
		}

        public override unsafe void Dispose()
        {
            _locker.Unlock();
            _pointer = null;
        }

        public override unsafe void MoveBy(int i)
		{
			_pointer += i;
		}

        public override PixelColor GetCurrent()
        {
            return Get();
        }
	}
}