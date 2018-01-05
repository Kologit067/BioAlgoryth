﻿using BaseContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsStorage.Accumulators
{
    public class RegulatoryMotifsStatisticAccumulator : IRegulatoryMotifsStatisticAccumulator
    {
        protected List<RegulatoryMotifPerfomance> _regulatoryMotifPerfomances;
        protected RegulatoryMotifPerfomance _currentRegulatoryMotifPerfomance;
        public RegulatoryMotifsStatisticAccumulator()
        {
            _regulatoryMotifPerfomances = new List<RegulatoryMotifPerfomance>();
        }
        public void CreateStatistics(int size, string inputData, string algorithm, int numberOdSequence,
                int averageSequenceLength, int motifLength, AlgorythmParameters algorythmParameters
        {
            _currentRegulatoryMotifPerfomance = new RegulatoryMotifPerfomance(size, inputData, algorithm, numberOdSequence, averageSequenceLength, motifLength);
            _regulatoryMotifPerfomances.Add(_currentRegulatoryMotifPerfomance, algorythmParameters);
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void IterationCountInc()
        {
            _currentRegulatoryMotifPerfomance.IterationCountInc();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void TerminalCountInc()
        {
            _currentRegulatoryMotifPerfomance.TerminalCountInc();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void UpdateOptcountInc()
        {
            _currentRegulatoryMotifPerfomance.UpdateOptcountInc();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void ElemenationCountInc()
        {
            _currentRegulatoryMotifPerfomance.ElemenationCountInc();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void AddRegulatoryMotifOptimalValueChange(long duration, long durationMilliSeconds,
            int optimalValue, string startPosition, string motif)
        {
            _currentRegulatoryMotifPerfomance.RegulatoryMotifOptimalValueChanges.Add(new RegulatoryMotifOptimalValueChange
            (_currentRegulatoryMotifPerfomance.IterationCount, duration, durationMilliSeconds, optimalValue, startPosition, motif));
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveStatisticData(string outputPresentation, int optimalValue, long duration, long durationMilliSeconds, DateTime dateComplete,
            bool isComplete, string lastRoute, string optimalRoute, List<List<char>> listOfMotif, List<int[]> solutionStartPositionList)
        {

            _currentRegulatoryMotifPerfomance.SaveStatisticData(outputPresentation, optimalValue, duration, durationMilliSeconds, dateComplete,
                       isComplete, lastRoute, optimalRoute, listOfMotif, solutionStartPositionList);
        }        
        //--------------------------------------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------------------------------------
}
