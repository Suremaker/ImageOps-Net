namespace ImageOps.Sources.Readers
{
    internal class BitmapReaderRgb24 : BitmapReader
    {
        private readonly unsafe byte* _pointer;
        private readonly int _dataWidth;
        private readonly int _dataStride;

        public unsafe BitmapReaderRgb24(IPixelSource source, BitmapLocker locker)
            : base(source, locker)
        {
            _pointer = (byte*)Data.Scan0.ToPointer();
            _dataWidth = Data.Width;
            _dataStride = Data.Stride;
        }

        private int GetPointerIndex(int pixelIndex)
        {
            int x = pixelIndex % _dataWidth;
            int y = pixelIndex / _dataWidth;
            return (_dataStride * y) + x * 3;
        }

        protected override unsafe PixelColor FastGet(int x, int y)
        {
            var index = GetPointerIndex(y * Width + x);
            return PixelColor.FromRgb(_pointer[index + 2], _pointer[index + 1], _pointer[index + 0]);
        }
    }
}