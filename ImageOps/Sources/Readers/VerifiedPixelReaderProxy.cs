namespace ImageOps.Sources.Readers
{
    internal class VerifiedPixelReaderProxy : IVerifiedPixelReader
    {
        private readonly IPixelReader _reader;

        public VerifiedPixelReaderProxy(IPixelReader reader)
        {
            _reader = reader;
        }

        public void Dispose()
        {
            _reader.Dispose();
        }

        public PixelColor VerifiedGet(int x, int y)
        {
            return _reader.Get(x, y);
        }

        public int Width { get { return _reader.Width; } }
        public int Height { get { return _reader.Height; } }
    }
}