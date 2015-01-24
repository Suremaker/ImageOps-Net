using System;

namespace ImageOps.Sources.Readers
{
    internal class ProcessingReader : SourceReader<ProcessedSource>
    {
        private readonly IVerifiedPixelReader _reader;
        private readonly Func<PixelColor, PixelColor> _colorFunction;

        public ProcessingReader(ProcessedSource source)
            : base(source)
        {
            _reader = Source.OriginalSource.OpenReader().InVerifiedContext();
            _colorFunction = Source.ColorFunction;
        }

        public override void Dispose()
        {
            _reader.Dispose();
        }

        public override PixelColor VerifiedGet(int x, int y)
        {
            return _colorFunction(_reader.VerifiedGet(x, y));
        }
    }
}