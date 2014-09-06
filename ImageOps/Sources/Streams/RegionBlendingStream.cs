using System.Linq;

namespace ImageOps.Sources.Streams
{
    internal class RegionBlendingStream : SourceStream<RegionBlendedSource>
    {
        class RegionStream
        {
            public RegionStream(BlendedRegion blendedRegion, IPixelStream stream, bool isActive)
            {
                BlendedRegion = blendedRegion;
                Stream = stream;
                IsActive = isActive;
            }

            public BlendedRegion BlendedRegion { get; private set; }
            public IPixelStream Stream { get; private set; }
            public bool IsActive { get; set; }
        }
        private readonly IPixelStream _stream;
        private readonly RegionStream[] _regionStreams;

        public RegionBlendingStream(RegionBlendedSource source)
            : base(source)
        {
            _regionStreams = source.Regions.Select(r => new RegionStream(r, r.Source.OpenStream(), false)).ToArray();
            _stream = source.OriginalSource.OpenStream();
        }

        public override void Dispose()
        {
            foreach (var region in _regionStreams)
                region.Stream.Dispose();
        }

        public override PixelColor Get(int x, int y)
        {
            PixelColor current = _stream.Get(x, y);
            foreach (var region in _regionStreams)
            {
                if (region.BlendedRegion.Region.IsInside(x, y))
                {
                    current = region.BlendedRegion.BlendingMethod.Blend(current, region.Stream.Get(x - region.BlendedRegion.Region.BoundingBox.X, y - region.BlendedRegion.Region.BoundingBox.Y));
                }
            }
            return current;
        }
    }
}