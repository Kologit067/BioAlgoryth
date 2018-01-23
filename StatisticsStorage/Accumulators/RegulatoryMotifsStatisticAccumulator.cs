using BaseContract;
using CommonLibrary.Objects;
using StatisticsStorage.Savers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsStorage.Accumulators
{
    //--------------------------------------------------------------------------------------------------------------------
    //  class RegulatoryMotifsStatisticAccumulator
    //--------------------------------------------------------------------------------------------------------------------
    public class RegulatoryMotifsStatisticAccumulator : IRegulatoryMotifsStatisticAccumulator
    {
        public static readonly int SolutionLimit = 20;
        protected List<RegulatoryMotifPerfomance> _regulatoryMotifPerfomances;
        protected RegulatoryMotifPerfomance _currentRegulatoryMotifPerfomance;
        protected RegulatoryMotifSaver _regulatoryMotifSaver;
        protected int _bufferSize;
        protected string _sequenceLengthes;
        protected int _motifLength;
        protected bool _isOptimizitaion;
        protected bool _isSumAsCriteria;
        protected bool _isAllResult;
        protected int _acceptibleDistance;
        //--------------------------------------------------------------------------------------------------------------------
        public RegulatoryMotifsStatisticAccumulator(RegulatoryMotifSaver regulatoryMotifSaver, int motifLength,
            string sequenceLengthes, bool isOptimizitaion, bool isSumAsCriteria, bool isAllResult, int acceptibleDistance,
            int bufferSize = 100)
        {
            _motifLength = motifLength;
            _sequenceLengthes = sequenceLengthes;
            _isOptimizitaion = isOptimizitaion;
            _isSumAsCriteria = isSumAsCriteria;
            _isAllResult = isAllResult;
            _acceptibleDistance = acceptibleDistance;
            _regulatoryMotifSaver = regulatoryMotifSaver;
            _bufferSize = bufferSize;
            _regulatoryMotifPerfomances = new List<RegulatoryMotifPerfomance>();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void CreateStatistics(int size, string inputData, string algorithm, int numberOfSequence,
                string sequenceLengthes, int motifLength, AlgorythmParameters algorythmParameters)
        {
            _currentRegulatoryMotifPerfomance = new RegulatoryMotifPerfomance(size, inputData, algorithm, numberOfSequence, sequenceLengthes, motifLength, algorythmParameters);
            _regulatoryMotifPerfomances.Add(_currentRegulatoryMotifPerfomance);
            if (_regulatoryMotifPerfomances.Count > _bufferSize)
            {
                _regulatoryMotifSaver.Save(_regulatoryMotifPerfomances);
                _regulatoryMotifPerfomances.Clear();
            }
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
            if (_currentRegulatoryMotifPerfomance.RegulatoryMotifOptimalValueChanges.Count < SolutionLimit)
            {
                _currentRegulatoryMotifPerfomance.RegulatoryMotifOptimalValueChanges.Add(new RegulatoryMotifOptimalValueChange
                (_currentRegulatoryMotifPerfomance.IterationCount, duration, durationMilliSeconds, optimalValue, startPosition, motif));
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveStatisticData(string outputPresentation, int optimalValue, long duration, long durationMilliSeconds, DateTime dateComplete,
            bool isComplete, string lastRoute, string optimalRoute, List<List<char>> listOfMotif, List<int[]> solutionStartPositionList)
        {

            _currentRegulatoryMotifPerfomance.SaveStatisticData(outputPresentation, optimalValue, duration, durationMilliSeconds, dateComplete,
                       isComplete, lastRoute, optimalRoute, listOfMotif, solutionStartPositionList);
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveRemain()
        {
            if (_regulatoryMotifPerfomances.Count > 0)
                _regulatoryMotifSaver.Save(_regulatoryMotifPerfomances);
            _regulatoryMotifPerfomances.Clear();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public string Delete(string algorithm)
        {
            return _regulatoryMotifSaver.Delete(algorithm, _motifLength, _sequenceLengthes,
                _isOptimizitaion, _isSumAsCriteria, _isAllResult, _acceptibleDistance);
        }
        //--------------------------------------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------------------------------------
    //  class FakeRegulatoryMotifsStatisticAccumulator
    //--------------------------------------------------------------------------------------------------------------------
    public class FakeRegulatoryMotifsStatisticAccumulator : IRegulatoryMotifsStatisticAccumulator
    {
        //--------------------------------------------------------------------------------------------------------------------
        public FakeRegulatoryMotifsStatisticAccumulator()
        {
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void CreateStatistics(int size, string inputData, string algorithm, int numberOfSequence,
                string sequenceLengthes, int motifLength, AlgorythmParameters algorythmParameters)
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
        public void AddRegulatoryMotifOptimalValueChange(long duration, long durationMilliSeconds,
            int optimalValue, string startPosition, string motif)
        {
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveStatisticData(string outputPresentation, int optimalValue, long duration, long durationMilliSeconds, DateTime dateComplete,
            bool isComplete, string lastRoute, string optimalRoute, List<List<char>> listOfMotif, List<int[]> solutionStartPositionList)
        {
        }
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
    //--------------------------------------------------------------------------------------------------------------------
}
