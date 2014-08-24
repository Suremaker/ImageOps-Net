namespace ImageOps.Blenders
{
    public static class BlendingMethods
    {
        public static readonly IBlendingMethod Add = new AddBlend();
        public static readonly IBlendingMethod Burn = new BurnBlend();
        public static readonly IBlendingMethod GrainMerge = new GrainMergeBlend();
        public static readonly IBlendingMethod Multiply = new MultiplyBlend();
        public static readonly IBlendingMethod Normal = new NormalBlend();
    }
}