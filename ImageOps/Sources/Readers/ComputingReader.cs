namespace ImageOps.Sources.Readers
{
    internal class ComputingReader : SourceReader<ComputedSource>
    {
        public ComputingReader(ComputedSource source)
            : base(source)
        {
        }

        protected override PixelColor FastGet(int x, int y)
        {
            return Source.ColorFunction(x, y);
        }
    }
}