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
                    i += lenPattern - llisValue[1];
                }
                else
                {
                    if (j < pattern.Length - 1)
                    {
                        j++;
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

            return result;
        }
        //--------------------------------------------------------------------------------------
        public List<int> FindSubstring(string text, string pattern)
        {
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
                    i += lenPattern - llisValue[1];
                }
                else
                {
                    int suffixStiff = 1;
                    int symbolStiff = 1;
                    if (rValue.ContainsKey(text[j0]))
                    {
                        if (rValue[text[j0]] < j)
                        {
                            symbolStiff  = rValue[text[j0]] - j;
                        }
                    }
                    else
                        symbolStiff = j0 + 1 - i;

                    if (j < pattern.Length - 1)
                    {
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

                    int stiff = Math.Max(symbolStiff, suffixStiff);
                    i += stiff;
                }
            }

            return result;
        }
        //--------------------------------------------------------------------------------------
        public List<int> FindSubstringByGoodSuffixBadSymbolAdv(string text, string pattern)
        {
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
                    i += lenPattern - llisValue[1];
                }
                else
                {
                    int suffixStiff = 1;
                    int symbolStiff = 1;

                    if (rAdvValue.ContainsKey(text[j0]))
                    {
                        int l = 0;
                        while (l < rAdvValue[text[j0]].Count && rAdvValue[text[j0]][l] < j)
                            l++;
                        if (l > 0)
                            l--;
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

                    int stiff = Math.Max(symbolStiff, suffixStiff);
                    i += stiff;
                }
            }

            return result;
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
