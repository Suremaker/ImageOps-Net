using System;
using ImageOps.Blenders;
using ImageOps.Sources.Regions;

namespace ImageOps.Sources.Readers
{
    internal class RegionMaskedBlendingReader : IDisposable
    {
        public RegionMaskedBlendingReader(BlendedRegion blendedRegion)
        {
            _blendingMethod = blendedRegion.BlendingMethod;
            _reader = blendedRegion.Source.OpenReader().InVerifiedContext();
            _region = blendedRegion.Region;
            _leftMargin = blendedRegion.Region.BoundingBox.X;
            _topMargin = blendedRegion.Region.BoundingBox.Y;
        }

        private readonly int _topMargin;
        private readonly int _leftMargin;
        private readonly IRegion _region;
        private readonly IVerifiedPixelReader _reader;
        private readonly IBlendingMethod _blendingMethod;

        public PixelColor BlendWith(PixelColor current, int x, int y)
        {
            return _blendingMethod.Blend(current, _reader.VerifiedGet(x - _leftMargin, y - _topMargin));
        }
        public bool IsInside(int x, int y)
        {
            return _region.IsInside(x, y);
        }

        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}