namespace ImageOps.Sources.Readers
{
    internal class RepeatingReader : SourceReader<RepeatedSource>
    {
        private readonly IPixelReader _reader;
        private readonly double _widthMul;
        private readonly double _heightMul;

        public RepeatingReader(RepeatedSource source)
            : base(source)
        {
            _reader = Source.OriginalSource.OpenReader();
            _widthMul = 1.0 / _reader.Width;
            _heightMul = 1.0 / _reader.Height;
        }

        public override void Dispose()
        {
            _reader.Dispose();
        }

        protected override PixelColor FastGet(int x, int y)
        {
            return _reader.Get(Modulo(x, _widthMul, _reader.Width), Modulo(y, _heightMul, _reader.Height));
        }

        private static int Modulo(int value, double multiplierInverse, int multiplier)
        {
            return value - multiplier * (int)(value * multiplierInverse);
        }
    }
}