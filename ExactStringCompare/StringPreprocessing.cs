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
                    if (line[i] == line[0])
                    {
                        int j = 0;
                        while (line[j] == line[i + j])
                        {
                            j++;
                            StatisticAccumulator.IterationCountInc();
                        }
                        StatisticAccumulator.IterationCountInc();
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

                        StatisticAccumulator.IterationCountInc();
                        int j = ri;
                        while (line[j - li] == line[j])
                        {
                            j++;
                            StatisticAccumulator.IterationCountInc();
                        }
                        li = i;
                        ri = j - 1;
                        zvalue[i] = ri - li + 1;
                        StatisticAccumulator.IterationCountInc();
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
            for (int i = last - 1; i >= 0; i--)
            {
                if (i < ri)
                {
                    if (line[i] == line[last])
                    {
                        int j = 0;
                        while (line[last - j] == line[i - j])
                            j--;
                        li = i;
                        ri = i - j + 1;
                        nvalue[i] = j;
                    }
                    else
                        nvalue[i] = 0;
                }
                else
                {
                    int i0 = len - li + i;
                    if (nvalue[i0] + li - i < nvalue[li])
                    {
                        nvalue[i] = nvalue[i0];
                    }
                    else
                    {

                        int j = ri;
                        int j2 = last - (i - ri);
                        while (line[j] == line[j2--])
                            j--;
                        li = i;
                        ri = j + 1;
                        nvalue[i] = li - ri + 1;
                    }
                }
            }
            return nvalue;
        }

        //--------------------------------------------------------------------------------------
        public void BadSymbolPreprocessString(string line)
        {
            rValue = new Dictionary<char, int>();
            for (int i = 1; i < line.Length; i++)
            {
                if (rValue.ContainsKey(line[i]))
                    rValue[line[i]] = i;
                else
                    rValue.Add(line[i], i);
            }
        }
        //--------------------------------------------------------------------------------------
        public void BadSymbolAdvPreprocessString(string line)
        {

            rAdvValue = new Dictionary<char, List<int>>();
            for (int i = 1; i < line.Length; i++)
            {
                if (!rAdvValue.ContainsKey(line[i]))
                    rAdvValue.Add(line[i], new List<int>());
                rAdvValue[line[i]].Add(i);
            }

        }
        //--------------------------------------------------------------------------------------
        public void LiPreprocessString(string line)
        {
            int len = line.Length;
            string lineReverse = line.ToString();
            lineReverse.Reverse();
            zReverseValue = PreprocessString(lineReverse);
            rAdvValue = new Dictionary<char, List<int>>();
            for (int i = len - 1; i >= 0; i--)
            {
                lisValue[len - zReverseValue[i] + 1] = len - i - 1;
            }
            liValue[1] = lisValue[1];
            for (int i = 2; i < line.Length; i++)
            {
                liValue[1] = Math.Max(lisValue[i - 1], lisValue[1]);
            }
        }
        //--------------------------------------------------------------------------------------
        public int[] NiPreprocessString(string line)
        {
            int len = line.Length;
            string lineReverse = line.ToString();
            lineReverse.Reverse();
            zReverseValue = PreprocessString(lineReverse);
            int[] nvalue = new int[len];
            for (int i = 0; i < len; i++)
            {
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
            for (int i = 0; i < len; i++)
            {
                if (nvalue[i] > 0)
                    lisValue[len - nvalue[i]] = i;
            }
            liValue[1] = lisValue[1];
            for (int i = 2; i < line.Length; i++)
            {
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
            for (int i = len - 1; i >= 0; i--)
            {
                if (zvalue[i] == len - i)
                    llisCurrent = len - i;
                llisValue[i] = llisCurrent;
            }
        }
        //--------------------------------------------------------------------------------------
        public void SpPreprocessString(string line)
        {
            int len = line.Length;

            int[] zvalue = PreprocessString(line);
            spsValue = new int[len];
            for (int i = len - 1; i >= 0; i--)
            {
                if (zvalue[i] > 0)
                    spsValue[i+zvalue[i]-1] = zvalue[i];
            }
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
