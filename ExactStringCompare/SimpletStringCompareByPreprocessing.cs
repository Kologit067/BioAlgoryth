using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExactStringCompare
{
    public class SimpletStringCompareByPreprocessing : StringPreprocessing
    {
        public List<int> FindSubstring(string text, string pattern)
        {
            List<int> result = new List<int>();
            string totalString = pattern + text;
            zValue = PreprocessString(totalString);
            int lenPattern = pattern.Length;
            for(int i = lenPattern; i < totalString.Length; i++)
            {
                if (zValue[i] == lenPattern)
                    result.Add(i);
            }

            return result;
        }
    }
}
