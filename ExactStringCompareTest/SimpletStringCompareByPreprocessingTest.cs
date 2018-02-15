using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonLibrary;
using System.Linq;
using ExactStringCompare;

namespace ExactStringCompareTest
{
    [TestClass]
    public class SimpletStringCompareByPreprocessingTest
    {
        [TestMethod]
        public void FindSubstringTest()
        {
        }
        //--------------------------------------------------------------------------------------
        // class EnumerateCharSetForSimpleStringCompareByPreprocessing 
        //--------------------------------------------------------------------------------------
        public class EnumerateCharSetForSimpleStringCompareByPreprocessing : EnumerateIntegerCharSet
        {
            protected int _patternLength;
            protected int _textLength;
            protected int _step;
            protected int _stepCounter;
            protected IStringCompareAccumulator _statisticAccumulator { get; set; }
            //--------------------------------------------------------------------------------------
            public EnumerateCharSetForSimpleStringCompareByPreprocessing(
                char[] pCharSet,
                int pPatternLength,
                int pTextLength,
                int pStep = 1)
                : base(pCharSet, pTextLength + pPatternLength, 0)
            {
                _patternLength = pPatternLength;
                _textLength = pTextLength;
                _step = pStep;
                _stepCounter = 1;
                _statisticAccumulator = new StringCompareAccumulator(new StringCompareSaver(), _patternLength, _textLength,
                    bufferSize: 1000);
                _statisticAccumulator.Delete("RegulatoryMotifsPatternEnumeration");
            }
            //--------------------------------------------------------------------------------------
            protected override bool MakeAction()
            {
                if (_fCurrentPosition == _fSize - 1 && --_stepCounter == 0)
                {
                    var currentSequence = _fCurrentSet.Select(i => _charSet[i]).ToList();
                    string pattern = new string( currentSequence.Take(_patternLength).ToArray());
                    string text = new string (currentSequence.Skip(_patternLength).Take(_textLength).ToArray());
                    SimpletStringCompareByPreprocessing simpletStringCompareByPreprocessing = new SimpletStringCompareByPreprocessing();
                    // act
                    simpletStringCompareByPreprocessing.FindSubstring(text,pattern);
                    // assert

                    _stepCounter = _step;
                }

                return false;
            }
            //--------------------------------------------------------------------------------------
            protected override void PostAction()
            {
//                _statisticAccumulator.SaveRemain();
            }
            //--------------------------------------------------------------------------------------
        }
        //--------------------------------------------------------------------------------------    
    }
 }
