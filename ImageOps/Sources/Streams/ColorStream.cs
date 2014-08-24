namespace ImageOps.Sources.Streams
{
    internal class ColorStream : SourceStream<ColorSource>
    {
        public ColorStream(ColorSource source)
            : base(source)
        {
        }

        public override void Dispose()
        {
        }

        public override void MoveBy(int delta)
        {
        }

        public override PixelColor GetCurrent()
        {
            return Source.Color;
        }
    }
}