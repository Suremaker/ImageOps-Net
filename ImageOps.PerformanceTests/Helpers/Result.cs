using System;

namespace ImageOps.PerformanceTests.Helpers
{
    public class Result
    {
        public Result(string name,TimeSpan total, int repeats, TimeSpan min, TimeSpan max)
        {
            Name = name;
            Max = max;
            Min = min;
            Repeats = repeats;
            Total = total;
        }

        public TimeSpan Min { get; private set; }
        public string Name { get; private set; }
        public TimeSpan Max { get; private set; }
        public TimeSpan Avg { get { return TimeSpan.FromTicks(Total.Ticks / Repeats); } }
        public int Repeats { get; private set; }
        public TimeSpan Total { get; private set; }
        public override string ToString()
        {
            return string.Format("R: {0}, T: {1}, Mi: {2}, Ma: {3}, Av: {4}", Repeats,
                                 (int)Total.TotalMilliseconds,
                                 (int)Min.TotalMilliseconds,
                                 (int)Max.TotalMilliseconds,
                                 (int)Avg.TotalMilliseconds);
        }
    }
}