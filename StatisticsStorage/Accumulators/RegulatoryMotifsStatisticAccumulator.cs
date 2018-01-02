using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsStorage.Accumulators
{
    public class RegulatoryMotifsStatisticAccumulator
    {
        protected List<RegulatoryMotifPerfomance> _regulatoryMotifPerfomances;
        protected RegulatoryMotifPerfomance _currentRegulatoryMotifPerfomance;
        public RegulatoryMotifsStatisticAccumulator()
        {
            _regulatoryMotifPerfomances = new List<RegulatoryMotifPerfomance>();
        }
        public void CreateStatistics()
        {
            _currentRegulatoryMotifPerfomance = new RegulatoryMotifPerfomance();
            _regulatoryMotifPerfomances.Add(_currentRegulatoryMotifPerfomance);
        }
    }
}
