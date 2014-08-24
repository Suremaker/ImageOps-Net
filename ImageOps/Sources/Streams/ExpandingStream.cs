namespace ImageOps.Sources.Streams
{
    internal class ExpandingStream : SourceStream<ExpandedSource>
    {
        private bool _isOutsideImage;
        private readonly IPixelStream _stream;

        public ExpandingStream(ExpandedSource expandCanvas)
            : base(expandCanvas)
        {
            _isOutsideImage = Source.LeftMargin > 0 || Source.TopMargin > 0;
            _stream = Source.OriginalSource.OpenStream();
        }

        public override void Dispose()
        {
            _stream.Dispose();
        }

        public override void MoveBy(int delta)
        {
            var canvasPos = Position + delta;
            var sourceY = canvasPos / Source.ImageWidth - Source.TopMargin;
            var sourceX = canvasPos % Source.ImageWidth - Source.LeftMargin;

            _isOutsideImage = sourceX < 0 || sourceY < 0 || sourceX >= Source.OriginalSource.ImageWidth ||
                              sourceY >= Source.OriginalSource.ImageHeight;
            if (_isOutsideImage)
                return;

            var sourcePos = sourceY * Source.OriginalSource.ImageWidth + sourceX;
            _stream.Move(sourcePos - _stream.Position);
        }

        public override PixelColor GetCurrent()
        {
            return _isOutsideImage ? Source.ExpandedColor : _stream.GetCurrent();
        }
    }
}