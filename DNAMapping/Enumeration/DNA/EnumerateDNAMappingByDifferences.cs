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
        protected List<List<int>> _listOfSolution = new List<List<int>>();
        protected bool _isAllResult;
        //--------------------------------------------------------------------------------------
        public List<int> Solution
        {
            get
            {
                return _solution;
            }
        }
        //--------------------------------------------------------------------------------------
        public List<List<int>> ListOfSolution
        {
            get
            {
                return _listOfSolution;
            }
        }
        //--------------------------------------------------------------------------------------
        public EnumerateDNAMappingByDifferences(int[] pairwiseDifferences, bool pIsAllResult = true)
            : base(pairwiseDifferences.Length - 1, DNAMappingBase.DefineRestrictionMapSizeFromDifferencesSize(pairwiseDifferences.Length), 0, 1)
        {
            _pairwiseDifferences = pairwiseDifferences.OrderBy(c => c).ToArray();
            _isAllResult = pIsAllResult;
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (fCurrentPosition == _fSize - 1)
            {
                var pairwiseDifferencesForCurrentSet = DNAMappingBase.ProduceMatrixOnIndexBase(fCurrentSet, _pairwiseDifferences);
                if (_pairwiseDifferences.SequenceEqual(pairwiseDifferencesForCurrentSet.OrderBy( d => d)))
                {
                    if (_solution == null)
                        _solution = fCurrentSet;
                    _listOfSolution.Add(fCurrentSet.ToList());
                    return !_isAllResult;
                }
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
    }
}
