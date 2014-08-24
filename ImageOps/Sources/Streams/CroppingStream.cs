namespace ImageOps.Sources.Streams
{
    internal class CroppingStream : SourceStream<CroppedSource>
    {
        private readonly IPixelStream _source;

        public CroppingStream(CroppedSource source)
            : base(source)
        {
            _source = Source.OriginalSource.OpenStream();
            _source.Move(Source.BaseOffset);
        }

        public override void Dispose()
        {
            _source.Dispose();
        }

        public override void MoveBy(int delta)
        {
            var cropPos = delta + Position;
            var expectedPos = Source.BaseOffset + cropPos + (cropPos / Source.CroppedRegion.Width) * Source.SkippedPixels;
            _source.Move(expectedPos - _source.Position);
        }

        public override PixelColor GetCurrent()
        {
            return _source.GetCurrent();
        }
    }
}