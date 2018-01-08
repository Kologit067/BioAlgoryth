using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsStorage.Accumulators
{
    //--------------------------------------------------------------------------------------
    // class StatisyicsPerfomance
    //--------------------------------------------------------------------------------------
    public class StatisyicsPerfomance
    {

        //--------------------------------------------------------------------------------------
        protected int _size;
        public int Size
        {
            get
            {
                return _size;
            }
        }
        //--------------------------------------------------------------------------------------
        protected string _inputData;
        public string InputData
        {
            get
            {
                return _inputData;
            }
        }
        //--------------------------------------------------------------------------------------
        protected string _algorithm;
        public string Algorithm
        {
            get
            {
                return _algorithm;
            }
        }
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
        protected long _fElapsedTicks;
        public long ElapsedTicks
        {
            get
            {
                return _fElapsedTicks;
            }
        }
        //--------------------------------------------------------------------------------------
        protected long fCountTerminal;
        public long CountTerminal
        {
            get
            {
                return fCountTerminal;
            }
        }
        //--------------------------------------------------------------------------------------
        protected long fUpdateOptcount;
        public long UpdateOptcount
        {
            get
            {
                return fUpdateOptcount;
            }
        }
        //--------------------------------------------------------------------------------------
        protected long fElemenationCount;
        public long ElemenationCount
        {
            get
            {
                return fElemenationCount;
            }
        }
        //--------------------------------------------------------------------------------------
        protected string _outputPresentation;
        public string OutputPresentation
        {
            get
            {
                return _outputPresentation;
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
        protected DateTime _dateComplete;
        public DateTime DateComplete
        {
            get
            {
                return _dateComplete;
            }
        }
        //--------------------------------------------------------------------------------------
        protected bool _isComplete;
        public bool IsComplete
        {
            get
            {
                return _isComplete;
            }
        }
        //--------------------------------------------------------------------------------------
        protected string _lastRoute;
        public string LastRoute
        {
            get
            {
                return _lastRoute;
            }
        }
        //--------------------------------------------------------------------------------------
        protected string _optimalRoute;
        public string OptimalRoute
        {
            get
            {
                return _optimalRoute;
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void IterationCountInc()
        {
            _fIterationCount++;
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void TerminalCountInc()
        {
            fCountTerminal++;
        }
        //--------------------------------------------------------------------------------------
        public void UpdateOptcountInc()
        {
            fUpdateOptcount++;
        }
        //--------------------------------------------------------------------------------------
        public void ElemenationCountInc()
        {
            fElemenationCount++;
        }
        //--------------------------------------------------------------------------------------
        public StatisyicsPerfomance(int size,string inputData, string algorithm)
        {
            _size = size;
            _inputData = inputData;
            _algorithm = algorithm;
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveStatisticData(string outputPresentation, long duration, long durationMilliSeconds, DateTime dateComplete,
            bool isComplete, string lastRoute, string optimalRoute)
        {
            _outputPresentation = outputPresentation;
            _duration = duration;
            _durationMilliSeconds = durationMilliSeconds;
            _dateComplete = dateComplete;
            _isComplete = isComplete;
            _lastRoute = lastRoute;
            _optimalRoute = optimalRoute;
        }
        //--------------------------------------------------------------------------------------
    }
}
