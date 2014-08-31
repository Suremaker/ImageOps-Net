using System;
using ImageOps.Blenders;
using ImageOps.Sources.Regions;

namespace ImageOps.Sources
{
    public class BlendedRegion
    {
        public BlendedRegion(IRegion region, IPixelSource source, IBlendingMethod blendingMethod)
        {
            if (source.ImageWidth != region.BoundingBox.Width || source.ImageHeight != region.BoundingBox.Height)
                throw new ArgumentException(string.Format("Source size have to match region size: Size={0}, Source={1}", region.BoundingBox, source));
            Source = source;
            BlendingMethod = blendingMethod;
            Region = region;
        }

        public IRegion Region { get; private set; }
        public IPixelSource Source { get; private set; }
        public IBlendingMethod BlendingMethod { get; private set; }
    }
}