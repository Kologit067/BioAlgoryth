using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    //--------------------------------------------------------------------------------------
    // class EnumerateintegervariableSubsequence 
    //--------------------------------------------------------------------------------------
    public class EnumerateintegervariableSubsequence : EnumerateintegervariableSet
    {
        protected char[][] _charSets;
        protected int _substringLength;
        //--------------------------------------------------------------------------------------
        public EnumerateintegervariableSubsequence(char[][] pCharSets, int pSubstringLength, int[] pMinimumValues = null)
            : base(pCharSets.Select(a => a.Length - pSubstringLength).ToArray(), pCharSets.Length, pMinimumValues)
        {
            _charSets = pCharSets;
            _substringLength = pSubstringLength;
        }
        //--------------------------------------------------------------------------------------
    }
}
