using System;
using System.Linq;
using ImageOps.Blenders;
using ImageOps.Sources.Regions;

namespace ImageOps.Sources.Readers
{
    internal class RegionMaskedBlendingReader : IDisposable
    {
        public RegionMaskedBlendingReader(BlendedRegion blendedRegion)
        {
            _blendingMethod = blendedRegion.BlendingMethod;
            _reader = blendedRegion.Source.OpenReader();
            _region = blendedRegion.Region;
            _leftMargin = blendedRegion.Region.BoundingBox.X;
            _topMargin = blendedRegion.Region.BoundingBox.Y;
        }

        private readonly int _topMargin;
        private readonly int _leftMargin;
        private readonly IRegion _region;
        private readonly IPixelReader _reader;
        private readonly IBlendingMethod _blendingMethod;

        public PixelColor BlendWith(PixelColor current, int x, int y)
        {
            return _blendingMethod.Blend(current, _reader.Get(x - _leftMargin, y - _topMargin));
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

    internal class RegionBlendingReader : SourceReader<RegionBlendedSource>
    {
        private readonly IPixelReader _reader;
        private readonly RegionMaskedBlendingReader[] _regionReaders;

        public RegionBlendingReader(RegionBlendedSource source)
            : base(source)
        {
            _regionReaders = source.Regions.Select(r => new RegionMaskedBlendingReader(r)).ToArray();
            _reader = source.OriginalSource.OpenReader();
        }

        public override void Dispose()
        {
            foreach (var reader in _regionReaders)
                reader.Dispose();
        }

        protected override PixelColor FastGet(int x, int y)
        {
            PixelColor current = _reader.Get(x, y);
            foreach (var reader in _regionReaders)
            {
                if (reader.IsInside(x, y))
                    current = reader.BlendWith(current, x, y);
            }
            return current;
        }
    }
}