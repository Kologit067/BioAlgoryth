using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExactStringCompare
{
    public class SimpletStringCompareByPreprocessing : StringPreprocessing
    {
        public static readonly string AlgorythmName = "SSCP";
        public List<int> FindSubstring(string text, string pattern)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            StatisticAccumulator.CreateStatistics(text, pattern);

            List<int> result = new List<int>();
            string totalString = pattern + text;
            zValue = PreprocessString(totalString);
            int lenPattern = pattern.Length;
            for(int i = lenPattern; i < totalString.Length; i++)
            {
                StatisticAccumulator.IterationCountInc();
                if (zValue[i] == lenPattern)
                    result.Add(i);
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
