namespace ImageOps.Sources.Streams
{
    internal class ExpandingStream : SourceStream<ExpandedSource>
    {
        private readonly IPixelStream _stream;

        public ExpandingStream(ExpandedSource expandCanvas)
            : base(expandCanvas)
        {
            _stream = Source.OriginalSource.OpenStream();
        }

        public override void Dispose()
        {
            _stream.Dispose();
        }

        public override PixelColor Get(int x, int y)
        {
            if (x >= Source.LeftMargin
                && x < Source.LeftMargin + Source.OriginalSource.ImageWidth
                && y >= Source.TopMargin
                && y < Source.TopMargin + Source.OriginalSource.ImageHeight)
                return _stream.Get(x - Source.LeftMargin, y - Source.TopMargin);
            return Source.ExpandedColor;
        }
    }
}