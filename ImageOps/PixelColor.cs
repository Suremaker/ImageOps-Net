using System.Drawing;
using System.Globalization;

namespace ImageOps
{
    public struct PixelColor
    {
        public static PixelColor Transparent = new PixelColor(Color.Transparent);
        private readonly uint _argb;

        public byte A
        {
            get { return (byte)(_argb >> 24); }
        }

        public uint Argb
        {
            get { return _argb; }
        }

        public byte B
        {
            get { return (byte)_argb; }
        }

        public Color Color
        {
            get { return Color.FromArgb((int)_argb); }
        }

        public byte G
        {
            get { return (byte)(_argb >> 8); }
        }

        public byte R
        {
            get { return (byte)(_argb >> 16); }
        }

        public PixelColor(Color color)
            : this((uint)color.ToArgb())
        {
        }

        public PixelColor(uint argb)
        {
            _argb = argb;
        }

        public static PixelColor FromRgb(byte red, byte green, byte blue)
        {
            return FromArgb(255, red, green, blue);
        }

        public static PixelColor FromArgb(byte alpha, byte red, byte green, byte blue)
        {
            return new PixelColor(((uint)alpha << 24) | ((uint)red << 16) | ((uint)green << 8) | blue);
        }

        public static PixelColor FromFargb(float alpha, float red, float green, float blue)
        {
            return FromArgb((byte)(alpha * 255), (byte)(red * 255), (byte)(green * 255), (byte)(blue * 255));
        }

        public float GetAlpha()
        {
            return A / 255.0f;
        }

        public float GetBlue()
        {
            return B / 255.0f;
        }

        public float GetGreen()
        {
            return G / 255.0f;
        }

        public float GetRed()
        {
            return R / 255.0f;
        }

        public override string ToString()
        {
            return "0x" + _argb.ToString("X8", CultureInfo.InvariantCulture);
        }

        public static implicit operator PixelColor(Color color)
        {
            return new PixelColor(color);
        }

        public bool Equals(PixelColor other)
        {
            return _argb == other._argb;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is PixelColor && Equals((PixelColor)obj);
        }

        public override int GetHashCode()
        {
            return (int)_argb;
        }
    }
}