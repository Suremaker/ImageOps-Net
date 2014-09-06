namespace ImageOps.Sources.Readers
{
    internal class BitmapReaderArgb32 : BitmapReader
    {
        private readonly unsafe uint* _pointer;

        public unsafe BitmapReaderArgb32(IPixelSource source, BitmapLocker locker)
            : base(source, locker)
        {
            _pointer = (uint*)Data.Scan0.ToPointer();
        }

        protected override unsafe PixelColor FastGet(int x, int y)
        {
            return new PixelColor(_pointer[y * Width + x]);
        }
    }
}