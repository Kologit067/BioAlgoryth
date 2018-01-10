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
    // class DNAMappingStatisticAccumulator
    //--------------------------------------------------------------------------------------------------------------------
    public class DNAMappingStatisticAccumulator : IDNAMappingStatisticAccumulator
    {
        protected List<DNAMappingPerfomance> _dnaMappingPerfomances;
        protected DNAMappingPerfomance _currentDNAMappingPerfomance;
        protected DNAMappingSaver _dnaMappingSaver;
        protected int _bufferSize;
        //--------------------------------------------------------------------------------------------------------------------
        public DNAMappingStatisticAccumulator(DNAMappingSaver dnaMappingSaver, int bufferSize = 100)
        {
            _dnaMappingSaver = dnaMappingSaver;
            _bufferSize = bufferSize;
            _dnaMappingPerfomances = new List<DNAMappingPerfomance>();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void CreateStatistics(int size,string inputData,string algorithm, AlgorythmParameters algorythmParameters)
        {
            _currentDNAMappingPerfomance = new DNAMappingPerfomance(size, inputData, algorithm, algorythmParameters);
            _dnaMappingPerfomances.Add(_currentDNAMappingPerfomance);
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveStatisticData(string outputPresentation, long duration, long durationMilliSeconds, DateTime dateComplete,
            bool isComplete, string lastRoute, string optimalRoute, List<List<int>> listOfSolution)
        {
            _currentDNAMappingPerfomance.SaveStatisticData(outputPresentation, duration, durationMilliSeconds, dateComplete,
            isComplete, lastRoute, optimalRoute, listOfSolution);
            if (_dnaMappingPerfomances.Count > _bufferSize)
            {
                _dnaMappingSaver.Save(_dnaMappingPerfomances);
                _dnaMappingPerfomances.Clear();
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void IterationCountInc()
        {
            _currentDNAMappingPerfomance.IterationCountInc();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void TerminalCountInc()
        {
            _currentDNAMappingPerfomance.TerminalCountInc();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void UpdateOptcountInc()
        {
            _currentDNAMappingPerfomance.UpdateOptcountInc();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void ElemenationCountInc()
        {
            _currentDNAMappingPerfomance.ElemenationCountInc();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveRemain()
        {
            if (_dnaMappingPerfomances.Count > 0)
                _dnaMappingSaver.Save(_dnaMappingPerfomances);
            _dnaMappingPerfomances.Clear();
        }
        //--------------------------------------------------------------------------------------------------------------------
    }
}
