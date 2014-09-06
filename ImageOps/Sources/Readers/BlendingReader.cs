namespace ImageOps.Sources.Readers
{
    internal class BlendingReader : SourceReader<BlendedSource>
    {
        private readonly IVerifiedPixelReader _foreground;
        private readonly IVerifiedPixelReader _background;

        public BlendingReader(BlendedSource source)
            : base(source)
        {
            _foreground = source.ForegroundSource.OpenReader().InVerifiedContext();
            _background = source.BackgroundSource.OpenReader().InVerifiedContext();
        }

        public override void Dispose()
        {
            _foreground.Dispose();
            _background.Dispose();
        }

        public override PixelColor VerifiedGet(int x, int y)
        {
            return Source.BlendingMethod.Blend(_background.VerifiedGet(x, y), _foreground.VerifiedGet(x, y));
        }
    }
}