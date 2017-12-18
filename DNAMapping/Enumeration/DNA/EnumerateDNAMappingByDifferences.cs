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
        protected int[] _originePairwiseDifferences;
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
            : base(pairwiseDifferences.Length, DNAMappingBase.DefineRestrictionMapSizeFromDifferencesSize(pairwiseDifferences.Length), 0, 1)
        {
            _originePairwiseDifferences = pairwiseDifferences.OrderBy(c => c).ToArray();
            var list = pairwiseDifferences.Distinct().ToList();
            list.Add(0);
            _pairwiseDifferences = list.OrderBy(c => c).ToArray();
            _isAllResult = pIsAllResult;
            _fLimit = _pairwiseDifferences.Length-1;
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (fCurrentPosition == _fSize - 1)
            {
                var pairwiseDifferencesForCurrentSet = DNAMappingBase.ProduceMatrixOnIndexBase(fCurrentSet, _pairwiseDifferences);
                if (_originePairwiseDifferences.SequenceEqual(pairwiseDifferencesForCurrentSet.OrderBy( d => d)))
                {
                    if (_solution == null)
                        _solution = fCurrentSet;
                    _listOfSolution.Add(fCurrentSet.Select(i => _pairwiseDifferences[i]).ToList());
                    return !_isAllResult;
                }
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
    }
}
