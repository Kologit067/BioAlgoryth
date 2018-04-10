using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExactStringCompare
{
    //--------------------------------------------------------------------------------------
    // class SimpletStringCompareByPreprocessing 
    //--------------------------------------------------------------------------------------
    public class SimpletStringCompareByPreprocessing : StringPreprocessing
    {
        public static readonly string AlgorythmName = "SSCP";
        //private string _outputPresentation;
        //public string OutputPresentation
        //{
        //    get
        //    {
        //        return _outputPresentation;
        //    }
        //}

        public List<int> FindSubstring(string text, string pattern, bool isSaveStatisticsForEmpty = true)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            StatisticAccumulator.CreateStatistics(text, pattern);

            List<int> result = new List<int>();
            string totalString = pattern + "#" + text;
            zValue = PreprocessString(totalString);
            int lenPattern = pattern.Length;
            int textShift = lenPattern + 1;
            for (int i = lenPattern; i < totalString.Length; i++)
            {
                StatisticAccumulator.IterationCountInc();
                if (zValue[i] == lenPattern)
                    result.Add(i - textShift);
            }

            stopwatch.Stop();
            if (result.Count > 0 || isSaveStatisticsForEmpty)
            {
                long elapsedTicks = stopwatch.ElapsedTicks;
                long durationMilliSeconds = stopwatch.ElapsedMilliseconds;
                _outputPresentation = string.Join(",", result.Select(p => p.ToString()));

                StatisticAccumulator.SaveStatisticData(_outputPresentation, elapsedTicks, durationMilliSeconds, DateTime.Now, null);
            }
            else
            {
                StatisticAccumulator.RemoveStatisticData();
            }

            return result;
        }
    }
}
