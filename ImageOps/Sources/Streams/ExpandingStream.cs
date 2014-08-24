namespace ImageOps.Sources.Streams
{
    internal class ExpandingStream : SourceStream<ExpandedSource>
    {
        private bool _isOutsideImage;
        private readonly IPixelStream _source;

        public ExpandingStream(ExpandedSource expandCanvas)
            : base(expandCanvas)
        {
            _isOutsideImage = Source.LeftMargin > 0 || Source.TopMargin > 0;
            _source = Source.OriginalSource.OpenStream();
        }

        public override void Dispose()
        {
            _source.Dispose();
        }

        public override void MoveBy(int delta)
        {
            var canvasPos = Position + delta;
            var sourceY = canvasPos/Source.ImageWidth - Source.TopMargin;
            var sourceX = canvasPos%Source.ImageWidth - Source.LeftMargin;

            _isOutsideImage = sourceX < 0 || sourceY < 0 || sourceX >= Source.OriginalSource.ImageWidth ||
                              sourceY >= Source.OriginalSource.ImageHeight;
            if (_isOutsideImage)
                return;

            var sourcePos = sourceY*Source.OriginalSource.ImageWidth + sourceX;
            _source.Move(sourcePos - _source.Position);
        }

        public override PixelColor GetCurrent()
        {
            return _isOutsideImage ? PixelColor.Transparent : _source.GetCurrent();
        }
    }
}