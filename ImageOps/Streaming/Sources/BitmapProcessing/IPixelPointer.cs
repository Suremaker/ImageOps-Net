namespace ImageOps.Streaming.Sources.BitmapProcessing
{
	internal interface IPixelPointer
	{
		PixelColor Get();
		void MoveBy(int i);
	}
}