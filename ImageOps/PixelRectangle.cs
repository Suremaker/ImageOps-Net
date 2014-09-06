using System.Drawing;

namespace ImageOps
{
    public struct PixelRectangle
    {
        public PixelRectangle(int x, int y, int width, int height)
            : this()
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public int Left { get { return X; } }
        public int Right { get { return X + Width - 1; } }
        public int Top { get { return Y; } }
        public int Bottom { get { return Y + Height - 1; } }

        public Point Location { get { return new Point(X, Y); } }
        public Size Size { get { return new Size(Width, Height); } }

        public override string ToString()
        {
            return string.Format("[X={0}, Y={1}, W={2}, H={3}]", X, Y, Width, Height);
        }

        public static PixelRectangle FromPoints(int left, int top, int right, int bottom)
        {
            return new PixelRectangle(left, top, right - left + 1, bottom - top + 1);
        }

        public bool IsInside(int x, int y)
        {
            return x >= Left && y >= Top && x <= Right && y <= Bottom;
        }
    }
}
