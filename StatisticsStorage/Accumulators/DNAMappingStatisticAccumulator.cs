using BaseContract;
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
        //--------------------------------------------------------------------------------------------------------------------
        public DNAMappingStatisticAccumulator()
        {
            _dnaMappingPerfomances = new List<DNAMappingPerfomance>();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void CreateStatistics(int size,string inputData,string algorithm)
        {
            _currentDNAMappingPerfomance = new DNAMappingPerfomance(size, inputData, algorithm);
            _dnaMappingPerfomances.Add(_currentDNAMappingPerfomance);
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveStatisticData(string outputPresentation, long duration, long durationMilliSeconds, DateTime dateComplete,
            bool isComplete, string lastRoute, string optimalRoute, List<List<int>> listOfSolution)
        {
            _currentDNAMappingPerfomance.SaveStatisticData(outputPresentation, duration, durationMilliSeconds, dateComplete,
            isComplete, lastRoute, optimalRoute, listOfSolution);
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
    }
}
