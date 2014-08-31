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

        public override void MoveBy(int delta)
        {
            int repeatedX = (Position + delta) % Source.ImageWidth;
            int repeatedY = (Position + delta) / Source.ImageWidth;
            int originalX = repeatedX % Source.OriginalSource.ImageWidth;
            int originalY = repeatedY % Source.OriginalSource.ImageHeight;
            int originalPos = originalY * Source.OriginalSource.ImageWidth + originalX;
            _stream.Move(originalPos - _stream.Position);
        }

        public override PixelColor GetCurrent()
        {
            return _stream.GetCurrent();
        }
    }
}