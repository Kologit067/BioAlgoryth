﻿using BaseContract;
using CommonLibrary;
using StatisticsStorage.Accumulators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepresentativesSet
{
    //--------------------------------------------------------------------------------------
    // class RepresentativesAsTree
    //--------------------------------------------------------------------------------------
    public class RepresentativesAsTree : EnumerateReverseBinVectors
    {
        protected int[][] listOfSet;
        protected long[] listOfSetAsNumber;
        protected int currentMinimum;
        protected List<int> _fCurrentOptimalSet;		    // текущий оптимальный набор элементов
        protected List<string> _fOptimalSets;               // 
        public int CurrentMinimum
        { 
            get 
            { 
                return currentMinimum; 
            } 
        }
        protected string _inputData;
        public string InputData
        {
            get
            {
                return _inputData;
            }
        }
        protected string _inputDataShort;
        public string InputDataShort
        {
            get
            {
                return _inputDataShort;
            }
        }
        public IRepresentativesStatisticAccumulator StatisticAccumulator { get; set; }
        public RepresentativesAsTree(int pLength, int[][] pListOfSet) : base(pLength)
        {
            listOfSet = pListOfSet;
            listOfSetAsNumber = listOfSet.Select(s => BruteForceRepresentatives.ElementNumbersToLongAsBinaryVector(s)).ToArray();
            if (listOfSet.Any(s => s.Any(e => e >= pLength)))
                throw new ArgumentException("Element of set can not be > Length.");
            _fCurrentOptimalSet = _fCurrentSet.ToList();
            currentMinimum = pLength;
            _fOptimalSets = new List<string>();
            StatisticAccumulator = new FakeRepresentativesStatisticAccumulator();
        }
        //-----------------------------------------------------------------------------------
        protected override void SupplementInitial()
        {
            _inputData = (Newtonsoft.Json.JsonConvert.SerializeObject(listOfSet));
            _inputDataShort = (Newtonsoft.Json.JsonConvert.SerializeObject(listOfSetAsNumber));
            StatisticAccumulator.CreateStatistics(_inputData, _inputDataShort, nameof(RepresentativesBranchAndBoundByValue));
        }
        //--------------------------------------------------------------------------------------
        protected bool IsIntersect()
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
            return isIntersect;
        }
        //--------------------------------------------------------------------------------------
        protected void UpdateOptimalResults(int candidatValue)
        {
            if (candidatValue < currentMinimum)
            {
                StatisticAccumulator.UpdateOptcountInc();
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
        //--------------------------------------------------------------------------------------
    }
}
