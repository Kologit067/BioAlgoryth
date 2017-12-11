using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNAMapping
{
    //--------------------------------------------------------------------------------------
    // class DNAMappingBase 
    //--------------------------------------------------------------------------------------
    public abstract class DNAMappingBase
    {
        protected int[] _pairwiseDifferences;
        protected int _pairwiseDifferencesSize;
        protected int[] _restrictionMap;
        protected int _restrictionMapSize;
        protected virtual void DefineRestrictionMapSize()
        {
            _pairwiseDifferencesSize = _pairwiseDifferences.Length;
            int baseNumber = (int)Math.Round( Math.Sqrt(2 * _pairwiseDifferencesSize), 0);
            _restrictionMapSize = 0;
            for (int i = baseNumber - 2; i < baseNumber + 3; i++)
                if ((i * (i - 1)) == 2 * _pairwiseDifferencesSize)
                    _restrictionMapSize = i;
            if (_restrictionMapSize == 0)
                throw new Exception("Incorrect size of Pairwise Differences table.");
        }
        public abstract void Calculate(int[] _pairwiseDifferences);
    }
}
