namespace ImageOps.Sources.Readers
{
    internal class RepeatingReader : SourceReader<RepeatedSource>
    {
        private readonly IPixelReader _reader;

        public RepeatingReader(RepeatedSource source)
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
            return _reader.Get(x%_reader.Width, y%_reader.Height);
        }
    }
}