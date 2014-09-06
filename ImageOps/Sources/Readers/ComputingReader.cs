namespace ImageOps.Sources.Readers
{
    internal class ComputingReader : SourceReader<ComputedSource>
    {
        public ComputingReader(ComputedSource source)
            : base(source)
        {
        }

        public override PixelColor VerifiedGet(int x, int y)
        {
            return Source.ColorFunction(x, y);
        }
    }
}