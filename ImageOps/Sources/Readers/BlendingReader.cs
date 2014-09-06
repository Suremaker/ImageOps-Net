namespace ImageOps.Sources.Readers
{
    internal class BlendingReader : SourceReader<BlendedSource>
    {
        private readonly IPixelReader _foreground;
        private readonly IPixelReader _background;

        public BlendingReader(BlendedSource source)
            : base(source)
        {
            _foreground = source.ForegroundSource.OpenReader();
            _background = source.BackgroundSource.OpenReader();
        }

        public override void Dispose()
        {
            _foreground.Dispose();
            _background.Dispose();
        }

        protected override PixelColor FastGet(int x, int y)
        {
            return Source.BlendingMethod.Blend(_background.Get(x, y), _foreground.Get(x, y));
        }
    }
}