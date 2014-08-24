using ImageOps.Sources.Streams;

namespace ImageOps.Sources
{
    public class ExpandedSource : IPixelSource
    {
        public IPixelSource OriginalSource { get; private set; }
        public int LeftMargin { get; private set; }
        public int TopMargin { get; private set; }
        public int ImageWidth { get; private set; }
        public int ImageHeight { get; private set; }

        public ExpandedSource(IPixelSource source, int margin)
            : this(source, margin, margin)
        {
        }

        public ExpandedSource(IPixelSource source, int horizontalMargin, int verticalMargin)
            : this(source, horizontalMargin, verticalMargin, horizontalMargin, verticalMargin)
        {
        }

        public ExpandedSource(IPixelSource source, int leftMargin, int topMargin, int rightMargin, int bottomMargin)
        {
            OriginalSource = source;
            LeftMargin = leftMargin;
            TopMargin = topMargin;
            ImageWidth = leftMargin + OriginalSource.ImageWidth + rightMargin;
            ImageHeight = topMargin + OriginalSource.ImageHeight + bottomMargin;
        }

        public IPixelStream OpenStream()
        {
            return new ExpandingStream(this);
        }

        public void Dispose()
        {
            OriginalSource.Dispose();
        }
    }
}