using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNAMapping.Enumeration.DNA
{
    //--------------------------------------------------------------------------------------
    // class EnumerateDNAMappingByIntegerTrangle 
    //--------------------------------------------------------------------------------------
    public class EnumerateDNAMappingByIntegerTrangle : EnumerateIntegerTrangle
    {
        protected int[] _pairwiseDifferences;
        private List<int> _solution = null;
        //--------------------------------------------------------------------------------------
        public List<int> Solution
        {
            get
            {
                return _solution;
            }
        }
        //--------------------------------------------------------------------------------------
        public EnumerateDNAMappingByIntegerTrangle(int[] pairwiseDifferences, int pMinimumValue = 1, int pForwardAdditive = 1)
            : base(pairwiseDifferences.Max(), DNAMappingBase.DefineRestrictionMapSizeFromDifferencesSize(pairwiseDifferences.Length), pMinimumValue ,pForwardAdditive)
        {
            _pairwiseDifferences = pairwiseDifferences;
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (fCurrentPosition == _fSize - 1)
            {
                var pairwiseDifferencesForCurrentSet = DNAMappingBase.ProduceMatrix(fCurrentSet);
                if (_pairwiseDifferences.SequenceEqual(pairwiseDifferencesForCurrentSet.OrderBy(d=>d)))
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
