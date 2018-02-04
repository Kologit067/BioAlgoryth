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
                        while (line[j-li] == line[j])
                            j++;
                        li = i;
                        Ki = j - 1;
                        zValue[i] = Ki - li;
                    }
                }
            }
        }
    }
}
