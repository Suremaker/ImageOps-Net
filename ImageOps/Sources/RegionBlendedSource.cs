using System.Collections.Generic;
using ImageOps.Blenders;
using ImageOps.Sources.Readers;
using ImageOps.Sources.Regions;

namespace ImageOps.Sources
{
    public class RegionBlendedSource : IPixelSource
    {
        private readonly List<BlendedRegion> _regions = new List<BlendedRegion>();
        public IPixelSource OriginalSource { get; private set; }
        public IEnumerable<BlendedRegion> Regions { get { return _regions; } }

        public RegionBlendedSource(IPixelSource source)
        {
            OriginalSource = source;
            ImageWidth = source.ImageWidth;
            ImageHeight = source.ImageHeight;
        }

        public RegionBlendedSource AddRegion(IRegion region, IPixelSource regionSource, IBlendingMethod blendingMethod)
        {
            _regions.Add(new BlendedRegion(region, regionSource, blendingMethod));
            return this;
        }

        public void Dispose()
        {
            OriginalSource.Dispose();
            foreach (var region in _regions)
                region.Source.Dispose();
        }

        public int ImageWidth { get; private set; }
        public int ImageHeight { get; private set; }
        public IPixelReader OpenReader()
        {
            return new RegionBlendingReader(this);
        }
    }
}