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
			_source.Move(_source.Position - sourcePos);
		}

		protected override PixelColor GetCurrentPixel()
		{
			return _isOutsideImage ? PixelColor.Transparent : _source.GetCurrent();
		}
	}
}
