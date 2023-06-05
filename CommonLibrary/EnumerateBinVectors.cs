using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class EnumerateBinVectors : EnumerateIntegerFullSet
    {
        //--------------------------------------------------------------------------------------
        public EnumerateBinVectors(int pLength)
            : base(1, pLength, 0)
        {
        }
    }
    public class EnumerateReverseBinVectors : EnumerateIntegerFullSet
    {
        //--------------------------------------------------------------------------------------
        public EnumerateReverseBinVectors(int pLength)
            : base(1, pLength, 0)
        {
        }

        //--------------------------------------------------------------------------------------
        protected override int FirstElement(int pPosition)
        {
            return _fLimit;
        }
        //--------------------------------------------------------------------------------------
        protected override bool NextElement(int pPosition)
        {
            if (_fCurrentSet[pPosition] <= _fMinimumValue)
                return false;
            _fCurrentSet[pPosition]--;
            return true;
        }
    }
}
