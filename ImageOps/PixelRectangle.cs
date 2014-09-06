using System.Drawing;

namespace ImageOps
{
    public class PixelRectangle
    {
        private readonly int _x;
        private readonly int _y;
        private readonly int _width;
        private readonly int _height;
        private readonly int _lastX;
        private readonly int _lastY;

        public PixelRectangle(int x, int y, int width, int height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _lastX = _x + _width - 1;
            _lastY = _y + _height - 1;
        }

        public int X { get { return _x; } }
        public int Y { get { return _y; } }
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }
        public int LastX { get { return _lastX; } }
        public int LastY { get { return _lastY; } }
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
            return x >= _x && y >= _y && x <= LastX && y <= LastY;
        }

        public bool Equals(PixelRectangle other)
        {
            return _x == other._x && _y == other._y && _width == other._width && _height == other._height;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is PixelRectangle && Equals((PixelRectangle)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _x;
                hashCode = (hashCode * 397) ^ _y;
                hashCode = (hashCode * 397) ^ _width;
                hashCode = (hashCode * 397) ^ _height;
                return hashCode;
            }
        }
    }
}
