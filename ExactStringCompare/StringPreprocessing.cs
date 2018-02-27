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
    // class StringPreprocessing 
    //--------------------------------------------------------------------------------------
    public class StringPreprocessing
    {
        //        protected char[] alphabet;
        protected int[] zValue;
        protected int[] zReverseValue;
        protected Dictionary<char, int> rValue;
        protected Dictionary<char, List<int>> rAdvValue;
        protected int[] liValue;
        protected int[] lisValue;
        protected int[] llisValue;
        protected int[] spsValue;

        protected Stopwatch stopwatch;

        public IStringCompareAccumulator StatisticAccumulator { get; set; }

        public int[] PreprocessString(string line)
        {
            int li = 0;
            int ri = 0;
            int[] zvalue = new int[line.Length];
            for (int i = 1; i < line.Length; i++)
            {
                StatisticAccumulator.IterationCountInc();
                if (ri < i)
                {
                    StatisticAccumulator.IterationCountInc();
                    StatisticAccumulator.NumberOfComparisonInc();
                    if (line[i] == line[0])
                    {
                        int j = 0;
                        StatisticAccumulator.IterationCountInc();
                        StatisticAccumulator.NumberOfComparisonInc();
                        while (i + j < line.Length && line[j] == line[i + j])
                        {
                            j++;
                            StatisticAccumulator.NumberOfComparisonInc();
                            StatisticAccumulator.IterationCountInc();
                        }
                        StatisticAccumulator.IterationCountInc(3);
                        ri = i + j - 1;
                        li = i;
                        zvalue[i] = j;
                    }
                    else
                    {
                        StatisticAccumulator.IterationCountInc();
                        zvalue[i] = 0;
                    }
                }
                else
                {
                    StatisticAccumulator.IterationCountInc();
                    int i0 = i - li;
                    if (zvalue[i0] + i0 < zvalue[li])
                    {
                        StatisticAccumulator.IterationCountInc();
                        zvalue[i] = zvalue[i0];
                    }
                    else
                    {

                        StatisticAccumulator.IterationCountInc(2);
                        int j = ri+1;
                        int j_ = j - i;
                        StatisticAccumulator.NumberOfComparisonInc();
                        while (j < line.Length && line[j_] == line[j])
                        {
                            j++;
                            j_++;
                            StatisticAccumulator.NumberOfComparisonInc();
                            StatisticAccumulator.IterationCountInc();
                        }
                        li = i;
                        ri = j - 1;
                        zvalue[i] = ri - li + 1;
                        StatisticAccumulator.IterationCountInc(3);
                    }
                }
            }
            return zvalue;
        }
        //--------------------------------------------------------------------------------------
        public int[] PreprocessNValueString(string line)
        {
            int len = line.Length;
            int last = len - 1;
            int li = last;
            int ri = last;
            int[] nvalue = new int[line.Length];
            StatisticAccumulator.IterationCountInc(5);
            for (int i = last - 1; i >= 0; i--)
            {
                StatisticAccumulator.IterationCountInc();
                if (i < ri)
                {
                    StatisticAccumulator.IterationCountInc(2);
                    if (line[i] == line[last])
                    {
                        int j = 0;
                        StatisticAccumulator.IterationCountInc();
                        StatisticAccumulator.NumberOfComparisonInc();
                        while (line[last - j] == line[i - j])
                        {
                            j--;
                            StatisticAccumulator.NumberOfComparisonInc();
                            StatisticAccumulator.IterationCountInc(2);
                        }
                        li = i;
                        ri = i - j + 1;
                        nvalue[i] = j;
                        StatisticAccumulator.IterationCountInc(3);
                    }
                    else
                        nvalue[i] = 0;
                }
                else
                {
                    StatisticAccumulator.IterationCountInc(2);
                    int i0 = len - li + i;
                    if (nvalue[i0] + li - i < nvalue[li])
                    {
                        StatisticAccumulator.IterationCountInc(2);
                        nvalue[i] = nvalue[i0];
                    }
                    else
                    {

                        int j = ri;
                        int j2 = last - (i - ri);
                        StatisticAccumulator.IterationCountInc(3);
                        StatisticAccumulator.NumberOfComparisonInc();
                        while (line[j] == line[j2--])
                        {
                            j--;
                            StatisticAccumulator.NumberOfComparisonInc();
                            StatisticAccumulator.IterationCountInc(2);
                        }
                        li = i;
                        ri = j + 1;
                        nvalue[i] = li - ri + 1;
                        StatisticAccumulator.IterationCountInc(3);
                    }
                }
            }
            return nvalue;
        }

        //--------------------------------------------------------------------------------------
        public void BadSymbolPreprocessString(string line)
        {
            StatisticAccumulator.IterationCountInc();
            rValue = new Dictionary<char, int>();
            for (int i = 0; i < line.Length; i++)
            {
                StatisticAccumulator.IterationCountInc(2);
                if (rValue.ContainsKey(line[i]))
                    rValue[line[i]] = i;
                else
                    rValue.Add(line[i], i);
            }
        }
        //--------------------------------------------------------------------------------------
        public void BadSymbolAdvPreprocessString(string line)
        {

            StatisticAccumulator.IterationCountInc();
            rAdvValue = new Dictionary<char, List<int>>();
            for (int i = 0; i < line.Length; i++)
            {
                StatisticAccumulator.IterationCountInc(2);
                if (!rAdvValue.ContainsKey(line[i]))
                {
                    rAdvValue.Add(line[i], new List<int>());
                    StatisticAccumulator.IterationCountInc();
                }
                rAdvValue[line[i]].Add(i);
            }

        }
        //--------------------------------------------------------------------------------------
        public void LiPreprocessString(string line)
        {
            int len = line.Length;
            string lineReverse = line.ToString();
            StatisticAccumulator.IterationCountInc(2);
            lineReverse.Reverse();
            StatisticAccumulator.IterationCountInc(lineReverse.Length);
            zReverseValue = PreprocessString(lineReverse);
            rAdvValue = new Dictionary<char, List<int>>();
            StatisticAccumulator.IterationCountInc(2);
            for (int i = len - 1; i >= 0; i--)
            {
                lisValue[len - zReverseValue[i] + 1] = len - i - 1;
                StatisticAccumulator.IterationCountInc();
            }
            liValue = new int[len];
            liValue[1] = lisValue[1];
            StatisticAccumulator.IterationCountInc(2);
            for (int i = 2; i < line.Length; i++)
            {
                liValue[1] = Math.Max(lisValue[i - 1], lisValue[1]);
                StatisticAccumulator.IterationCountInc(2);
            }
        }
        //--------------------------------------------------------------------------------------
        public int[] NiPreprocessString(string line)
        {
            int len = line.Length;
            string lineReverse = line.ToString();
            lineReverse.Reverse();
            StatisticAccumulator.IterationCountInc(4);
            zReverseValue = PreprocessString(lineReverse);
            int[] nvalue = new int[len];
            for (int i = 0; i < len; i++)
            {
                StatisticAccumulator.IterationCountInc();
                nvalue[i] = zReverseValue[len - i - 1];
            }
            return nvalue;
        }
        //--------------------------------------------------------------------------------------
        public void LiByNPreprocessString(string line)
        {
            int len = line.Length;

            int[] nvalue = NiPreprocessString(line);
            lisValue = new int[len];
            liValue = new int[len];
            StatisticAccumulator.IterationCountInc(2);
            for (int i = 0; i < len; i++)
            {
                StatisticAccumulator.IterationCountInc();
                if (nvalue[i] > 0)
                {
                    lisValue[len - nvalue[i]] = i;
                    StatisticAccumulator.IterationCountInc();
                }
            }
            liValue[1] = lisValue[1];
            StatisticAccumulator.IterationCountInc();
            for (int i = 2; i < line.Length; i++)
            {
                StatisticAccumulator.IterationCountInc();
                liValue[1] = Math.Max(lisValue[i - 1], lisValue[1]);
            }
        }
        //--------------------------------------------------------------------------------------
        public void LliPreprocessString(string line)
        {
            int len = line.Length;

            int[] zvalue = PreprocessString(line);
            llisValue = new int[len];
            int llisCurrent = 0;
            StatisticAccumulator.IterationCountInc(2);
            for (int i = len - 1; i >= 0; i--)
            {
                StatisticAccumulator.IterationCountInc(2);
                if (zvalue[i] == len - i)
                {
                    llisCurrent = len - i;
                    StatisticAccumulator.IterationCountInc();
                }
                llisValue[i] = llisCurrent;
            }
        }
        //--------------------------------------------------------------------------------------
        public void SpPreprocessString(string line)
        {
            int len = line.Length;

            int[] zvalue = PreprocessString(line);
            spsValue = new int[len];
            StatisticAccumulator.IterationCountInc(2);
            for (int i = len - 1; i >= 0; i--)
            {
                StatisticAccumulator.IterationCountInc();
                if (zvalue[i] > 0)
                {
                    spsValue[i + zvalue[i] - 1] = zvalue[i];
                    StatisticAccumulator.IterationCountInc();
                }
            }
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
