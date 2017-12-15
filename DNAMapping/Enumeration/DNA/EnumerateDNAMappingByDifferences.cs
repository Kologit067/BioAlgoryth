using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNAMapping.Enumeration.DNA
{
    //--------------------------------------------------------------------------------------
    // class EnumerateDNAMappingByDifferences 
    //--------------------------------------------------------------------------------------
    public class EnumerateDNAMappingByDifferences : EnumerateIntegerTrangle
    {
        protected int[] _pairwiseDifferences;
        protected List<int> _solution = null;
        //--------------------------------------------------------------------------------------
        public List<int> Solution
        {
            get
            {
                return _solution;
            }
        }
        //--------------------------------------------------------------------------------------
        public EnumerateDNAMappingByDifferences(int[] pairwiseDifferences, int length)
            : base(length - 1, length, 0, 1)
        {
            _pairwiseDifferences = pairwiseDifferences;
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (fCurrentPosition == _fSize - 1)
            {
                var pairwiseDifferencesForCurrentSet = DNAMappingBase.ProduceMatrixOnIndexBase(fCurrentSet, _pairwiseDifferences);
                if (_pairwiseDifferences.SequenceEqual(pairwiseDifferencesForCurrentSet.OrderBy( d => d)))
                {
                    _solution = fCurrentSet;
                    return true;
                }
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
    }
}
