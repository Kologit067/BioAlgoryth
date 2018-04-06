using BaseContract;
using StatisticsStorage.Accumulators.Objects;
using StatisticsStorage.Savers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsStorage.Accumulators
{
    public class SuffixTreeAccumulator : ISuffixTreeAccumulator
    {
        public static readonly int SolutionLimit = 20;
        protected List<SuffixTreePerfomance> _suffixTreePerfomances;
        protected SuffixTreePerfomance _currentSuffixTreePerfomance;
        protected SuffixTreeSaver _suffixTreeSaver;
        protected int _bufferSize;
        protected int _textLength;
        protected string _algorythm;
        protected int _alphabetSize;
        //--------------------------------------------------------------------------------------------------------------------
        public SuffixTreeAccumulator(SuffixTreeSaver suffixTreeSaver, string algorythm, int textLength, int bufferSize, int alphabetSize)
        {
            _textLength = textLength;
            _bufferSize = bufferSize;
            _alphabetSize = alphabetSize;
            _algorythm = algorythm;
            _suffixTreePerfomances = new List<SuffixTreePerfomance>();
            _suffixTreeSaver = suffixTreeSaver;
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void CreateStatistics(string text)
        {
            _currentSuffixTreePerfomance = new SuffixTreePerfomance()
            {
                Algorithm = _algorythm,
                TextSize = _textLength,
                AlphabetSize = _alphabetSize,
                Text = text,
            };
            _suffixTreePerfomances.Add(_currentSuffixTreePerfomance);
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void IterationCountInc(int count = 1)
        {
            _currentSuffixTreePerfomance.IterationCountInc(count);
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void NumberOfComparisonInc(int count = 1)
        {
            _currentSuffixTreePerfomance.NumberOfComparisonInc(count);
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveStatisticData(string outputPresentation, long duration, long durationMilliSeconds, DateTime dateComplete, string additionalInfo)
        {

            _currentSuffixTreePerfomance.SaveStatisticData(outputPresentation, duration, durationMilliSeconds, dateComplete, additionalInfo);
            if (_suffixTreePerfomances.Count >= _bufferSize)
            {
                _suffixTreeSaver.Save(_suffixTreePerfomances);
                _suffixTreePerfomances.Clear();
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void RemoveStatisticData()
        {
            _suffixTreePerfomances.Remove(_currentSuffixTreePerfomance);
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveRemain()
        {
            if (_suffixTreePerfomances.Count > 0)
                _suffixTreeSaver.Save(_suffixTreePerfomances);
            _suffixTreePerfomances.Clear();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public string Delete()
        {
            return _suffixTreeSaver.Delete(_algorythm, _textLength, _alphabetSize);
        }
        //--------------------------------------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------------------------------------
    //  class FakeStringCompareAccumulator
    //--------------------------------------------------------------------------------------------------------------------
    public class FakeSuffixTreeAccumulator : ISuffixTreeAccumulator
    {
        //--------------------------------------------------------------------------------------------------------------------
        public FakeSuffixTreeAccumulator()
        {
        }
        public void IterationCountInc(int count = 1)
        { }
        public void NumberOfComparisonInc(int count = 1)
        { }
        public void CreateStatistics(string text) { }
        public void SaveStatisticData(string outputPresentation, long duration, long durationMilliSeconds, DateTime dateComplete, string additionalInfo) { }
        public void SaveRemain() { }
        public string Delete()
        {
            return string.Empty;
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void RemoveStatisticData()
        {
        }
        //--------------------------------------------------------------------------------------------------------------------
    }
}
