namespace ImageOps.Sources.Streams
{
    internal class ComputingStream : SourceStream<ComputedSource>
    {
        public ComputingStream(ComputedSource source)
            : base(source)
        {
        }

        public override PixelColor Get(int x, int y)
        {
            return Source.ColorFunction(x, y);
        }
    }
}