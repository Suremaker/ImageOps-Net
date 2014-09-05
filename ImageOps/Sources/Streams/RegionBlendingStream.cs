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
            MoveBy(0);
        }

        public override void Dispose()
        {
            foreach (var region in _regionStreams)
                region.Stream.Dispose();
        }

        public override void MoveBy(int delta)
        {
            _stream.Move(delta);
            var newPos = Position + delta;
            var y = newPos / Source.ImageHeight;
            var x = newPos % Source.ImageWidth;
            foreach (var region in _regionStreams)
            {
                if ((region.IsActive = region.BlendedRegion.Region.IsInside(x, y)))
                    MoveInnerStream(region, x, y);
            }
        }

        private static void MoveInnerStream(RegionStream region, int x, int y)
        {
            var boundingBox = region.BlendedRegion.Region.BoundingBox;
            var newPosition = (y - boundingBox.Y) * boundingBox.Width + (x - boundingBox.X);
            region.Stream.Move(newPosition - region.Stream.Position);
        }

        public override PixelColor GetCurrent()
        {
            PixelColor current = _stream.GetCurrent();
            foreach (var region in _regionStreams)
            {
                if (region.IsActive)
                    current = region.BlendedRegion.BlendingMethod.Blend(current, region.Stream.GetCurrent());
            }
            return current;
        }
    }
}