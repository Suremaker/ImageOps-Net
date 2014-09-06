namespace ImageOps.Sources.Readers
{
    internal class CroppingReader : SourceReader<CroppedSource>
    {
        private readonly IPixelReader _reader;
        private readonly int _leftMargin;
        private readonly int _topMargin;

        public CroppingReader(CroppedSource source)
            : base(source)
        {
            _reader = Source.OriginalSource.OpenReader();
            _leftMargin = Source.CroppedRegion.X;
            _topMargin = Source.CroppedRegion.Y;
        }

        public override void Dispose()
        {
            _reader.Dispose();
        }

        protected override PixelColor FastGet(int x, int y)
        {
            return _reader.Get(_leftMargin + x, _topMargin + y);
        }
    }
}