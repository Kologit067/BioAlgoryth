using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepresentativesSet
{
    //--------------------------------------------------------------------------------------
    // class RepresentativesBranchAndBoundByValue
    //--------------------------------------------------------------------------------------
    public class RepresentativesBranchAndBoundByValue : EnumerateBinVectors
    {
        protected int[][] listOfSet;
        protected int currentMinimum;
        protected List<int> _fCurrentOptimalSet;		    // текущий оптимальный набор элементов
        protected List<string> _fOptimalSets;		        // 
        protected int _fCurrentCardinality;
        //--------------------------------------------------------------------------------------
        public RepresentativesBranchAndBoundByValue(int pLength, int[][] pListOfSet)
            : base(pLength)
        {
            listOfSet = pListOfSet;
            if (listOfSet.Any(s => s.Any(e => e >= pLength)))
                throw new ArgumentException("Element of set can not be > Length.");
            _fCurrentOptimalSet = _fCurrentSet.ToList();
            currentMinimum = pLength;
            _fOptimalSets = new List<string>();
            _fCurrentCardinality = 0;
        }
        //--------------------------------------------------------------------------------------
        protected override void RemoveAction(int element)
        {
            _fCurrentCardinality -= element;
        }
        //--------------------------------------------------------------------------------------
        protected override void AddAction(int element)
        {
            _fCurrentCardinality += element;
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
                    if (_fCurrentCardinality < currentMinimum)
                    {
                        for (int i = 0; i < _fCurrentSet.Count; i++)
                        {
                            _fCurrentOptimalSet[i] = _fCurrentSet[i];
                        }
                        currentMinimum = _fCurrentCardinality;
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
            return false;
        }
        //--------------------------------------------------------------------------------------
        protected override bool IsCompleteCondition()
        {
            IterationAction();
            if ((_fCurrentPosition >= _fSize - 1) || (_fCurrentCardinality > currentMinimum))
            {
                TerminalAction();
                return true;
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
}
