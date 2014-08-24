namespace ImageOps.Streaming.Sources.BitmapProcessing
{
	internal interface IPixelPointer:IPixelStream2
	{
		PixelColor Get();
		void MoveBy(int i);
	}
}