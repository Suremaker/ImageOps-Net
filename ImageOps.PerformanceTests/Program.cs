using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ImageOps.PerformanceTests.Helpers;

namespace ImageOps.PerformanceTests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var testCases = typeof(Program)
                .Assembly.GetTypes()
                .Where(t => typeof(TestCase).IsAssignableFrom(t))
                .Where(t => !t.IsAbstract)
                .OrderBy(t => t.Name)
                .Select(Activator.CreateInstance)
                .Cast<TestCase>()
                .ToArray();

            Console.WriteLine("Executing {0} cases...", testCases.Length);
            var results = testCases.Select((t, index) => RunCase(index, testCases)).ToList();
            WriteResults(results);
            Console.WriteLine("Done.");
            Console.ReadLine();
        }

        private static void WriteResults(IEnumerable<Result> results)
        {
            var date = DateTime.Now;
            var file = string.Format("report_{0}.xml", date.ToString("yyyy-MM-dd HH-mm"));
            using (var fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write))
            using (var sw = new StreamWriter(fs, Encoding.UTF8))
            {
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                sw.Write("<results>");
                foreach (var result in results)
                    sw.Write("<result name=\"{0}\" Repeats=\"{1}\" Total=\"{2}\" Min=\"{3}\" Max=\"{4}\" Avg=\"{5}\"/>",
                             result.Name,
                             result.Repeats,
                             (int)result.Total.TotalMilliseconds,
                             (int)result.Min.TotalMilliseconds,
                             (int)result.Max.TotalMilliseconds,
                             (int)result.Avg.TotalMilliseconds);
                sw.Write("</results>");
            }
        }

        private static Result RunCase(int index, TestCase[] testCases)
        {
            Console.Write("{0}/{1} - {2}: ", index + 1, testCases.Length, testCases[index].GetType().Name);
            var result = testCases[index].Test();
            Console.WriteLine(result);
            return result;
        }
    }
}