namespace ImageOps.Sources.Readers
{
    internal class ColorReader : SourceReader<ColorSource>
    {
        public ColorReader(ColorSource source)
            : base(source)
        {
        }

        protected override PixelColor FastGet(int x, int y)
        {
            return Source.Color;
        }
    }
}