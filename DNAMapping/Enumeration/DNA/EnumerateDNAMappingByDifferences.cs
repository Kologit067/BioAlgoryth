﻿using BaseContract;
using CommonLibrary;
using CommonLibrary.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNAMapping.Enumeration.DNA
{
    //--------------------------------------------------------------------------------------
    // class EnumerateDNAMappingByDifferences 
    //--------------------------------------------------------------------------------------
    public class EnumerateDNAMappingByDifferences : EnumerateIntegerTrangle
    {
        protected int[] _originePairwiseDifferences;
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
        public IDNAMappingStatisticAccumulator StatisticAccumulator { get; set; }
        //--------------------------------------------------------------------------------------
        public EnumerateDNAMappingByDifferences(int[] pairwiseDifferences, bool pIsAllResult = true)
            : base(pairwiseDifferences.Length, DNAMappingBase.DefineRestrictionMapSizeFromDifferencesSize(pairwiseDifferences.Length), 0, 1)
        {
            _originePairwiseDifferences = pairwiseDifferences.OrderBy(c => c).ToArray();
            var list = pairwiseDifferences.Distinct().ToList();
            list.Add(0);
            _pairwiseDifferences = list.OrderBy(c => c).ToArray();
            _isAllResult = pIsAllResult;
            _fLimit = _pairwiseDifferences.Length-1;
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                var pairwiseDifferencesForCurrentSet = DNAMappingBase.ProduceMatrixOnIndexBase(_fCurrentSet, _pairwiseDifferences);
                if (_originePairwiseDifferences.SequenceEqual(pairwiseDifferencesForCurrentSet.OrderBy( d => d)))
                {
                    StatisticAccumulator.UpdateOptcountInc();
                    if (_solution == null)
                    {
                        _solution = _fCurrentSet.Select(i => _pairwiseDifferences[i]).ToList();
                        _listOfSolution.Add(_solution);
                    }
                    _listOfSolution.Add(_fCurrentSet.Select(i => _pairwiseDifferences[i]).ToList());
                    return !_isAllResult;
                }
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
        protected override void IterationAction()
        {
            StatisticAccumulator.IterationCountInc();
        }
        //--------------------------------------------------------------------------------------
        protected override void TerminalAction()
        {
            StatisticAccumulator.TerminalCountInc();
        }
        //-----------------------------------------------------------------------------------
        protected override void SupplementInitial()
        {
            StatisticAccumulator.CreateStatistics(string.Join(",", _originePairwiseDifferences.Select(p => p.ToString())), "EnumerateDNAMappingByDifferences", new AlgorythmParameters()
            {
                IsAllResult = _isAllResult
            });
        }
        //-----------------------------------------------------------------------------------
        protected override void PostAction()
        {
            StatisticAccumulator.SaveStatisticData(OutputPresentation, ElapsedTicks, DurationMilliSeconds, DateTime.Now,
                IsComplete, CurrentSetAsString, OptimalRouteAsString, _listOfSolution);
        }        //-----------------------------------------------------------------------------------
        public override string OptimalRouteAsString
        {
            get
            {
                if (_fCurrentSet != null && _fCurrentSet.Count > 0)
                    return string.Join(",", _fCurrentSet.Select(i => i.ToString()));
                return "Empty";
            }
        }
        //-----------------------------------------------------------------------------------
        public override string OutputPresentation
        {
            get
            {
                if (_solution != null && _solution.Count > 0)
                    return string.Join(",", _solution.Select(i => i.ToString()));
                return "Empty";
            }
        }
        //--------------------------------------------------------------------------------------
    }
}
