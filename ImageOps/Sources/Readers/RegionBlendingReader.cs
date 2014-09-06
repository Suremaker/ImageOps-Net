using System.Collections.Generic;
using System.Drawing;

namespace ImageOps.Sources.Readers
{
    internal class RegionBlendingReader : SourceReader<RegionBlendedSource>
    {
        private readonly IVerifiedPixelReader _reader;
        private readonly List<RegionMaskedBlendingReader> _regionReaders;
        private readonly IDictionary<Point, List<RegionMaskedBlendingReader>> _sectorReaders = new Dictionary<Point, List<RegionMaskedBlendingReader>>();
        private readonly int _sectorSize;

        public RegionBlendingReader(RegionBlendedSource source)
            : base(source)
        {
            _reader = source.OriginalSource.OpenReader().InVerifiedContext();
            _regionReaders = new List<RegionMaskedBlendingReader>();
            _sectorSize = Source.SectorSize;
            foreach (var blendedRegion in source.Regions)
                AddReader(blendedRegion);
        }

        private void AddReader(BlendedRegion blendedRegion)
        {
            var reader = new RegionMaskedBlendingReader(blendedRegion);
            _regionReaders.Add(reader);
            var box = blendedRegion.Region.BoundingBox;
            for (int sx = box.X / _sectorSize; sx * _sectorSize < box.LastX; sx += 1)
                for (int sy = box.Y / _sectorSize; sy * _sectorSize < box.LastY; sy += 1)
                    AddToGroup(sx, sy, reader);
        }

        private void AddToGroup(int sectionX, int sectionY, RegionMaskedBlendingReader reader)
        {
            var point = new Point(sectionX, sectionY);
            List<RegionMaskedBlendingReader> regions;
            if (!_sectorReaders.TryGetValue(point, out regions))
                _sectorReaders.Add(point, regions = new List<RegionMaskedBlendingReader>());
            regions.Add(reader);
        }

        public override void Dispose()
        {
            foreach (var reader in _regionReaders)
                reader.Dispose();
        }

        public override PixelColor VerifiedGet(int x, int y)
        {
            PixelColor current = _reader.VerifiedGet(x, y);
            List<RegionMaskedBlendingReader> readers;
            if (!_sectorReaders.TryGetValue(new Point(x / _sectorSize, y / _sectorSize), out readers))
                return current;

            for (int i = 0; i < readers.Count; ++i)
            {
                if (readers[i].IsInside(x, y))
                    current = readers[i].BlendWith(current, x, y);
            }
            return current;
        }
    }
}