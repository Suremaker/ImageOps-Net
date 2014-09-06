namespace ImageOps.Sources.Streams
{
    internal class CroppingStream : SourceStream<CroppedSource>
    {
        private readonly IPixelStream _stream;

        public CroppingStream(CroppedSource source)
            : base(source)
        {
            _stream = Source.OriginalSource.OpenStream();
        }

        public override void Dispose()
        {
            _stream.Dispose();
        }

        public override PixelColor Get(int x, int y)
        {
            return _stream.Get(Source.CroppedRegion.X + x, Source.CroppedRegion.Y + y);
        }
    }
}