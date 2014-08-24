using System.Drawing.Imaging;

namespace ImageOps.Sources.Streams
{
    internal abstract class BitmapStream : SourceStream<IPixelSource>
    {
        private readonly BitmapLocker _locker;
        protected BitmapData Data { get; private set; }

        protected BitmapStream(IPixelSource source, BitmapLocker locker)
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