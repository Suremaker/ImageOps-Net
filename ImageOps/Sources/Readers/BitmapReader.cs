using System.Drawing.Imaging;

namespace ImageOps.Sources.Readers
{
    internal abstract class BitmapReader : SourceReader<IPixelSource>
    {
        private readonly BitmapLocker _locker;
        protected BitmapData Data { get; private set; }

        protected BitmapReader(IPixelSource source, BitmapLocker locker)
            : base(source)
        {
            _locker = locker;
            Data = locker.Lock();
        }

        public override void Dispose()
        {
            _locker.Unlock();
        }
    }
}