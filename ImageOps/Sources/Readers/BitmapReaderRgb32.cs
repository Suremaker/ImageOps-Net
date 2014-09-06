namespace ImageOps.Sources.Readers
{
    internal class BitmapReaderRgb32 : BitmapReader
    {
        private readonly unsafe uint* _pointer;

        public unsafe BitmapReaderRgb32(IPixelSource source, BitmapLocker locker)
            : base(source, locker)
        {
            _pointer = (uint*)locker.Lock().Scan0.ToPointer();
        }

        protected override unsafe PixelColor FastGet(int x, int y)
        {
            return new PixelColor(0xff000000 | _pointer[y * Width + x]);
        }
    }
}