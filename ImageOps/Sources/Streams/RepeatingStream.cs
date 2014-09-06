namespace ImageOps.Sources.Streams
{
    internal class RepeatingStream : SourceStream<RepeatedSource>
    {
        private readonly IPixelStream _stream;

        public RepeatingStream(RepeatedSource source)
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
            return _stream.Get(x%_stream.Width, y%_stream.Height);
        }
    }
}