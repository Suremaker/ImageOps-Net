namespace ImageOps.Sources.Streams
{
    internal class BitmapStreamRgb32 : BitmapStream
    {
        private readonly unsafe uint* _pointer;

        public unsafe BitmapStreamRgb32(IPixelSource source, BitmapLocker locker)
            : base(source, locker)
        {
            _pointer = (uint*)locker.Lock().Scan0.ToPointer();
        }

        public override unsafe PixelColor Get(int x, int y)
        {
            return new PixelColor(0xff000000 | _pointer[y*Width + x]);
        }
    }
}