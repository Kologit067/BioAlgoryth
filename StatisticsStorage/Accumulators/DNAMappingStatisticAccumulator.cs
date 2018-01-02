using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsStorage.Accumulators
{
    public class DNAMappingStatisticAccumulator
    {
        protected List<DNAMappingPerfomance> _dnaMappingPerfomances;
        protected DNAMappingPerfomance _currentDNAMappingPerfomance;
        public DNAMappingStatisticAccumulator()
        {
            _dnaMappingPerfomances = new List<DNAMappingPerfomance>();
        }
        public void CreateStatistics()
        {
            _currentDNAMappingPerfomance = new DNAMappingPerfomance();
            _dnaMappingPerfomances.Add(_currentDNAMappingPerfomance);
        }
    }
}
