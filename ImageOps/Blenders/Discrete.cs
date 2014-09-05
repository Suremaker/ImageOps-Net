using System;

namespace ImageOps.Blenders
{
    public static class Discrete
    {
        private const int _maxColor = 255;

        public static int Clamp(int color)
        {
            if (color <= _maxColor)
                return color;
            return _maxColor;
        }

        public static int MulRatio(int color, int ratio)
        {
            return DivBy255(color * ratio);
        }

        public static int CalcAlphaRatio(int backAlpha, int frontAlpha)
        {
            var minAlpha = Math.Min(backAlpha, frontAlpha);
            var newAlpha = backAlpha + CompRatio(minAlpha, backAlpha);
            return (minAlpha * _maxColor / newAlpha);
        }

        public static int CompRatio(int color, int ratio)
        {
            return DivBy255(color * (_maxColor - ratio));
        }

        public static int DivBy255(int value)
        {
            return (value + 1 + (value >> 8)) >> 8;
        }

        public static byte BlendWithRatio(int backColor, int frontColor, int ratio)
        {
            return (byte)(MulRatio(frontColor, ratio) + CompRatio(backColor, ratio));
        }
    }
}