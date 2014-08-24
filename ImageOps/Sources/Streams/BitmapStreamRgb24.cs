namespace ImageOps.Sources.Streams
{
    internal class BitmapStreamRgb24 : BitmapStream
    {
        private readonly unsafe byte* _pointer;
        private int _pixelIndex;

        public unsafe BitmapStreamRgb24(IPixelSource source, BitmapLocker locker)
            : base(source, locker)
        {
            _pointer = (byte*)Data.Scan0.ToPointer();
        }

        private int GetPointerIndex()
        {
            int x = _pixelIndex % Data.Width;
            int y = _pixelIndex / Data.Width;
            return (Data.Stride * y) + x * 3;
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