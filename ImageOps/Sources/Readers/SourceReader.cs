namespace ImageOps.Sources.Readers
{
    public abstract class SourceReader<TSource> : PixelReader where TSource : IPixelSource
    {
        protected TSource Source { get; private set; }

        protected SourceReader(TSource source)
            : base(source.ImageWidth, source.ImageHeight)
        {
            Source = source;
        }
    }
}