namespace ImageOps.Sources.Streams
{
    internal class BitmapStreamRgb32 : BitmapStream
    {
        private unsafe uint* _pointer;

        public unsafe BitmapStreamRgb32(IPixelSource source, BitmapLocker locker)
            : base(source, locker)
        {
            _pointer = (uint*)locker.Lock().Scan0.ToPointer();
        }

        public override unsafe void MoveBy(int i)
        {
            _pointer += i;
        }

        public override unsafe PixelColor GetCurrent()
        {
            return new PixelColor(0xff000000 | *_pointer);
        }
    }
}