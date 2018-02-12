using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExactStringCompare
{
    public class BruteForceStringCompare
    {
        public List<int> FindSubstring(string text, string pattern)
        {
            List<int> result = new List<int>();
            int max = text.Length - pattern.Length;
            for ( int i = 0; i < max; i++)
            {
                int j = 0;
                for (; j < pattern.Length; j++)
                {
                    if (text[i] != pattern[j])
                        break;
                }
                if (j == pattern.Length)
                    result.Add(i);
            }
            return result;
        }
    }
}
