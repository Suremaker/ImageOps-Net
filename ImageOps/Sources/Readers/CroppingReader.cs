namespace ImageOps.Sources.Readers
{
    internal class CroppingReader : SourceReader<CroppedSource>
    {
        private readonly IPixelReader _reader;

        public CroppingReader(CroppedSource source)
            : base(source)
        {
            _reader = Source.OriginalSource.OpenReader();
        }

        public override void Dispose()
        {
            _reader.Dispose();
        }

        protected override PixelColor FastGet(int x, int y)
        {
            return _reader.Get(Source.CroppedRegion.X + x, Source.CroppedRegion.Y + y);
        }
    }
}