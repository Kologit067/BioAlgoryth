using BaseContract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExactStringCompare
{
    public class BruteForceStringCompare
    {
        public static readonly string AlgorythmName = "BFSC";
        public IStringCompareAccumulator StatisticAccumulator { get; set; }

        public List<int> FindSubstring(string text, string pattern)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            StatisticAccumulator.CreateStatistics(text, pattern);

            List<int> result = new List<int>();
            int max = text.Length - pattern.Length;
            for ( int i = 0; i <= max; i++)
            {
                int j = 0;
                int k = i;
                StatisticAccumulator.IterationCountInc();
                for (; j < pattern.Length; j++)
                {
                    StatisticAccumulator.NumberOfComparisonInc();
                    StatisticAccumulator.IterationCountInc();
                    if (text[k++] != pattern[j])
                        break;
                }
                StatisticAccumulator.IterationCountInc();
                if (j == pattern.Length)
                {
                    StatisticAccumulator.IterationCountInc();
                    result.Add(i);
                }
            }

            stopwatch.Stop();
            long elapsedTicks = stopwatch.ElapsedTicks;
            long durationMilliSeconds = stopwatch.ElapsedMilliseconds;
            string outputPresentation = string.Join(",", result.Select(p => p.ToString()));

            StatisticAccumulator.SaveStatisticData(outputPresentation, elapsedTicks, durationMilliSeconds, DateTime.Now);
            return result;
        }
    }
}
