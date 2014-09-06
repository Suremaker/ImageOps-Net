namespace ImageOps.Sources.Streams
{
    internal class ColorStream : SourceStream<ColorSource>
    {
        public ColorStream(ColorSource source)
            : base(source)
        {
        }

        public override PixelColor Get(int x, int y)
        {
            return Source.Color;
        }
    }
}