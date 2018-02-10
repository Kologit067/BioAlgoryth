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
    public class BoyerMooreCompare : StringPreprocessing
    {
        //--------------------------------------------------------------------------------------
        public List<int> FindSubstringBadSymbolAdv(string text, string pattern)
        {
            List<int> result = new List<int>();
            BadSymbolAdvPreprocessString(pattern);
            int lenPattern = pattern.Length;
            int i = 0;
            while (i < text.Length - pattern.Length)
            {
                int j = pattern.Length - 1;
                int j0 = j + i;
                while (j >= 0)
                {
                    if (pattern[j] != text[j0])
                        break;
                    j--;
                    j0--;
                }
                if (j < 0)
                {
                    result.Add(i);
                    i++;
                }
                else
                {
                    if (rAdvValue.ContainsKey(text[j0]))
                    {
                        int l = 0;
                        while (l < rAdvValue[text[j0]].Count && rAdvValue[text[j0]][l] < j)
                            l++;
                        if (l > 0)
                            l--;
                        int maxPos = rAdvValue[text[j0]][l];
                        if (l >= j)
                            i = j0 + 1;
                        else
                        {
                            i += maxPos - j;
                        }
                    }
                    else
                        i = j0 + 1;
                }
            }

            return result;
        }
        //--------------------------------------------------------------------------------------
        public List<int> FindSubstringBadSymbol(string text, string pattern)
        {
            List<int> result = new List<int>();
            BadSymbolPreprocessString(pattern);
            int lenPattern = pattern.Length;
            int i = 0;
            while (i < text.Length - pattern.Length)
            {
                int j = pattern.Length - 1;
                int j0 = j + i;
                while (j >= 0)
                {
                    if (pattern[j] != text[j0])
                        break;
                    j--;
                    j0--;
                }
                if (j < 0)
                {
                    result.Add(i);
                    i++;
                }
                else
                {
                    if (rValue.ContainsKey(text[j0]))
                    {
                        if (rValue[text[j0]] >= j)
                            i++;
                        else
                        {
                            i += rValue[text[j0]] - j;
                        }
                    }
                    else
                        i = j0 + 1;
                }
            }

            return result;
        }
        //--------------------------------------------------------------------------------------
        public List<int> FindSubstringGoodSuffix(string text, string pattern)
        {
            List<int> result = new List<int>();
            LliPreprocessString(pattern);
            LiByNPreprocessString(pattern);
            int lenPattern = pattern.Length;
            int i = 0;
            while (i < text.Length - pattern.Length)
            {
                int j = pattern.Length - 1;
                int j0 = j + i;
                while (j >= 0)
                {
                    if (pattern[j] != text[j0])
                        break;
                    j--;
                    j0--;
                }
                if (j < 0)
                {
                    result.Add(i);
                    i++;
                }
                else
                {
                    if (rAdvValue.ContainsKey(text[j0]))
                    {
                        int l = 0;
                        while (l < rAdvValue[text[j0]].Count && rAdvValue[text[j0]][l] < j)
                            l++;
                        if (l > 0)
                            l--;
                        int maxPos = rAdvValue[text[j0]][l];
                        if (l >= j)
                            i = j0 + 1;
                        else
                        {
                            i += maxPos - j;
                        }
                    }
                    else
                        i = j0 + 1;
                }
            }

            return result;
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
