using System;

namespace ImageOps.Blenders
{
    public static class Discrete
    {
        public const int MaxColor = 255;

        /// <summary>
        /// Returns color value or 255 is it is higher than 255.
        /// Return value range: 0-255
        /// </summary>
        /// <param name="color">Valid range: 0-max</param>
        public static int Clamp(int color)
        {
            if (color <= MaxColor)
                return color;
            return MaxColor;
        }

        /// <summary>
        /// Returns color multiplied in ratio to max color (color*ratio/255)
        /// Return value range: 0-255
        /// </summary>
        /// <param name="color">Valid range: 0-255</param>
        /// <param name="ratio">Valid range: 0-255</param>
        public static int MulRatio(int color, int ratio)
        {
            return DivBy255(color * ratio);
        }

        /// <summary>
        /// Calculates alpha ratio that could be used later to apply blended front-back color over back color in proportion reflecting front and black alpha.
        /// Return value range: 0-255
        /// </summary>
        /// <param name="backAlpha">Valid range: 0-255</param>
        /// <param name="frontAlpha">Valid range: 0-255</param>
        public static int CalcAlphaRatio(int backAlpha, int frontAlpha)
        {
            var minAlpha = Math.Min(backAlpha, frontAlpha);
            var newAlpha = backAlpha + CompRatio(minAlpha, backAlpha);
            return (minAlpha * MaxColor / newAlpha);
        }

        /// <summary>
        /// Calculates color component that would complete second component calculated with MulRatio (color*(255-ratio)/255)
        /// Return value range: 0-255
        /// </summary>
        /// <param name="color">Valid range: 0-255</param>
        /// <param name="ratio">Valid range: 0-255</param>
        public static int CompRatio(int color, int ratio)
        {
            return DivBy255(color * (MaxColor - ratio));
        }

        /// <summary>
        /// Fast int division by 255
        /// Valid range: 0-65025 (255*255)
        /// </summary>
        public static int DivBy255(int value)
        {
            return (value + 1 + (value >> 8)) >> 8;
        }

        /// <summary>
        /// Blends back and front color in specified ratio
        /// Return value range: 0-255
        /// </summary>
        /// <param name="backColor">Valid range: 0-255</param>
        /// <param name="frontColor">Valid range: 0-255</param>
        /// <param name="ratio">Valid range: 0-255</param>
        public static byte BlendWithRatio(int backColor, int frontColor, int ratio)
        {
            return (byte)(MulRatio(frontColor, ratio) + CompRatio(backColor, ratio));
        }
    }
}