using System;
using System.Diagnostics;
using System.Drawing;

namespace ImageOps.PerformanceTests.Helpers
{
    public abstract class TestCase
    {
        public readonly int Repeats;

        protected TestCase(int repeats)
        {
            Repeats = repeats;
        }

        protected abstract Bitmap Run();

        public Result Test()
        {
            Run().Save(GetType().Name + ".png"); //cold run
            var watch = new Stopwatch();
            var total = new TimeSpan();
            var min = TimeSpan.MaxValue;
            var max = TimeSpan.MinValue;
            for (int i = 0; i < Repeats; ++i)
            {
                GC.Collect();
                watch.Restart();
                Run();
                watch.Stop();
                total += watch.Elapsed;
                if (watch.Elapsed < min) min = watch.Elapsed;
                if (watch.Elapsed > max) max = watch.Elapsed;
            }
            return new Result(GetType().Name, total, Repeats, min, max);
        }
    }
}