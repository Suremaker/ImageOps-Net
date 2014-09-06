namespace ImageOps.Sources.Regions
{
    public class RectangleRegion : IRegion
    {
        public RectangleRegion(int x, int y, int width, int height)
        {
            BoundingBox = new PixelRectangle(x, y, width, height);
        }

        public bool IsInside(int x, int y)
        {
            return BoundingBox.IsInside(x, y);
        }

        public PixelRectangle BoundingBox { get; private set; }
    }
}