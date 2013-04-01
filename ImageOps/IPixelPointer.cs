namespace ImageOps
{
	internal interface IPixelPointer
	{
		PixelColor Get();
		void MoveBy(int i);
	}
}