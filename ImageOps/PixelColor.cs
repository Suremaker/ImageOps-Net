﻿using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

namespace ImageOps
{
    [StructLayout(LayoutKind.Explicit)]
    public struct PixelColor
    {
        private const float TO_FLOAT_MULTIPLIER = 1.0f / 255;
        public static PixelColor Transparent = new PixelColor(Color.Transparent);
        [FieldOffset(0)]
        private readonly uint _argb;
        [FieldOffset(3)]
        private readonly byte _a;
        [FieldOffset(2)]
        private readonly byte _r;
        [FieldOffset(1)]
        private readonly byte _g;
        [FieldOffset(0)]
        private readonly byte _b;

        public byte A
        {
            get { return _a; }
        }

        public uint Argb
        {
            get { return _argb; }
        }

        public byte B
        {
            get { return _b; }
        }

        public Color Color
        {
            get { return Color.FromArgb((int)_argb); }
        }

        public byte G
        {
            get { return _g; }
        }

        public byte R
        {
            get { return _r; }
        }

        public PixelColor(Color color)
            : this((uint)color.ToArgb())
        {
        }

        public PixelColor(uint argb)
            : this()
        {
            _argb = argb;
        }

        public PixelColor(byte alpha, byte red, byte green, byte blue)
            : this()
        {
            _a = alpha;
            _b = blue;
            _r = red;
            _g = green;
        }

        public static PixelColor FromRgb(byte red, byte green, byte blue)
        {
            return FromArgb(255, red, green, blue);
        }

        public static PixelColor FromArgb(byte alpha, byte red, byte green, byte blue)
        {
            return new PixelColor(alpha, red, green, blue);
        }

        public static PixelColor FromGrayscale(byte value)
        {
            return FromRgb(value, value, value);
        }

        public static PixelColor FromGrayscale(byte alpha, byte value)
        {
            return FromArgb(alpha, value, value, value);
        }

        public static PixelColor FromFGrayscale(float alpha, float value)
        {
            return FromFargb(alpha, value, value, value);
        }

        public static PixelColor FromFargb(float alpha, float red, float green, float blue)
        {
            return FromArgb((byte)(alpha * 255), (byte)(red * 255), (byte)(green * 255), (byte)(blue * 255));
        }

        public float GetAlpha()
        {
            return A * TO_FLOAT_MULTIPLIER;
        }

        public float GetBlue()
        {
            return B * TO_FLOAT_MULTIPLIER;
        }

        public float GetGreen()
        {
            return G * TO_FLOAT_MULTIPLIER;
        }

        public float GetRed()
        {
            return R * TO_FLOAT_MULTIPLIER;
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