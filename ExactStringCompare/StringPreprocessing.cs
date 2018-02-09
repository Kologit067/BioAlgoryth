using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExactStringCompare
{
    public class StringPreprocessing
    {
        protected int[] zValue;
        //        protected char[] alphabet;
        protected Dictionary<char, int> rValue;
        protected Dictionary<char, List<int>> rAdvValue;
        protected int[] liValue;
        protected int[] lisValue;
        public void PreprocessString(string line)
        {
            int li = 0;
            int Ki = 0;
            zValue = new int[line.Length];
            for (int i = 1; i < line.Length; i++)
            {
                if (li <= i)
                {
                    if (line[i] == line[0])
                    {
                        int j = 0;
                        while (line[j] == line[i + j])
                            j++;
                        li = i;
                        Ki = j - 1;
                        zValue[i] = Ki - li;
                    }
                }
                else
                {
                    int i0 = i - li;
                    if (zValue[i0] + i0 < zValue[li])
                    {
                        zValue[i] = zValue[i0];
                    }
                    else
                    {

                        int j = Ki;
                        while (line[j - li] == line[j])
                            j++;
                        li = i;
                        Ki = j - 1;
                        zValue[i] = Ki - li;
                    }
                }
            }
        }

        public void PreprocessNValueString(string line)
        {
            int lastPosition = line.Length;
            int li = lastPosition;
            int Ki = lastPosition;
            zValue = new int[line.Length];
            for (int i = lastPosition-1; i >= 0; i--)
            {
                if (li >= i)
                {
                    if (line[i] == line[lastPosition])
                    {
                        int j = lastPosition;
                        while (line[j] == line[i - j])
                            j--;
                        li = i;
                        Ki = j + 1;
                        zValue[i] = li - Ki;
                    }
                }
                else
                {
                    // ---------------------
                    int i0 = i + li;
                    if (zValue[i0] - i0 < zValue[li])
                    {
                        zValue[i] = zValue[i0];
                    }
                    else
                    {

                        int j = Ki;
                        while (line[j - li] == line[j])
                            j++;
                        li = i;
                        Ki = j - 1;
                        zValue[i] = Ki - li;
                    }
                }
            }
        }

        public void BadSymbolPreprocessString(string line)
        {

            rAdvValue = new Dictionary<char, List<int>>();
            for (int i = 1; i < line.Length; i++)
            {
                if (!rAdvValue.ContainsKey(line[i]))
                    rAdvValue.Add(line[i], new List<int>());
                rAdvValue[line[i]].Add(i);
            }
        }

        public void BadSymbolAdvPreprocessString(string line)
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

        public void LiPreprocessString(string line)
        {
            int len = line.Length;
            string lineReverse = line.ToString();
            lineReverse.Reverse();
            PreprocessString(lineReverse);
            rAdvValue = new Dictionary<char, List<int>>();
            for (int i = len - 1; i >= 0; i--)
            {
                lisValue[len - zValue[i] + 1] = len - i - 1;
            }
            liValue[1] = lisValue[1];
            for (int i = 2; i < line.Length; i++)
            {
                liValue[1] = Math.Max(lisValue[i-1], lisValue[1]);
            }
        }
    }
}
