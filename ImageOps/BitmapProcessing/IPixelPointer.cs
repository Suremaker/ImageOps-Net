namespace ImageOps.BitmapProcessing
{
	internal interface IPixelPointer
	{
		PixelColor Get();
		void MoveBy(int i);
	}
}