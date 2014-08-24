namespace ImageOps.Sources.Streams
{
    internal class CroppingStream : SourceStream<CroppedSource>
    {
        private readonly IPixelStream _stream;

        public CroppingStream(CroppedSource source)
            : base(source)
        {
            _stream = Source.OriginalSource.OpenStream();
            _stream.Move(Source.BaseOffset);
        }

        public override void Dispose()
        {
            _stream.Dispose();
        }

        public override void MoveBy(int delta)
        {
            var cropPos = delta + Position;
            var expectedPos = Source.BaseOffset + cropPos + (cropPos / Source.CroppedRegion.Width) * Source.SkippedPixels;
            _stream.Move(expectedPos - _stream.Position);
        }

        public override PixelColor GetCurrent()
        {
            return _stream.GetCurrent();
        }
    }
}