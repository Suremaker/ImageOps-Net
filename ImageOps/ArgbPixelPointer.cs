using System;

namespace ImageOps
{
	public class ArgbPixelPointer : IPixelPointer
	{
		private unsafe uint* _pointer;

		public unsafe ArgbPixelPointer(IntPtr scan0)
		{
			_pointer = (uint*)scan0.ToPointer();
		}

		public unsafe PixelColor Get()
		{
			return new PixelColor(*_pointer);
		}

		public unsafe void MoveBy(int i)
		{
			_pointer += i;
		}
	}
}