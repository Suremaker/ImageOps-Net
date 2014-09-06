namespace ImageOps.Sources.Readers
{
    internal class ExpandingReader : SourceReader<ExpandedSource>
    {
        private readonly IPixelReader _reader;
        private readonly int _leftMargin;
        private readonly int _topMargin;
        private readonly int _originalWidth;
        private readonly int _originalHeight;

        public ExpandingReader(ExpandedSource expandCanvas)
            : base(expandCanvas)
        {
            _reader = Source.OriginalSource.OpenReader();
            _leftMargin = Source.LeftMargin;
            _topMargin = Source.TopMargin;
            _originalWidth = Source.OriginalSource.ImageWidth;
            _originalHeight = Source.OriginalSource.ImageHeight;
        }

        public override void Dispose()
        {
            _reader.Dispose();
        }

        protected override PixelColor FastGet(int x, int y)
        {
            x -= _leftMargin;
            y -= _topMargin;
            if (x >= 0 && x < _originalWidth && y >= 0 && y < _originalHeight)
                return _reader.Get(x, y);
            return Source.ExpandedColor;
        }
    }
}