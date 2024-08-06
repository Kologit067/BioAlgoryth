using BaseContract;
using CommonLibrary;
using StatisticsStorage.Accumulators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepresentativesSet
{
    //--------------------------------------------------------------------------------------
    // class BruteForceRepresentativesAsTree
    //--------------------------------------------------------------------------------------
    public class BruteForceRepresentativesAsTree : RepresentativesAsTree
    {
        //--------------------------------------------------------------------------------------
        public BruteForceRepresentativesAsTree(int pLength)
            : base(pLength)
        {
        }
        //-----------------------------------------------------------------------------------
        public override void Execute(int[][] pListOfSet)
        {
            base.Execute(pListOfSet);
        }
        //-----------------------------------------------------------------------------------
        public virtual void Execute(string setAsString)
        {
            Execute(RepresentativesTriangle.StringToArray(setAsString));
        }        
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                bool isIntersect = IsIntersect();
                if (isIntersect)
                {
                    int candidatValue = _fCurrentSet.Sum();
                    if (candidatValue <= currentMinimum)
                    {
                        UpdateOptimalResults(candidatValue);
                    }
                }
                StatisticAccumulator.TerminalCountInc();
            }
            return false;
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
        //-----------------------------------------------------------------------------------
        protected override void SupplementInitial()
        {
            StatisticAccumulator.CreateStatistics(listOfSet, _inputDataShort, AlgorithmName);
        }       
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
