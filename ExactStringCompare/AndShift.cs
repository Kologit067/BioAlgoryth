using BaseContract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExactStringCompare
{
    //--------------------------------------------------------------------------------------
    // class AndShift 
    //--------------------------------------------------------------------------------------
    public class AndShift
    {
        public static readonly string AlgorythmName = "ASH";
        protected Dictionary<char, long> letterVectors = new Dictionary<char, long>();
        public IStringCompareAccumulator StatisticAccumulator { get; set; }

        protected string _outputPresentation;
        public string OutputPresentation
        {
            get
            {
                return _outputPresentation;
            }
        }

        protected Stopwatch stopwatch;

        public List<int> FindSubstring(string text, string pattern, bool isSaveStatisticsForEmpty = true)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            StatisticAccumulator.CreateStatistics(text, pattern);

            List<int> result = new List<int>();
            PrePreprocessString(pattern);
            long textAsNumber = 0;
            long lastBit = 1 << (pattern.Length - 1);
            for(int i = 0; i < text.Length; i++)
            {
                textAsNumber <<= 1;
                textAsNumber |= 1;
                long letterVector = 0;
                letterVectors.TryGetValue(text[i], out letterVector);
                textAsNumber &= letterVector;
                if ((textAsNumber & lastBit) != 0)
                    result.Add(i - pattern.Length + 1);
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

        //--------------------------------------------------------------------------------------
        public void PrePreprocessString(string line)
        {
            StatisticAccumulator.IterationCountInc();

            letterVectors.Clear();
            long k = 1;
            for (int i = 0; i < line.Length; i++)
            {
                StatisticAccumulator.IterationCountInc(2);
                if (!letterVectors.ContainsKey(line[i]))
                {
                    letterVectors.Add(line[i], 0);
                    StatisticAccumulator.IterationCountInc();
                }
                letterVectors[line[i]] |= k;
                k <<= 1;
            }

        }
    }
}
