using BaseContract;
using CommonLibrary.Objects;
using StatisticsStorage.Accumulators.Objects;
using StatisticsStorage.Savers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsStorage.Accumulators
{
    //--------------------------------------------------------------------------------------------------------------------
    //  class StringCompareAccumulator
    //--------------------------------------------------------------------------------------------------------------------
    public class StringCompareAccumulator : IStringCompareAccumulator
    {
        public static readonly int SolutionLimit = 20;
        protected List<FindPatternPerfomance> _findPatternPerfomances;
        protected FindPatternPerfomance _currentFindPatternPerfomance;
        protected StringCompareSaver _stringCompareSaver;
        protected int _bufferSize;
        protected int _patternLength;
        protected int _textLength;
        protected string _algorythm;
        //--------------------------------------------------------------------------------------------------------------------
        public StringCompareAccumulator(StringCompareSaver stringCompareSaver, string algorythm, int patternLength, int textLength, int bufferSize )
        {
            _patternLength = patternLength;
            _textLength = textLength;
            _bufferSize = bufferSize;
            _algorythm = algorythm;
            _findPatternPerfomances = new List<FindPatternPerfomance>();
            _stringCompareSaver = stringCompareSaver;
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void CreateStatistics(string text, string pattern)
        {
            _currentFindPatternPerfomance = new FindPatternPerfomance()
            {
                Algorithm = _algorythm,
                TextSize = _textLength,
                PatternSize = _patternLength,
                Text = text,
                Pattern = pattern
            };
            _findPatternPerfomances.Add(_currentFindPatternPerfomance);
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void IterationCountInc()
        {
            _currentFindPatternPerfomance.IterationCountInc();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveStatisticData(string outputPresentation, long duration, long durationMilliSeconds, DateTime dateComplete)
        {

            _currentFindPatternPerfomance.SaveStatisticData(outputPresentation, duration, durationMilliSeconds, dateComplete);
            if (_findPatternPerfomances.Count >= _bufferSize)
            {
                _stringCompareSaver.Save(_findPatternPerfomances);
                _findPatternPerfomances.Clear();
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveRemain()
        {
            if (_findPatternPerfomances.Count > 0)
                _stringCompareSaver.Save(_findPatternPerfomances);
            _findPatternPerfomances.Clear();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public string Delete()
        {
            return _stringCompareSaver.Delete(_algorythm, _patternLength, _textLength);
        }
        //--------------------------------------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------------------------------------
    //  class FakeStringCompareAccumulator
    //--------------------------------------------------------------------------------------------------------------------
    public class FakeStringCompareAccumulator : IStringCompareAccumulator
    {
        //--------------------------------------------------------------------------------------------------------------------
        public FakeStringCompareAccumulator()
        {
        }
        public void IterationCountInc()
        { }
        public void CreateStatistics(string text, string pattern) { }
        public void SaveStatisticData(string outputPresentation, long duration, long durationMilliSeconds, DateTime dateComplete) { }
        public void SaveRemain() { }
        public string Delete()
        {
            return string.Empty;
        }
        //--------------------------------------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------------------------------------
}
