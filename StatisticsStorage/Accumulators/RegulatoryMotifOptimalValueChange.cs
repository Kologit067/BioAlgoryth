using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsStorage.Accumulators
{
    public class RegulatoryMotifOptimalValueChange
    {
        protected long _numberOfIteration;
        protected long _duration;
        protected long _durationMilliSeconds;
        protected int _optimalValue;
        protected string _startPosition;
        protected string _motif;
        public RegulatoryMotifOptimalValueChange(long numberOfIteration, long duration, long durationMilliSeconds,
            int optimalValue, string startPosition, string motif)
        {
            _numberOfIteration = numberOfIteration;
            _duration = duration;
            _durationMilliSeconds = durationMilliSeconds;
            _optimalValue = optimalValue;
            _startPosition = startPosition;
            _motif = motif;
        }
    }
}
