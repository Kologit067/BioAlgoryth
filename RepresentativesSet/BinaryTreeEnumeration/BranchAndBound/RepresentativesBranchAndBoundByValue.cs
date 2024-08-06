using System;
using System.Collections.Generic;

namespace RepresentativesSet
{
    //--------------------------------------------------------------------------------------
    // class RepresentativesBranchAndBoundByValue
    //--------------------------------------------------------------------------------------
    public class RepresentativesBranchAndBoundByValue : RepresentativesAsTree
    {
        protected int _currentCardinality;
        //--------------------------------------------------------------------------------------
        public RepresentativesBranchAndBoundByValue(int pLength)
            : base(pLength)
        {
        }
        //-----------------------------------------------------------------------------------
        protected override void SupplementInitial()
        {
            StatisticAccumulator.CreateStatistics(listOfSet, _inputDataShort, AlgorithmName);
            _currentCardinality = _fCurrentSet[0];
        }
        //-----------------------------------------------------------------------------------
        public override void Execute(int[][] pListOfSet)
        {
            _currentCardinality = 0;
            base.Execute(pListOfSet);
        }
        //--------------------------------------------------------------------------------------
        protected override void RemoveAction(int element)
        {
            _currentCardinality -= element;
        }
        //--------------------------------------------------------------------------------------
        protected override void AddAction(int element)
        {
            _currentCardinality += element;
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1 && _currentCardinality <= currentMinimum)
            {
                bool isIntersect = IsIntersect();
                if (isIntersect)
                {
                    UpdateOptimalResults(_currentCardinality);
                }
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
        protected override bool IsCompleteCondition()
        {
            StatisticAccumulator.IterationCountInc();
            IterationAction();
            if ((_fCurrentPosition >= _fSize - 1) || IsCompleteByCardinality())
            {
                if (_fCurrentPosition < _fSize - 1)
                    StatisticAccumulator.ElemenationCountInc();
                else
                    StatisticAccumulator.TerminalCountInc();
                TerminalAction();
                return true;
            }
            return false;
        }
        //-----------------------------------------------------------------------------------
        protected virtual bool IsCompleteByCardinality()
        {
            return _currentCardinality > currentMinimum;
        }

        //-----------------------------------------------------------------------------------
        protected override void PostAction()
        {
            StatisticAccumulator.SaveStatisticData(ElapsedTicks, DurationMilliSeconds, DateTime.Now,
                IsComplete, CurrentSetAsString, _fOptimalSets, currentMinimum);
        }
        //--------------------------------------------------------------------------------------
        public List<int> Result
        {
            get
            {
                List<int> result = new List<int>();
                for (int i = 0; i < _fCurrentOptimalSet.Count; i++)
                {
                    if (_fCurrentOptimalSet[i] != 0)
                        result.Add(i);
                }
                return result;
            }
        }
        //--------------------------------------------------------------------------------------
        public List<string> OptimalSets
        {
            get
            {
                return _fOptimalSets;
            }
            set
            {
                _fOptimalSets = value;
            }
        }
    }
    //--------------------------------------------------------------------------------------
    // class RepresentativesBranchAndBoundFirst
    //--------------------------------------------------------------------------------------
    public class RepresentativesBranchAndBoundFirst : RepresentativesBranchAndBoundByValue
    {
        //--------------------------------------------------------------------------------------
        public RepresentativesBranchAndBoundFirst(int pLength)
            : base(pLength)
        {
        }
        //-----------------------------------------------------------------------------------
        protected override bool IsCompleteByCardinality()
        {
            return _currentCardinality >= currentMinimum;
        }
    }
}
