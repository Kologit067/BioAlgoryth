﻿using BaseContract;
using CommonLibrary.Objects;
using StatisticsStorage.Accumulators.Objects;
using StatisticsStorage.Savers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsStorage.Accumulators
{
    //--------------------------------------------------------------------------------------------------------------------
    // class RepresentativesStatisticAccumulator
    //--------------------------------------------------------------------------------------------------------------------
    public class RepresentativesStatisticAccumulator : IRepresentativesStatisticAccumulator
    {
        protected List<RepresentativesPerfomance> _representativesPerfomances;
        protected RepresentativesPerfomance _currentRepresentativesPerfomance;
        protected RepresentativesSaver _representativesSaver;
        protected int _bufferSize;
        protected int _numberOfSet;
        protected int _dimension;        
        //--------------------------------------------------------------------------------------------------------------------
        public RepresentativesStatisticAccumulator(RepresentativesSaver representativesSaver, int numberOfSet, int dimension, int bufferSize = 100)
        {
            _numberOfSet = numberOfSet;
            _dimension = dimension;
            _representativesSaver = representativesSaver;
            _bufferSize = bufferSize;
            _representativesPerfomances = new List<RepresentativesPerfomance>();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void CreateStatistics(int[][] listOfSet, string inputDataShort, string algorithm)
        {
            _currentRepresentativesPerfomance = new RepresentativesPerfomance(_numberOfSet, _dimension, listOfSet, inputDataShort, algorithm);
            _representativesPerfomances.Add(_currentRepresentativesPerfomance);
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveStatisticData(long duration, long durationMilliSeconds, DateTime dateComplete,
            bool isComplete, string lastRoute, List<string> optimalSets, int bestValue)
        {
            _currentRepresentativesPerfomance.SaveStatisticData(duration, durationMilliSeconds, dateComplete,
            isComplete, lastRoute, optimalSets, bestValue);
            if (_representativesPerfomances.Count > _bufferSize)
            {
                _representativesSaver.Save(_representativesPerfomances);
                _representativesPerfomances.Clear();
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void IterationCountInc()
        {
            _currentRepresentativesPerfomance.IterationCountInc();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void TerminalCountInc()
        {
            _currentRepresentativesPerfomance.TerminalCountInc();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void UpdateOptcountInc()
        {
            _currentRepresentativesPerfomance.UpdateOptcountInc();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void ElemenationCountInc()
        {
            _currentRepresentativesPerfomance.ElemenationCountInc();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveRemain()
        {
            if (_representativesPerfomances.Count > 0)
                _representativesSaver.Save(_representativesPerfomances);
            _representativesPerfomances.Clear();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public string Delete(string algorithm)
        {
            return _representativesSaver.Delete(algorithm, _numberOfSet, _dimension);
        }
        //--------------------------------------------------------------------------------------------------------------------
        public string DeleteAlgorithm(string algorithm, int? numberOfSet = null, int? dimension = null)
        {
            return _representativesSaver.Delete(algorithm, numberOfSet, dimension);
        }

        public void RemoveLastStatistic()
        {
            if (_representativesPerfomances.Count > 0)
                _representativesPerfomances.RemoveAt(_representativesPerfomances.Count - 1);
        }
        //--------------------------------------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------------------------------------
    // class FakeRepresentativesStatisticAccumulator
    //--------------------------------------------------------------------------------------------------------------------
    public class FakeRepresentativesStatisticAccumulator : IRepresentativesStatisticAccumulator
    { 
        //--------------------------------------------------------------------------------------------------------------------
        public FakeRepresentativesStatisticAccumulator()
        {
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void CreateStatistics(int[][] listOfSet, string inputDataShort, string algorithm)
        {
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveStatisticData(long duration, long durationMilliSeconds, DateTime dateComplete,
            bool isComplete, string lastRoute, List<string> optimalSets, int bestValue)
        {
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void IterationCountInc()
        {
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void TerminalCountInc()
        {
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void UpdateOptcountInc()
        {
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void ElemenationCountInc()
        {
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveRemain()
        {
        }
        //--------------------------------------------------------------------------------------------------------------------
        public string Delete(string algorithm)
        {
            return "";
        }
        //--------------------------------------------------------------------------------------------------------------------
    }
}
