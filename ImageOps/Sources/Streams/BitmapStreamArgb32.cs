namespace ImageOps.Sources.Streams
{
    internal class BitmapStreamArgb32 : BitmapStream
    {
        private unsafe uint* _pointer;

        public unsafe BitmapStreamArgb32(IPixelSource source, BitmapLocker locker) 
            : base(source, locker)
        {
            _pointer = (uint*)Data.Scan0.ToPointer();
        }

        public override unsafe void MoveBy(int i)
        {
            _pointer += i;
        }

        public override unsafe PixelColor GetCurrent()
        {
            return new PixelColor(*_pointer);
        }
    }
}