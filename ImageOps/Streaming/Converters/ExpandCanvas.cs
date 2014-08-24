namespace ImageOps.Streaming.Converters
{
    public class ExpandCanvas : SourceStream
    {
        private readonly IPixelStream _source;
        private readonly int _leftMargin;
        private readonly int _topMargin;
        private readonly int _width;
        private readonly int _height;
        private bool _isOutsideImage;

        public ExpandCanvas(IPixelStream source, int margin)
            : this(source, margin, margin)
        {
        }
        public ExpandCanvas(IPixelStream source, int horizontalMargin, int verticalMargin)
            : this(source, horizontalMargin, verticalMargin, horizontalMargin, verticalMargin)
        {
        }
        public ExpandCanvas(IPixelStream source, int leftMargin, int topMargin, int rightMargin, int bottomMargin)
        {
            _source = source;
            _leftMargin = leftMargin;
            _topMargin = topMargin;
            _width = leftMargin + _source.ImageWidth + rightMargin;
            _height = topMargin + _source.ImageHeight + bottomMargin;
            _isOutsideImage = _leftMargin > 0 || _topMargin > 0;
        }

        public override int ImageWidth
        {
            get { return _width; }
        }

        public override int ImageHeight
        {
            get { return _height; }
        }

        public override void Dispose()
        {
            _source.Dispose();
        }

        protected override void MoveBy(int i)
        {
            var canvasPos = Position + i;
            var sourceY = canvasPos / _width - _topMargin;
            var sourceX = canvasPos % _width - _leftMargin;

            _isOutsideImage = sourceX < 0 || sourceY < 0 || sourceX >= _source.ImageWidth || sourceY >= _source.ImageHeight;
            if (_isOutsideImage)
                return;

            var sourcePos = sourceY * _source.ImageWidth + sourceX;
            _source.Move(sourcePos - _source.Position);
        }

        protected override PixelColor GetCurrentPixel()
        {
            return _isOutsideImage ? PixelColor.Transparent : _source.GetCurrent();
        }
    }

    public class ExpandCanvas2 : IPixelSource
    {
        public IPixelSource Source { get; private set; }
        public int LeftMargin { get; private set; }
        public int TopMargin { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public ExpandCanvas2(IPixelSource source, int margin)
            : this(source, margin, margin)
        {
        }
        public ExpandCanvas2(IPixelSource source, int horizontalMargin, int verticalMargin)
            : this(source, horizontalMargin, verticalMargin, horizontalMargin, verticalMargin)
        {
        }
        public ExpandCanvas2(IPixelSource source, int leftMargin, int topMargin, int rightMargin, int bottomMargin)
        {
            Source = source;
            LeftMargin = leftMargin;
            TopMargin = topMargin;
            Width = leftMargin + Source.ImageWidth + rightMargin;
            Height = topMargin + Source.ImageHeight + bottomMargin;

        }

        public int ImageWidth
        {
            get { return Width; }
        }

        public int ImageHeight
        {
            get { return Height; }
        }

        public IPixelStream2 OpenStream()
        {
            return new ExpandStream(this);
        }

        public void Dispose()
        {
            Source.Dispose();
        }
    }

    public class ExpandStream : SourceStream2<ExpandCanvas2>
    {
        private bool _isOutsideImage;
        private readonly IPixelStream2 _source;

        public ExpandStream(ExpandCanvas2 expandCanvas)
            : base(expandCanvas)
        {
            _isOutsideImage = Source.LeftMargin > 0 || Source.TopMargin > 0;
            _source = Source.Source.OpenStream();
        }

        public override void Dispose()
        {
            _source.Dispose();
        }

        public override void MoveBy(int delta)
        {
            var canvasPos = Position + delta;
            var sourceY = canvasPos / Source.Width - Source.TopMargin;
            var sourceX = canvasPos % Source.Width - Source.LeftMargin;

            _isOutsideImage = sourceX < 0 || sourceY < 0 || sourceX >= Source.Source.ImageWidth || sourceY >= Source.Source.ImageHeight;
            if (_isOutsideImage)
                return;

            var sourcePos = sourceY * Source.Source.ImageWidth + sourceX;
            _source.Move(sourcePos - _source.Position);
        }

        public override PixelColor GetCurrent()
        {
            return _isOutsideImage ? PixelColor.Transparent : _source.GetCurrent();
        }
    }
}
