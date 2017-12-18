using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNAMapping.Enumeration.DNA
{
    //--------------------------------------------------------------------------------------
    // class EnumerateDNAMappingBranchBoundary 
    //--------------------------------------------------------------------------------------
    public class EnumerateDNAMappingBranchBoundary : EnumerateIntegerTrangle
    {
        protected DifferenceElement[] _originePairwiseDifferences;
        protected List<int> _solution = null;
        protected int[] _pairwiseDifferences;
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
        public EnumerateDNAMappingBranchBoundary(int[] pairwiseDifferences, bool pIsAllResult = true)
            : base(pairwiseDifferences.Length, DNAMappingBase.DefineRestrictionMapSizeFromDifferencesSize(pairwiseDifferences.Length), 0, 1)
        {
            _originePairwiseDifferences = pairwiseDifferences.Select(p => new DifferenceElement() { Data = p, IsIncluded = false }).ToArray();
            var list = pairwiseDifferences.Distinct().ToList();
            list.Add(0);
            _pairwiseDifferences = list.OrderBy(c => c).ToArray();
            _isAllResult = pIsAllResult;
            _fLimit = _pairwiseDifferences.Length - 1;
        }
        //--------------------------------------------------------------------------------------
        protected override bool IsCompleteCondition()
        {
            if (ChaeckCurrentPart())
                return true;
            if (fCurrentPosition >= _fSize - 1)
            {
                if (_solution == null)
                    _solution = fCurrentSet.Select(s => _pairwiseDifferences[s]).ToList();
                _listOfSolution.Add(fCurrentSet.Select(s => _pairwiseDifferences[s]).ToList());
                return true;
            }
            else if (fCurrentSet[fCurrentPosition] + _forwardAdditive > _fLimit)
                return true;
            return false;
        }
        //--------------------------------------------------------------------------------------
        private bool ChaeckCurrentPart()
        {
            DifferenceElement elementFromOrigine = _originePairwiseDifferences.FirstOrDefault(e => e.Data == _pairwiseDifferences[fCurrentSet[fCurrentPosition]] && e.IsIncluded);
            elementFromOrigine.IsIncluded = true;
            for (int i = 0; i < fCurrentSet[fCurrentPosition]; i++)
            {
                int d = _pairwiseDifferences[fCurrentSet[fCurrentPosition]] - _pairwiseDifferences[i];
                DifferenceElement element = _originePairwiseDifferences.FirstOrDefault(e => e.Data == d && !e.IsIncluded);
                if (element.Data == 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        int revValue = _pairwiseDifferences[fCurrentSet[fCurrentPosition]] - _pairwiseDifferences[i];
                        DifferenceElement revElement = _originePairwiseDifferences.FirstOrDefault(e => e.Data == revValue && e.IsIncluded);
                        if (revElement.Data == 0)
                            throw new Exception("Logical error in EnumerateDNAMappingBranchBoundary.BackAction");
                        revElement.IsIncluded = false;
                    }
                    elementFromOrigine.IsIncluded = false;
                    return false;
                }
                element.IsIncluded = true;
            }
            return true;
        }
        //--------------------------------------------------------------------------------------
        protected override void BackAction()
        {
            for (int i = 0; i < fCurrentSet[fCurrentPosition]; i++)
            {
                int d = _pairwiseDifferences[fCurrentSet[fCurrentPosition]] - _pairwiseDifferences[i];
                DifferenceElement element = _originePairwiseDifferences.FirstOrDefault(e => e.Data == d && e.IsIncluded);
                if (element.Data == 0)
                    throw new Exception("Logical error in EnumerateDNAMappingBranchBoundary.BackAction");
                element.IsIncluded = false;
            }
            DifferenceElement elementFromOrigine = _originePairwiseDifferences.FirstOrDefault(e => e.Data == _pairwiseDifferences[fCurrentSet[fCurrentPosition]] && e.IsIncluded);
            elementFromOrigine.IsIncluded = false;
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_solution != null)
            {
                return true;
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
    public struct DifferenceElement
    {
        public int Data { get; set; }
        public bool IsIncluded { get; set; }
    }
    //--------------------------------------------------------------------------------------
}
