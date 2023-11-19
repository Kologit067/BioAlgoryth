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
        public BruteForceRepresentativesAsTree(int pLength, string setAsString)
            : this(pLength, RepresentativesTriangle.StringToArray(setAsString))
        {
        }
        //--------------------------------------------------------------------------------------
        public BruteForceRepresentativesAsTree(int pLength, int[][] pListOfSet)
            : base(pLength, pListOfSet)
        {
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
            StatisticAccumulator.CreateStatistics(listOfSet, _inputDataShort, nameof(BruteForceRepresentativesAsTree));
        }       
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
    public class BruteForceRepresentativesAsTreeDirect : EnumerateBinVectors
    {
        private int[][] listOfSet;
        private int currentMinimum;
        protected List<int> _fCurrentOptimalSet;		    // текущий оптимальный набор элементов
        protected List<string> _fOptimalSets;		        // 
        //--------------------------------------------------------------------------------------
        public BruteForceRepresentativesAsTreeDirect(int pLength, int[][] pListOfSet)
            : base(pLength)
        {
            listOfSet = pListOfSet;
            if (listOfSet.Any(s => s.Any(e => e >= pLength)))
                throw new ArgumentException("Element of set can not be > Length.");
            _fCurrentOptimalSet = _fCurrentSet.ToList();
            currentMinimum = pLength;
            _fOptimalSets = new List<string>();
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                bool isIntersect = true;
                for (int k = 0; k < listOfSet.Length; k++)
                {
                    if (!listOfSet[k].Any(s => _fCurrentSet[s] > 0))
                    {
                        isIntersect = false;
                        break;
                    }
                }
                if (isIntersect)
                {
                    int candidatValue = _fCurrentSet.Sum();
                    if (candidatValue <= currentMinimum)
                    {
                        if (candidatValue < currentMinimum)
                        {
                            for (int i = 0; i < _fCurrentSet.Count; i++)
                            {
                                _fCurrentOptimalSet[i] = _fCurrentSet[i];
                            }
                            currentMinimum = candidatValue;
                            _fOptimalSets.Clear();
                        }
                        List<int> result = new List<int>();
                        for (int i = 0; i < _fCurrentSet.Count; i++)
                        {
                            if (_fCurrentSet[i] != 0)
                                result.Add(i);
                        }
                        _fOptimalSets.Add(string.Join(",", result));
                    }
                }
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
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
