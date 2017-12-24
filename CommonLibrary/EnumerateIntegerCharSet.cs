using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    //--------------------------------------------------------------------------------------
    // class EnumerateIntegerCharSet 
    //--------------------------------------------------------------------------------------
    public class EnumerateIntegerCharSet : EnumerateIntegerFullSet
    {
        protected char[] _charSet;
        //--------------------------------------------------------------------------------------
        public EnumerateIntegerCharSet(char[] pCharSet, int pLength, int pMinimumValue = 0)
            : base(pCharSet.Length-1, pLength, pMinimumValue)
        {
            _charSet = pCharSet;
        }
        //--------------------------------------------------------------------------------------
    }
}
