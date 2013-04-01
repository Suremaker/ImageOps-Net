
using System.Drawing;
using System.Globalization;

namespace ImageOps
{
	public struct PixelColor
	{
		private readonly uint _argb;

		public PixelColor(float alpha, float red, float green, float blue)
			: this((byte)(alpha * 255), (byte)(red * 255), (byte)(green * 255), (byte)(blue * 255))
		{ }

		public PixelColor(byte alpha, byte red, byte green, byte blue)
			: this(((uint)alpha << 24) | ((uint)red << 16) | ((uint)green << 8) | blue)
		{ }

		public PixelColor(Color color)
			: this((uint)color.ToArgb())
		{ }

		public PixelColor(uint argb)
		{
			_argb = argb;
		}

		public byte A { get { return (byte)(_argb >> 24); } }
		public byte R { get { return (byte)(_argb >> 16); } }
		public byte G { get { return (byte)(_argb >> 8); } }
		public byte B { get { return (byte)_argb; } }

		public float GetAlpha() { return A / 255.0f; }
		public float GetRed() { return R / 255.0f; }
		public float GetGreen() { return G / 255.0f; }
		public float GetBlue() { return B / 255.0f; }

		public Color Color { get { return Color.FromArgb((int)_argb); } }
		public uint Argb { get { return _argb; } }

		public override string ToString()
		{
			return _argb.ToString("X", CultureInfo.InvariantCulture);
		}
	}
}