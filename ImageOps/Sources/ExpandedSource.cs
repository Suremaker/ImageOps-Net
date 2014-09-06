using ImageOps.Sources.Readers;

namespace ImageOps.Sources
{
    public class ExpandedSource : IPixelSource
    {
        public IPixelSource OriginalSource { get; private set; }
        public int LeftMargin { get; private set; }
        public int TopMargin { get; private set; }
        public PixelColor ExpandedColor { get; private set; }
        public int ImageWidth { get; private set; }
        public int ImageHeight { get; private set; }

        public ExpandedSource(IPixelSource source, int leftMargin, int topMargin, int rightMargin, int bottomMargin)
            :this(source,leftMargin,topMargin,rightMargin,bottomMargin,PixelColor.Transparent)
        {
        }

        public ExpandedSource(IPixelSource source, int leftMargin, int topMargin, int rightMargin, int bottomMargin, PixelColor expandedColor)
        {
            OriginalSource = source;
            LeftMargin = leftMargin;
            TopMargin = topMargin;
            ExpandedColor = expandedColor;
            ImageWidth = leftMargin + OriginalSource.ImageWidth + rightMargin;
            ImageHeight = topMargin + OriginalSource.ImageHeight + bottomMargin;
        }

        public IPixelReader OpenReader()
        {
            return new ExpandingReader(this);
        }

        public void Dispose()
        {
            OriginalSource.Dispose();
        }
    }
}