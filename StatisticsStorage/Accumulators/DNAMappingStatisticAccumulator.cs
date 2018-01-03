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
        protected string _outputPresentation;
        protected long _duration;
        protected long _durationMilliSeconds;
        protected DateTime _dateComplete;
        protected string _isComplete;
        protected string _lastRoute;
        protected string _optimalRoute;
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
            string isComplete, string lastRoute, string optimalRoute)
        {
            _outputPresentation = outputPresentation;
            _duration = duration;
            _durationMilliSeconds = durationMilliSeconds;
            _dateComplete = dateComplete;
            _isComplete = isComplete;
            _lastRoute = lastRoute;
            _optimalRoute = optimalRoute;
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
