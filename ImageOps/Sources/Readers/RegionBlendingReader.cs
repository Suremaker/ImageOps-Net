using System.Linq;

namespace ImageOps.Sources.Readers
{
    internal class RegionBlendingReader : SourceReader<RegionBlendedSource>
    {
        class RegionStream
        {
            public RegionStream(BlendedRegion blendedRegion, IPixelReader reader, bool isActive)
            {
                BlendedRegion = blendedRegion;
                Reader = reader;
                IsActive = isActive;
            }

            public BlendedRegion BlendedRegion { get; private set; }
            public IPixelReader Reader { get; private set; }
            public bool IsActive { get; set; }
        }
        private readonly IPixelReader _reader;
        private readonly RegionStream[] _regionStreams;

        public RegionBlendingReader(RegionBlendedSource source)
            : base(source)
        {
            _regionStreams = source.Regions.Select(r => new RegionStream(r, r.Source.OpenReader(), false)).ToArray();
            _reader = source.OriginalSource.OpenReader();
        }

        public override void Dispose()
        {
            foreach (var region in _regionStreams)
                region.Reader.Dispose();
        }

        protected override PixelColor FastGet(int x, int y)
        {
            PixelColor current = _reader.Get(x, y);
            foreach (var region in _regionStreams)
            {
                if (region.BlendedRegion.Region.IsInside(x, y))
                {
                    current = region.BlendedRegion.BlendingMethod.Blend(current, region.Reader.Get(x - region.BlendedRegion.Region.BoundingBox.X, y - region.BlendedRegion.Region.BoundingBox.Y));
                }
            }
            return current;
        }
    }
}