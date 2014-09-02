using System.Drawing;
using ImageOps.Sources;

namespace ImageOps.PerformanceTests.Helpers
{
    public abstract class SourceTestCase : TestCase
    {
        private readonly IPixelSource _source;

        protected SourceTestCase()
            : base(100)
        {
            _source = CreateSource(1024, 1024);
        }

        protected abstract IPixelSource CreateSource(int width, int height);
        protected override Bitmap Run()
        {
            return _source.ToBitmap();
        }
    }
}