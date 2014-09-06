using ImageOps.Sources.Readers;

namespace ImageOps.Sources
{
    public class RepeatedSource : IPixelSource
    {
        public RepeatedSource(IPixelSource originalSource, int width, int height)
        {
            OriginalSource = originalSource;
            ImageWidth = width;
            ImageHeight = height;
        }

        public IPixelSource OriginalSource { get; private set; }
        public void Dispose()
        {
            OriginalSource.Dispose();
        }

        public int ImageWidth { get; private set; }
        public int ImageHeight { get; private set; }
        public IPixelReader OpenReader()
        {
            return new RepeatingReader(this);
        }
    }
}