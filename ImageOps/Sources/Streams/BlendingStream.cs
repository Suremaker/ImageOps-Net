namespace ImageOps.Sources.Streams
{
    internal class BlendingStream : SourceStream<BlendedSource>
    {
        private readonly IPixelStream _foreground;
        private readonly IPixelStream _background;

        public BlendingStream(BlendedSource source)
            : base(source)
        {
            _foreground = source.ForegroundSource.OpenStream();
            _background = source.BackgroundSource.OpenStream();
        }

        public override void Dispose()
        {
            _foreground.Dispose();
            _background.Dispose();
        }

        public override void MoveBy(int delta)
        {
            _foreground.Move(delta);
            _background.Move(delta);
        }

        public override PixelColor GetCurrent()
        {
            return Source.BlendingMethod.Blend(_background.GetCurrent(), _foreground.GetCurrent());
        }
    }
}