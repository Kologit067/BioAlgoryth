using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            _originePairwiseDifferences = pairwiseDifferences.OrderBy(d => d).Select(p => new DifferenceElement() { Data = p, IsIncluded = false }).ToArray();
            var list = pairwiseDifferences.Distinct().ToList();
            list.Add(0);
            _pairwiseDifferences = list.OrderBy(c => c).ToArray();
            _isAllResult = pIsAllResult;
            _fLimit = _pairwiseDifferences.Length - 1;
        }
        //--------------------------------------------------------------------------------------
        protected override bool IsCompleteCondition()
        {
            fIterationCount++;
            if (_fCurrentPosition == 0)
                return false;
            if ( _fCurrentSet[0] > 0 || !ChaeckCurrentPart())
                return true;
            if (_fCurrentPosition >= _fSize - 1)
            {
                if (_solution == null)
                    _solution = _fCurrentSet.Select(s => _pairwiseDifferences[s]).ToList();
                _listOfSolution.Add(_fCurrentSet.Select(s => _pairwiseDifferences[s]).ToList());
                return true;
            }
            else if (_fCurrentSet[_fCurrentPosition] + _forwardAdditive > _fLimit)
                return true;
            return false;
        }
        //--------------------------------------------------------------------------------------
        private bool ChaeckCurrentPart()
        {
            //DifferenceElement elementFromOrigine = _originePairwiseDifferences.FirstOrDefault(e => e.Data == _pairwiseDifferences[fCurrentSet[fCurrentPosition]] && !e.IsIncluded);
            //if (elementFromOrigine.Data == 0)
            //    throw new Exception("Logical error in EnumerateDNAMappingBranchBoundary.ChaeckCurrentPart (search current diff in _originePairwiseDifferences)");
            //elementFromOrigine.IsIncluded = true;
            for (int i = 0; i < _fCurrentPosition; i++)
            {
                int d = _pairwiseDifferences[_fCurrentSet[_fCurrentPosition]] - _pairwiseDifferences[_fCurrentSet[i]];
                DifferenceElement element = _originePairwiseDifferences.FirstOrDefault(e => e.Data == d && !e.IsIncluded);
                if (element == null)
                {
                    for (int j = 0; j < i; j++)
                    {
                        int revValue = _pairwiseDifferences[_fCurrentSet[_fCurrentPosition]] - _pairwiseDifferences[_fCurrentSet[j]];
                        DifferenceElement revElement = _originePairwiseDifferences.FirstOrDefault(e => e.Data == revValue && e.IsIncluded);
                        if (revElement == null)
                            throw new Exception("Logical error in EnumerateDNAMappingBranchBoundary.ChaeckCurrentPart (Restore state)");
                        revElement.IsIncluded = false;
                    }
//                    elementFromOrigine.IsIncluded = false;
                    return false;
                }
                element.IsIncluded = true;
            }
            return true;
        }
        //--------------------------------------------------------------------------------------
        protected override void RemoveAction(int p)
        {
            if (_fCurrentPosition == 0 )
                return;
            if (_fCurrentSet[0] > 0)
                return;
            int d = _pairwiseDifferences[_fCurrentSet[_fCurrentPosition]];
            DifferenceElement element = _originePairwiseDifferences.FirstOrDefault(e => e.Data == d && e.IsIncluded);
            if (element == null)
                return;
            element.IsIncluded = false;
            for (int i = 1; i < _fCurrentPosition; i++)
            {
                d = _pairwiseDifferences[_fCurrentSet[_fCurrentPosition]] - _pairwiseDifferences[_fCurrentSet[i]];
                element = _originePairwiseDifferences.FirstOrDefault(e => e.Data == d && e.IsIncluded);
                if (element == null)
                    throw new Exception("Logical error in EnumerateDNAMappingBranchBoundary.BackAction");
                element.IsIncluded = false;
            }
            //DifferenceElement elementFromOrigine = _originePairwiseDifferences.FirstOrDefault(e => e.Data == _pairwiseDifferences[fCurrentSet[fCurrentPosition]] && e.IsIncluded);
            //elementFromOrigine.IsIncluded = false;
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_solution != null)
            {
                return !_isAllResult;
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
    [DebuggerDisplay("Data = {Data} - {IsIncluded}")]
    public class DifferenceElement
    {
        public int Data { get; set; }
        public bool IsIncluded { get; set; }
    }
    //--------------------------------------------------------------------------------------
}
