using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using CommonLibrary.Objects;

namespace StatisticsStorage.Accumulators
{
    //--------------------------------------------------------------------------------------
    // class RegulatoryMotifPerfomance
    //--------------------------------------------------------------------------------------
    public class RegulatoryMotifPerfomance : StatisyicsPerfomance
    {
        //--------------------------------------------------------------------------------------
        protected int _averageSequenceLength;
        public int AverageSequenceLength
        {
            get
            {
                return _averageSequenceLength;
            }
        }
        //--------------------------------------------------------------------------------------
        protected int _motifLength;
        public int MotifLength
        {
            get
            {
                return _motifLength;
            }
        }
        //--------------------------------------------------------------------------------------
        protected int _numberOdSequence;
        public int NumberOdSequence
        {
            get
            {
                return _numberOdSequence;
            }
        }
        //--------------------------------------------------------------------------------------
        protected int _optimalValue;
        public int OptimalValue
        {
            get
            {
                return _optimalValue;
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        protected List<RegulatoryMotifOptimalValueChange> _regulatoryMotifOptimalValueChanges = new List<RegulatoryMotifOptimalValueChange>();
        public List<RegulatoryMotifOptimalValueChange> RegulatoryMotifOptimalValueChanges
        {
            get
            {
                return _regulatoryMotifOptimalValueChanges;
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        protected List<List<char>> _listOfMotif;
        public List<List<char>> ListOfMotif
        {
            get
            {
                return _listOfMotif;
            }
        }
        //--------------------------------------------------------------------------------------
        protected List<int[]> _solutionStartPositionList;
        public List<int[]> SolutionStartPositionList
        {
            get
            {
                return _solutionStartPositionList;
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        protected AlgorythmParameters _algorythmParameters;
        public AlgorythmParameters AlgorythmParameters
        {
            get
            {
                return _algorythmParameters;
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        public RegulatoryMotifPerfomance(int size, string inputData, string algorithm, int numberOdSequence,
                int averageSequenceLength, int motifLength, AlgorythmParameters algorythmParameters) : base(size, inputData, algorithm)
        {
            _numberOdSequence = numberOdSequence;
            _averageSequenceLength = averageSequenceLength;
            _motifLength = motifLength;
            _algorythmParameters = algorythmParameters;
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveStatisticData(string outputPresentation,int optimalValue, long duration, long durationMilliSeconds, DateTime dateComplete,
            bool isComplete, string lastRoute, string optimalRoute, List<List<char>> listOfMotif, List<int[]> solutionStartPositionList)
        {

            SaveStatisticData(outputPresentation, duration, durationMilliSeconds, dateComplete,
                       isComplete, lastRoute, optimalRoute);
            _optimalValue = optimalValue;
            _listOfMotif = listOfMotif;
            _solutionStartPositionList = solutionStartPositionList;
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------------
}