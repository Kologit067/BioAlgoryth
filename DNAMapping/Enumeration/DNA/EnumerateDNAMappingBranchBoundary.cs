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
        protected DifferenceElement[] _pairwiseDifferences;
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
        public EnumerateDNAMappingBranchBoundary(int[] pairwiseDifferences, int length)
            : base(length - 1, length, 0, 1)
        {
            _pairwiseDifferences = pairwiseDifferences.Select(p => new DifferenceElement() { Data = p, IsIncluded = false }).ToArray();
        }
        //--------------------------------------------------------------------------------------
        protected override bool IsCompleteCondition()
        {
            if (ChaeckCurrentPart())
                return true;
            if (fCurrentPosition >= _fSize - 1)
            {
                _solution = fCurrentSet.Select(s => _pairwiseDifferences[s].Data).ToList();
                return true;
            }
            else if (fCurrentSet[fCurrentPosition] + _forwardAdditive > _fLimit)
                return true;
            return false;
        }
        //--------------------------------------------------------------------------------------
        private bool ChaeckCurrentPart()
        {
            for (int i = 0; i < fCurrentSet[fCurrentPosition]; i++)
            {
                int d = _pairwiseDifferences[fCurrentSet[fCurrentPosition]].Data - _pairwiseDifferences[i].Data;
                DifferenceElement element = _pairwiseDifferences.FirstOrDefault(e => e.Data == d && !e.IsIncluded);
                if (element.Data == 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        int revValue = _pairwiseDifferences[fCurrentSet[fCurrentPosition]].Data - _pairwiseDifferences[i].Data;
                        DifferenceElement revElement = _pairwiseDifferences.FirstOrDefault(e => e.Data == revValue && e.IsIncluded);
                        if (revElement.Data == 0)
                            throw new Exception("Logical error in EnumerateDNAMappingBranchBoundary.BackAction");
                        revElement.IsIncluded = false;
                    }
                    return false;
                }
                element.IsIncluded = true;
            }
            _pairwiseDifferences[fCurrentSet[fCurrentPosition]].IsIncluded = true;
            return true;
        }
        //--------------------------------------------------------------------------------------
        protected override void BackAction()
        {
            for (int i = 0; i < fCurrentSet[fCurrentPosition]; i++)
            {
                int d = _pairwiseDifferences[fCurrentSet[fCurrentPosition]].Data - _pairwiseDifferences[i].Data;
                DifferenceElement element = _pairwiseDifferences.FirstOrDefault(e => e.Data == d && e.IsIncluded);
                if (element.Data == 0)
                    throw new Exception("Logical error in EnumerateDNAMappingBranchBoundary.BackAction");
                element.IsIncluded = false;
            }
            _pairwiseDifferences[fCurrentSet[fCurrentPosition]].IsIncluded = false;
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
