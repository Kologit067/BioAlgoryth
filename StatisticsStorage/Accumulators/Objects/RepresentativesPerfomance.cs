using CommonLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsStorage.Accumulators.Objects
{
    public class RepresentativesPerfomance
    {
        //--------------------------------------------------------------------------------------
        protected int _numberOfSet;
        public int NumberOfSet
        {
            get
            {
                return _numberOfSet;
            }
        }
        //--------------------------------------------------------------------------------------
        protected int _dimension;
        public int Dimension
        {
            get
            {
                return _dimension;
            }
        }
        //--------------------------------------------------------------------------------------
        protected string _inputLen;
        public string InputLen 
        {
            get
            {
                return _inputLen;
            }
        }
        //--------------------------------------------------------------------------------------
        protected string _inputLenSort;
        public string InputLenSort 
        {
            get
            {
                return _inputLenSort;
            }
        }
        //--------------------------------------------------------------------------------------
        protected double _inputLenAvg;
        public double InputLenAvg
        {
            get
            {
                return _inputLenAvg;
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
        protected string _inputDataShort;
        public string InputDataShort
        {
            get
            {
                return _inputDataShort;
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
        protected long _iterationCount;
        public long IterationCount
        {
            get
            {
                return _iterationCount;
            }
        }

        //--------------------------------------------------------------------------------------
        protected long _elapsedTicks;
        public long ElapsedTicks
        {
            get
            {
                return _elapsedTicks;
            }
        }
        //--------------------------------------------------------------------------------------
        protected long _countTerminal;
        public long CountTerminal
        {
            get
            {
                return _countTerminal;
            }
        }
        //--------------------------------------------------------------------------------------
        protected long _updateOptcount;
        public long UpdateOptcount
        {
            get
            {
                return _updateOptcount;
            }
        }
        //--------------------------------------------------------------------------------------
        protected long _elemenationCount;
        public long ElemenationCount
        {
            get
            {
                return _elemenationCount;
            }
        }
        //--------------------------------------------------------------------------------------
        //protected string _outputPresentation;
        //public string OutputPresentation
        //{
        //    get
        //    {
        //        return _outputPresentation;
        //    }
        //}
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
        public string OptimalRoute
        {
            get
            {
                return (Newtonsoft.Json.JsonConvert.SerializeObject(OptimalSets)); ;
            }
        }        
        //--------------------------------------------------------------------------------------
        protected int _bestValue;
        public int BestValue
        {
            get
            {
                return _bestValue;
            }
        }      
        //--------------------------------------------------------------------------------------
        protected List<string> _optimalSets;
        public List<string> OptimalSets
        {
            get
            {
                return _optimalSets;
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void IterationCountInc()
        {
            _iterationCount++;
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void TerminalCountInc()
        {
            _countTerminal++;
        }
        //--------------------------------------------------------------------------------------
        public void UpdateOptcountInc()
        {
            _updateOptcount++;
        }
        //--------------------------------------------------------------------------------------
        public void ElemenationCountInc()
        {
            _elemenationCount++;
        }
        //--------------------------------------------------------------------------------------
        public RepresentativesPerfomance(int numberOfSet, int dimension, int[][] listOfSet, string inputDataShort, string algorithm)
        {
            string inputData = listOfSet.AsString();
            List<double> lengthArray = listOfSet.Select(l => (double)l.Length).ToList();
            _numberOfSet = numberOfSet;
            _dimension= dimension;
            _inputData = inputData;
            _inputDataShort = inputDataShort;
            _algorithm = algorithm;
            _inputLenAvg = lengthArray.Average();
            _inputLen = string.Join(",", lengthArray);
            _inputLenSort = string.Join(",", lengthArray.OrderBy(l => l));
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveStatisticData(long duration, long durationMilliSeconds, DateTime dateComplete,
            bool isComplete, string lastRoute, List<string> optimalSets, int bestValue)
        {
            _duration = duration;
            _durationMilliSeconds = durationMilliSeconds;
            _dateComplete = dateComplete;
            _isComplete = isComplete;
            _lastRoute = lastRoute;
            _optimalSets = optimalSets;
            _bestValue = bestValue;
        }
        //--------------------------------------------------------------------------------------

    }
}
