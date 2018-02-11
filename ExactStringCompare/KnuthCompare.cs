using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExactStringCompare
{
    //--------------------------------------------------------------------------------------
    // class BoyerMooreCompare 
    //--------------------------------------------------------------------------------------
    public class KnuthCompare : StringPreprocessing
    {
        //--------------------------------------------------------------------------------------
        public List<int> FindSubstring(string text, string pattern)
        {
            List<int> result = new List<int>();
            SpPreprocessString(pattern);
            int lenPattern = pattern.Length;
            int i = 0;
            int jstart = 0;
            while (i < text.Length - pattern.Length)
            {
                int j = jstart;
                int j0 = i + j;
                while (j < pattern.Length)
                {
                    if (pattern[j] != text[j0])
                        break;
                    j++;
                    j0++;
                }
                if (j == pattern.Length)
                {
                    result.Add(i);
                    i += lenPattern - spsValue[pattern.Length-1];
                    jstart = spsValue[pattern.Length - 1];
                }
                else
                {
                    int stiff = 1;
                    jstart = 0;

                    if (j > 0)
                    {
                        stiff = lenPattern - spsValue[--j];
                        jstart = spsValue[--j];
                    }

                    i += stiff;
                }
            }

            return result;
        }
    }
}
