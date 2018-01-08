using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsStorage.Accumulators
{
    public class RegulatoryMotifOptimalValueChange
    {
        //--------------------------------------------------------------------------------------
        protected long _fIterationCount;
        public long IterationCount
        {
            get
            {
                return _fIterationCount;
            }
        }
        //--------------------------------------------------------------------------------------
        protected long _duration;
        public long Duration
        {
            get
            {
                return _duration;
            }
        }
        //--------------------------------------------------------------------------------------
        protected long _durationMilliSeconds;
        public long DurationMilliSeconds
        {
            get
            {
                return _durationMilliSeconds;
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
        //--------------------------------------------------------------------------------------
        protected string _startPosition;
        public string StartPosition
        {
            get
            {
                return _startPosition;
            }
        }
        //--------------------------------------------------------------------------------------
        protected string _motif;
        public string Motif
        {
            get
            {
                return _motif;
            }
        }
        //--------------------------------------------------------------------------------------
        public RegulatoryMotifOptimalValueChange(long numberOfIteration, long duration, long durationMilliSeconds,
            int optimalValue, string startPosition, string motif)
        {
            _fIterationCount = numberOfIteration;
            _duration = duration;
            _durationMilliSeconds = durationMilliSeconds;
            _optimalValue = optimalValue;
            _startPosition = startPosition;
            _motif = motif;
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
