using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonLibrary;
using ExactStringCompare;
using BaseContract;
using StatisticsStorage.Accumulators;
using StatisticsStorage.Savers;
using System.Linq;

namespace ExactStringCompareTest
{
    //--------------------------------------------------------------------------------------
    // class AndShiftTest
    //--------------------------------------------------------------------------------------
    [TestClass]
    public class AndShiftTest
    {
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void AndShiftCase1Text()
        {

            // arrange
            char[] alphabet = new char[] { 'a', 'c' };
            string pattern = "aaaaaa";
            string text = "aaccaaacaaaaaaaa";
            AndShift andShift = new AndShift()
            {
                StatisticAccumulator = new FakeStringCompareAccumulator()
            };
            // act
            andShift.FindSubstring(text, pattern);
            // assert
            string expected = "8,9,10";
            Assert.AreEqual(andShift.OutputPresentation, expected, $"Wrong result:{andShift.OutputPresentation}, expected:{expected}");

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void AndShiftCase2Text()
        {

            // arrange
            char[] alphabet = new char[] { 'a', 'c' };
            string pattern = "aaaaaa";
            string text = "aaccaaaccaacacaa";
            AndShift andShift = new AndShift()
            {
                StatisticAccumulator = new FakeStringCompareAccumulator()
            };
            // act
            andShift.FindSubstring(text, pattern);
            // assert
            string expected = "8,9,10";
            //            Assert.AreEqual(boyerMooreCompare.OutputPresentation, expected, $"Wrong result:{boyerMooreCompare.OutputPresentation}, expected:{expected}");

        }
        //--------------------------------------------------------------------------------------
        // multiple testing
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void AndShiftCharSet2PatterText()
        {
            // arrange
            int patternLength = 6;
            int textLength = 16;
            char[] alphabet = new char[] { 'a', 'c' };
            EnumerateCharSetForAndShift enumeration = new EnumerateCharSetForAndShift(
                alphabet, patternLength, textLength);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void AndShiftPatterText()
        {
            // arrange
            int patternLength = 4;
            int textLength = 10;
            char[] alphabet = new char[] { 'a', 'c', 'g' };
            EnumerateCharSetForAndShift enumeration = new EnumerateCharSetForAndShift(
                alphabet, patternLength, textLength);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void AndShiftCharSet4PatterText()
        {
            // arrange
            int patternLength = 4;
            int textLength = 7;
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            EnumerateCharSetForAndShift enumeration = new EnumerateCharSetForAndShift(
                alphabet, patternLength, textLength);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ProcessInputDataAndShiftTestLength7Number3Pattern4Step987()
        {
            // arrange
            int step = 98798;
            int bufferSize = 1000;
            int patternLength = 7;
            int textLength = 14;
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            StringCompareAccumulator statisticAccumulator = new StringCompareAccumulator(new StringCompareSaver(), BoyerMooreComparer.AlgorythmNameBadSymbolAdv,
                patternLength, textLength, bufferSize, alphabet.Length);
            statisticAccumulator.Delete();
            int size = patternLength + textLength;
            long max = 1L << (2 * size);
            long sequenceAsNumber = 0;
            int[] sequence = new int[size];
            char[] charSequence = new char[size];
            long[] masks = new long[size];

            long mask = 3;
            for (int i = 0; i < size; i++)
            {
                masks[i] = mask;
                mask <<= 2;
            }
            // act
            while (sequenceAsNumber < max)
            {
                int shift = 0;
                for (int i = 0; i < size; i++)
                {
                    sequence[i] = (int)((sequenceAsNumber & masks[i]) >> shift);
                    shift += 2;
                }
                sequenceAsNumber += step;
                charSequence = sequence.Select(j => alphabet[j]).ToArray();
                string pattern = new string(charSequence.Take(patternLength).ToArray());
                string text = new string(charSequence.Skip(patternLength).Take(textLength).ToArray());
                AndShift andShift = new AndShift()
                {
                    StatisticAccumulator = statisticAccumulator
                };
                // act
                andShift.FindSubstring(text, pattern);

            }
            statisticAccumulator.SaveRemain();

            // assert

        }


    }

    //--------------------------------------------------------------------------------------
    // class EnumerateCharSetForAndShift 
    //--------------------------------------------------------------------------------------
    public class EnumerateCharSetForAndShift : EnumerateIntegerCharSet
    {
        protected int _patternLength;
        protected int _textLength;
        protected int _step;
        protected int _stepCounter;
        protected AndShift andShift;
        protected IStringCompareAccumulator _statisticAccumulator { get; set; }
        //--------------------------------------------------------------------------------------
        public EnumerateCharSetForAndShift(
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
            _statisticAccumulator = new StringCompareAccumulator(new StringCompareSaver(), AndShift.AlgorythmName,
                _patternLength, _textLength, bufferSize, pCharSet.Length);
            _statisticAccumulator.Delete();
            andShift = new AndShift()
            {
                StatisticAccumulator = _statisticAccumulator
            };
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1 && --_stepCounter == 0)
            {
                var currentSequence = _fCurrentSet.Select(i => _charSet[i]).ToList();
                string pattern = new string(currentSequence.Take(_patternLength).ToArray());
                string text = new string(currentSequence.Skip(_patternLength).Take(_textLength).ToArray());
                // act
                andShift.FindSubstring(text, pattern);
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

