using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativesSet.BranchAndBound
{
    //--------------------------------------------------------------------------------------
    // class RepresentativesTriangleBranchAndBound 
    //--------------------------------------------------------------------------------------
    public class RepresentativesTriangleBranchAndBound : RepresentativesTriangle
    {
        //--------------------------------------------------------------------------------------
        public RepresentativesTriangleBranchAndBound(int pLength, string pListOfSetAsString) : base(pLength, RepresentativesTriangle.StringToArray(pListOfSetAsString))
        {
        }
        //--------------------------------------------------------------------------------------
        public RepresentativesTriangleBranchAndBound(int pLength, int[][] pListOfSet) : base(pLength, pListOfSet)
        {
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (commonCounter == SetList.Count && _fCurrentPosition < currentMinimum)
            {
                UpdateOptimalResults(_fCurrentPosition + 1);
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
        protected override bool IsCompleteCondition()
        {
            StatisticAccumulator.IterationCountInc();
            IterationAction();
            if (IsCompleteByCardinality())
            {
                if (commonCounter < SetList.Count)
                    StatisticAccumulator.ElemenationCountInc();
                else
                    StatisticAccumulator.TerminalCountInc();
                TerminalAction();
                return true;
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
        protected virtual bool IsCompleteByCardinality()
        {
            return commonCounter == SetList.Count || _fCurrentPosition >= currentMinimum;
        }
        //--------------------------------------------------------------------------------------
        protected override void SupplementInitial()
        {
            StatisticAccumulator.CreateStatistics(listOfSet, _inputDataShort, nameof(RepresentativesTriangleBranchAndBound));
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
