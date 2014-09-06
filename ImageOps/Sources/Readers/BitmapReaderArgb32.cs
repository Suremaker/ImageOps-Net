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

        public override unsafe PixelColor VerifiedGet(int x, int y)
        {
            return new PixelColor(_pointer[y * Width + x]);
        }
    }
}