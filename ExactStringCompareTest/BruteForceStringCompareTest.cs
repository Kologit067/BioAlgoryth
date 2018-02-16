using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonLibrary;
using System.Linq;
using ExactStringCompare;
using BaseContract;
using StatisticsStorage.Accumulators;
using StatisticsStorage.Savers;

namespace ExactStringCompareTest
{
    [TestClass]
    public class BruteForceStringCompareTest
    {
        [TestMethod]
        public void FindSubstringTest()
        {
        }
        //--------------------------------------------------------------------------------------
        // class EnumerateCharSetForBruteForceStringCompare 
        //--------------------------------------------------------------------------------------
        public class EnumerateCharSetForBruteForceStringCompare : EnumerateIntegerCharSet
        {
            protected int _patternLength;
            protected int _textLength;
            protected int _step;
            protected int _stepCounter;
            protected IStringCompareAccumulator _statisticAccumulator { get; set; }
            //--------------------------------------------------------------------------------------
            public EnumerateCharSetForBruteForceStringCompare(
                char[] pCharSet,
                int pPatternLength,
                int pTextLength,
                int pStep = 1,
                int bufferSize = 1000)
                : base(pCharSet, pTextLength + pPatternLength, 0)
            {
                _patternLength = pPatternLength;
                _textLength = pTextLength;
                _step = pStep;
                _stepCounter = 1;
                _statisticAccumulator = new StringCompareAccumulator(new StringCompareSaver(), SimpletStringCompareByPreprocessing.AlgorythmName,
                    _patternLength, _textLength, bufferSize);
                _statisticAccumulator.Delete();
            }
            //--------------------------------------------------------------------------------------
            protected override bool MakeAction()
            {
                if (_fCurrentPosition == _fSize - 1 && --_stepCounter == 0)
                {
                    var currentSequence = _fCurrentSet.Select(i => _charSet[i]).ToList();
                    string pattern = new string(currentSequence.Take(_patternLength).ToArray());
                    string text = new string(currentSequence.Skip(_patternLength).Take(_textLength).ToArray());
                    BruteForceStringCompare bruteForceStringCompare = new BruteForceStringCompare();
                    // act
                    bruteForceStringCompare.FindSubstring(text, pattern);
                    // assert

                    _stepCounter = _step;
                }

                return false;
            }
            //--------------------------------------------------------------------------------------
            protected override void PostAction()
            {
                _statisticAccumulator.SaveRemain();
            }
            //--------------------------------------------------------------------------------------
        }
        //--------------------------------------------------------------------------------------    
    }
}
