namespace ImageOps.Sources.Regions
{
    public interface IRegion
    {
        bool IsInside(int x, int y);
        PixelRectangle BoundingBox { get; }
    }
}