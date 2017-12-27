using CommonLibrary;
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
        public EnumerateDNAMappingByIntegerTrangle(int[] pairwiseDifferences, int pMinimumValue = 1, int pForwardAdditive = 1, bool pIsAllResult = true)
            : base(pairwiseDifferences.Max(), DNAMappingBase.DefineRestrictionMapSizeFromDifferencesSize(pairwiseDifferences.Length), pMinimumValue ,pForwardAdditive)
        {
            _pairwiseDifferences = pairwiseDifferences.OrderBy(c => c).ToArray();
            _isAllResult = pIsAllResult;
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                var pairwiseDifferencesForCurrentSet = DNAMappingBase.ProduceMatrix(_fCurrentSet);
                if (_pairwiseDifferences.SequenceEqual(pairwiseDifferencesForCurrentSet.OrderBy(d=>d)))
                {
                    if (_solution == null)
                        _solution = _fCurrentSet.ToList();
                    _listOfSolution.Add(_fCurrentSet.ToList());
                    return !_isAllResult;
                }
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
    }
}
