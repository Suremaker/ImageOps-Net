using System;
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
            SectorSize = Math.Max(16, Math.Max(ImageWidth, ImageHeight) / 8);
        }

        public RegionBlendedSource AddRegion(IRegion region, IPixelSource regionSource, IBlendingMethod blendingMethod)
        {
            var blendedRegion = new BlendedRegion(region, regionSource, blendingMethod);
            _regions.Add(blendedRegion);
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
        public int SectorSize { get; private set; }

        public IPixelReader OpenReader()
        {
            return new RegionBlendingReader(this);
        }
    }
}