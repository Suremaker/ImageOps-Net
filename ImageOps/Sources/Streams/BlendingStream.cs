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

        public override PixelColor Get(int x, int y)
        {
            return Source.BlendingMethod.Blend(_background.Get(x, y), _foreground.Get(x, y));
        }
    }
}