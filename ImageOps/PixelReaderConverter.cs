using System.Collections.Generic;
using ImageOps.Sources.Readers;

namespace ImageOps
{
    public static class PixelReaderConverter
    {
        public static IVerifiedPixelReader InVerifiedContext(this IPixelReader reader)
        {
            return reader as IVerifiedPixelReader ?? new VerifiedPixelReaderProxy(reader);
        }

        public static IEnumerable<PixelColor> AsEnumerable(this IPixelReader reader)
        {
            for (int y = 0; y < reader.Height; y += 1)
                for (int x = 0; x < reader.Width; x += 1)
                    yield return reader.Get(x, y);
        }
    }
}
