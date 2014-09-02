namespace ImageOps.Sources.Streams
{
    internal class ComputingStream : SourceStream<ComputedSource>
    {
        public ComputingStream(ComputedSource source)
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
            return Source.ColorFunction(Position % Source.ImageWidth, Position / Source.ImageWidth);
        }
    }
}