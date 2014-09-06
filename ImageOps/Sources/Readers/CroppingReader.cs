namespace ImageOps.Sources.Readers
{
    internal class CroppingReader : SourceReader<CroppedSource>
    {
        private readonly IVerifiedPixelReader _reader;
        private readonly int _leftMargin;
        private readonly int _topMargin;

        public CroppingReader(CroppedSource source)
            : base(source)
        {
            _reader = Source.OriginalSource.OpenReader().InVerifiedContext();
            _leftMargin = Source.CroppedRegion.X;
            _topMargin = Source.CroppedRegion.Y;
        }

        public override void Dispose()
        {
            _reader.Dispose();
        }

        public override PixelColor VerifiedGet(int x, int y)
        {
            return _reader.VerifiedGet(_leftMargin + x, _topMargin + y);
        }
    }
}