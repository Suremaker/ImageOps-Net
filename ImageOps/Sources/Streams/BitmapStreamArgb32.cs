namespace ImageOps.Sources.Streams
{
    internal class BitmapStreamArgb32 : BitmapStream
    {
        private readonly unsafe uint* _pointer;

        public unsafe BitmapStreamArgb32(IPixelSource source, BitmapLocker locker) 
            : base(source, locker)
        {
            _pointer = (uint*)Data.Scan0.ToPointer();
        }

        public override unsafe PixelColor Get(int x, int y)
        {
            return new PixelColor(_pointer[y*Width+x]);
        }
    }
}