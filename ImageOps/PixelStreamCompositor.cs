using System.Linq;
using ImageOps.Streaming.Blenders;

namespace ImageOps
{
	public static class PixelStreamCompositor
	{
		public static IPixelStream Multiply(this IPixelStream source, params IPixelStream[] layers)
		{
			return layers.Aggregate(source, (current, stream) => new MultiplyBlend(current, stream));
		}

		public static IPixelStream Add(this IPixelStream source, params IPixelStream[] layers)
		{
			return layers.Aggregate(source, (current, stream) => new AddBlend(current, stream));
		}

		public static IPixelStream Mix(this IPixelStream source, params IPixelStream[] layers)
		{
			return layers.Aggregate(source, (current, stream) => new NormalBlend(current, stream));
		}

		public static IPixelStream AddAlphaMask(this IPixelStream source, IPixelStream mask, ColorChannel maskChannel = ColorChannel.Alpha)
		{
			return new AlphaMaskBlend(source, mask, maskChannel);
		}
	}
}
