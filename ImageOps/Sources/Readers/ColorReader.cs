namespace ImageOps.Sources.Readers
{
    internal class ColorReader : SourceReader<ColorSource>
    {
        public ColorReader(ColorSource source)
            : base(source)
        {
        }

        public override PixelColor VerifiedGet(int x, int y)
        {
            return Source.Color;
        }
    }
}