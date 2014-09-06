namespace ImageOps.Sources.Readers
{
    internal class ExpandingReader : SourceReader<ExpandedSource>
    {
        private readonly IPixelReader _reader;

        public ExpandingReader(ExpandedSource expandCanvas)
            : base(expandCanvas)
        {
            _reader = Source.OriginalSource.OpenReader();
        }

        public override void Dispose()
        {
            _reader.Dispose();
        }

        protected override PixelColor FastGet(int x, int y)
        {
            if (x >= Source.LeftMargin
                && x < Source.LeftMargin + Source.OriginalSource.ImageWidth
                && y >= Source.TopMargin
                && y < Source.TopMargin + Source.OriginalSource.ImageHeight)
                return _reader.Get(x - Source.LeftMargin, y - Source.TopMargin);
            return Source.ExpandedColor;
        }
    }
}