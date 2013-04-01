namespace ImageOps
{
	public class ColorSource : PixelStream
	{
		private readonly int _width;
		private readonly int _height;
		private readonly PixelColor _color;

		public ColorSource(int width, int height, PixelColor color)
		{
			_width = width;
			_height = height;
			_color = color;
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
		}

		protected override void MoveBy(int i)
		{
		}

		protected override PixelColor ReadPixel()
		{
			return _color;
		}
	}
}