namespace ImageOps.Sources.Readers
{
    internal class ExpandingReader : SourceReader<ExpandedSource>
    {
        private readonly IVerifiedPixelReader _reader;
        private readonly int _leftMargin;
        private readonly int _topMargin;
        private readonly int _originalWidth;
        private readonly int _originalHeight;

        public ExpandingReader(ExpandedSource expandCanvas)
            : base(expandCanvas)
        {
            _reader = Source.OriginalSource.OpenReader().InVerifiedContext();
            _leftMargin = Source.LeftMargin;
            _topMargin = Source.TopMargin;
            _originalWidth = Source.OriginalSource.ImageWidth;
            _originalHeight = Source.OriginalSource.ImageHeight;
        }

        public override void Dispose()
        {
            _reader.Dispose();
        }

        public override PixelColor VerifiedGet(int x, int y)
        {
            x -= _leftMargin;
            y -= _topMargin;
            if (x >= 0 && x < _originalWidth && y >= 0 && y < _originalHeight)
                return _reader.VerifiedGet(x, y);
            return Source.ExpandedColor;
        }
    }
}