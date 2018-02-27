using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExactStringCompare
{
    //--------------------------------------------------------------------------------------
    // class BoyerMooreCompare 
    //--------------------------------------------------------------------------------------
    public class BoyerMooreCompare : StringPreprocessing
    {
        public static readonly string AlgorythmName = "BMC";
        public static readonly string AlgorythmNameBadSymbolAdv = "BMCBSA";
        public static readonly string AlgorythmNameBadSymbol = "BMCBS";
        public static readonly string AlgorythmNameGoodSuffix = "BMCGS";
        public static readonly string AlgorythmNameGoodSuffixBadSymbol = "BMCGSBS";
        //--------------------------------------------------------------------------------------
        private string _outputPresentation;
        public string OutputPresentation
        {
            get
            {
                return _outputPresentation;
            }
        }
        //--------------------------------------------------------------------------------------
        public List<int> FindSubstringBadSymbolAdv(string text, string pattern)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            StatisticAccumulator.CreateStatistics(text, pattern);

            List<int> result = new List<int>();
            BadSymbolAdvPreprocessString(pattern);
            int lenPattern = pattern.Length;
            int i = 0;
            while (i <= text.Length - pattern.Length)
            {
                int j = pattern.Length - 1;
                int j0 = j + i;
                StatisticAccumulator.IterationCountInc(2);
                while (j >= 0)
                {
                    StatisticAccumulator.NumberOfComparisonInc();
                    StatisticAccumulator.IterationCountInc();
                    if (pattern[j] != text[j0])
                        break;
                    j--;
                    j0--;
                    StatisticAccumulator.IterationCountInc(2);
                }
                if (j < 0)
                {
                    result.Add(i);
                    i++;
                    StatisticAccumulator.IterationCountInc(2);
                }
                else
                {
                    StatisticAccumulator.IterationCountInc();
                    if (rAdvValue.ContainsKey(text[j0]))
                    {
                        int l = 0;
                        StatisticAccumulator.IterationCountInc(3);
                        while (l < rAdvValue[text[j0]].Count && rAdvValue[text[j0]][l] < j)
                        {
                            l++;
                            StatisticAccumulator.IterationCountInc(3);
                        }
                        if (l > 0)
                        {
                            l--;
                            StatisticAccumulator.IterationCountInc();
                        }
                        StatisticAccumulator.IterationCountInc(2);
                        int maxPos = rAdvValue[text[j0]][l];
                        if (rAdvValue[text[j0]][l] >= j)
                        {
                            i += j + 1;
                        }
                        else
                        {
                            i += j - maxPos;
                        }
                    }
                    else
                    {
                        StatisticAccumulator.IterationCountInc();
                        i = j0 + 1;
                    }
                }
            }

            stopwatch.Stop();
            long elapsedTicks = stopwatch.ElapsedTicks;
            long durationMilliSeconds = stopwatch.ElapsedMilliseconds;
            _outputPresentation = string.Join(",", result.Select(p => p.ToString()));

            StatisticAccumulator.SaveStatisticData(_outputPresentation, elapsedTicks, durationMilliSeconds, DateTime.Now);

            return result;
        }
        //--------------------------------------------------------------------------------------
        public List<int> FindSubstringBadSymbol(string text, string pattern)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            StatisticAccumulator.CreateStatistics(text, pattern);

            List<int> result = new List<int>();
            BadSymbolPreprocessString(pattern);
            int lenPattern = pattern.Length;
            int i = 0;
            while (i <= text.Length - pattern.Length)
            {
                int j = pattern.Length - 1;
                int j0 = j + i;
                StatisticAccumulator.IterationCountInc(2);
                while (j >= 0)
                {
                    StatisticAccumulator.IterationCountInc();
                    StatisticAccumulator.NumberOfComparisonInc();
                    if (pattern[j] != text[j0])
                        break;
                    StatisticAccumulator.IterationCountInc(2);
                    j--;
                    j0--;
                }
                if (j < 0)
                {
                    StatisticAccumulator.IterationCountInc(2);
                    result.Add(i);
                    i++;
                }
                else
                {
                    StatisticAccumulator.IterationCountInc(2);
                    if (rValue.ContainsKey(text[j0]))
                    {
                        StatisticAccumulator.IterationCountInc();
                        if (rValue[text[j0]] >= j)
                            i++;
                        else
                        {
                            i += j - rValue[text[j0]];
                        }
                    }
                    else
                        i = j0 + 1;
                }
            }

            stopwatch.Stop();
            long elapsedTicks = stopwatch.ElapsedTicks;
            long durationMilliSeconds = stopwatch.ElapsedMilliseconds;
            _outputPresentation = string.Join(",", result.Select(p => p.ToString()));

            StatisticAccumulator.SaveStatisticData(_outputPresentation, elapsedTicks, durationMilliSeconds, DateTime.Now);

            return result;
        }
        //--------------------------------------------------------------------------------------
        public List<int> FindSubstringGoodSuffix(string text, string pattern)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            StatisticAccumulator.CreateStatistics(text, pattern);

            List<int> result = new List<int>();
            LliPreprocessString(pattern);
            LiByNPreprocessString(pattern);
            int lenPattern = pattern.Length;
            int i = 0;
            while (i < text.Length - pattern.Length)
            {
                int j = pattern.Length - 1;
                int j0 = j + i;
                StatisticAccumulator.IterationCountInc(2);
                while (j >= 0)
                {
                    StatisticAccumulator.IterationCountInc();
                    StatisticAccumulator.NumberOfComparisonInc();
                    if (pattern[j] != text[j0])
                        break;
                    StatisticAccumulator.IterationCountInc(2);
                    j--;
                    j0--;
                }
                if (j < 0)
                {
                    result.Add(i);
                    i += lenPattern - llisValue[1];
                    StatisticAccumulator.IterationCountInc(2);
                }
                else
                {
                    StatisticAccumulator.IterationCountInc();
                    if (j < pattern.Length - 1)
                    {
                        j++;
                        StatisticAccumulator.IterationCountInc();
                        if (lisValue[j] > 0)
                        {
                            i += lenPattern - lisValue[j];
                        }
                        else
                        {
                            i += lenPattern - llisValue[j];
                        }
                    }
                    else
                        i++;
                }
            }

            stopwatch.Stop();
            long elapsedTicks = stopwatch.ElapsedTicks;
            long durationMilliSeconds = stopwatch.ElapsedMilliseconds;
            _outputPresentation = string.Join(",", result.Select(p => p.ToString()));

            StatisticAccumulator.SaveStatisticData(_outputPresentation, elapsedTicks, durationMilliSeconds, DateTime.Now);

            return result;
        }
        //--------------------------------------------------------------------------------------
        public List<int> FindSubstring(string text, string pattern)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            StatisticAccumulator.CreateStatistics(text, pattern);

            List<int> result = new List<int>();
            LliPreprocessString(pattern);
            LiByNPreprocessString(pattern);
            BadSymbolPreprocessString(pattern);
            int lenPattern = pattern.Length;
            int i = 0;
            while (i < text.Length - pattern.Length)
            {
                int j = pattern.Length - 1;
                int j0 = j + i;
                StatisticAccumulator.IterationCountInc(2);
                while (j >= 0)
                {
                    StatisticAccumulator.IterationCountInc();
                    StatisticAccumulator.NumberOfComparisonInc();
                    if (pattern[j] != text[j0])
                        break;
                    j--;
                    j0--;
                    StatisticAccumulator.IterationCountInc(2);
                }
                if (j < 0)
                {
                    result.Add(i);
                    i += lenPattern - llisValue[1];
                    StatisticAccumulator.IterationCountInc(2);
                }
                else
                {
                    int suffixStiff = 1;
                    int symbolStiff = 1;
                    StatisticAccumulator.IterationCountInc(3);
                    if (rValue.ContainsKey(text[j0]))
                    {
                        if (rValue[text[j0]] < j)
                        {
                            symbolStiff  = rValue[text[j0]] - j;
                            StatisticAccumulator.IterationCountInc();
                        }
                    }
                    else
                        symbolStiff = j0 + 1 - i;

                    if (j < pattern.Length - 1)
                    {
                        StatisticAccumulator.IterationCountInc(2);
                        j++;
                        if (lisValue[j] > 0)
                        {
                            suffixStiff = lenPattern - lisValue[j];
                        }
                        else
                        {
                            suffixStiff = lenPattern - llisValue[j];
                        }

                    }

                    StatisticAccumulator.IterationCountInc(2);
                    int stiff = Math.Max(symbolStiff, suffixStiff);
                    i += stiff;
                }
            }

            stopwatch.Stop();
            long elapsedTicks = stopwatch.ElapsedTicks;
            long durationMilliSeconds = stopwatch.ElapsedMilliseconds;
            _outputPresentation = string.Join(",", result.Select(p => p.ToString()));

            StatisticAccumulator.SaveStatisticData(_outputPresentation, elapsedTicks, durationMilliSeconds, DateTime.Now);

            return result;
        }
        //--------------------------------------------------------------------------------------
        public List<int> FindSubstringByGoodSuffixBadSymbolAdv(string text, string pattern)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            StatisticAccumulator.CreateStatistics(text, pattern);

            List<int> result = new List<int>();
            LliPreprocessString(pattern);
            LiByNPreprocessString(pattern);
            BadSymbolAdvPreprocessString(pattern);
            int lenPattern = pattern.Length;
            int i = 0;
            while (i < text.Length - pattern.Length)
            {
                int j = pattern.Length - 1;
                int j0 = j + i;
                StatisticAccumulator.IterationCountInc(2);
                while (j >= 0)
                {
                    StatisticAccumulator.IterationCountInc();
                    StatisticAccumulator.NumberOfComparisonInc();
                    if (pattern[j] != text[j0])
                        break;
                    j--;
                    j0--;
                    StatisticAccumulator.IterationCountInc(2);
                }
                if (j < 0)
                {
                    result.Add(i);
                    i += lenPattern - llisValue[1];
                    StatisticAccumulator.IterationCountInc(2);
                }
                else
                {
                    int suffixStiff = 1;
                    int symbolStiff = 1;

                    StatisticAccumulator.IterationCountInc(4);
                    if (rAdvValue.ContainsKey(text[j0]))
                    {
                        StatisticAccumulator.IterationCountInc(3);
                        int l = 0;
                        while (l < rAdvValue[text[j0]].Count && rAdvValue[text[j0]][l] < j)
                        {
                            l++;
                            StatisticAccumulator.IterationCountInc(3);
                        }
                        if (l > 0)
                        {
                            l--;
                            StatisticAccumulator.IterationCountInc();
                        }
                        StatisticAccumulator.IterationCountInc(3);
                        int maxPos = rAdvValue[text[j0]][l];
                        if (l >= j)
                            symbolStiff = j0 + 1 -i;
                        else
                        {
                            symbolStiff  = maxPos - j;
                        }
                    }
                    else
                        symbolStiff = j0 + 1 -i;

                    if (j < pattern.Length - 1)
                    {
                        StatisticAccumulator.IterationCountInc(3);
                        j++;
                        if (lisValue[j] > 0)
                        {
                            suffixStiff = lenPattern - lisValue[j];
                        }
                        else
                        {
                            suffixStiff = lenPattern - llisValue[j];
                        }

                    }

                    StatisticAccumulator.IterationCountInc(2);
                    int stiff = Math.Max(symbolStiff, suffixStiff);
                    i += stiff;
                }
            }

            stopwatch.Stop();
            long elapsedTicks = stopwatch.ElapsedTicks;
            long durationMilliSeconds = stopwatch.ElapsedMilliseconds;
            _outputPresentation = string.Join(",", result.Select(p => p.ToString()));

            StatisticAccumulator.SaveStatisticData(_outputPresentation, elapsedTicks, durationMilliSeconds, DateTime.Now);

            return result;
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
