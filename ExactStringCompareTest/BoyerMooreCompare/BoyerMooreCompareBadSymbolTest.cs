using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonLibrary;
using BaseContract;
using StatisticsStorage.Accumulators;
using StatisticsStorage.Savers;
using System.Linq;
using ExactStringCompare;

namespace ExactStringCompareTest
{
    [TestClass]
    public class BoyerMooreCompareBadSymbolTest
    {
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SimpletStringCompareByPreprocessingCase1Text()
        {

            // arrange
            char[] alphabet = new char[] { 'a', 'c' };
            string pattern = "ccacaa";
            string text = "aaacacaaaaccacaa";
            BoyerMooreComparer boyerMooreCompare = new BoyerMooreComparer()
            {
                StatisticAccumulator = new FakeStringCompareAccumulator()
            };
            // act
            boyerMooreCompare.FindSubstringBadSymbol(text, pattern);
            // assert
            string expected = "10";
            Assert.AreEqual(boyerMooreCompare.OutputPresentation, expected, $"Wrong result:{boyerMooreCompare.OutputPresentation}, expected:{expected}");

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SimpletStringCompareByPreprocessingCase2Text()
        {

            // arrange
            char[] alphabet = new char[] { 'a', 'c' };
            string pattern = "accccc";
            string text = "aaaaaaaaaaaccccc";
            BoyerMooreComparer boyerMooreCompare = new BoyerMooreComparer()
            {
                StatisticAccumulator = new FakeStringCompareAccumulator()
            };
            // act
            boyerMooreCompare.FindSubstringBadSymbol(text, pattern);
            // assert
            string expected = "10";
            Assert.AreEqual(boyerMooreCompare.OutputPresentation, expected, $"Wrong result:{boyerMooreCompare.OutputPresentation}, expected:{expected}");

        }
        //--------------------------------------------------------------------------------------
        // multiple testing
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BoyerMooreCompareCharSet2PatterText()
        {
            // arrange
            int patternLength = 6;
            int textLength = 16;
            char[] alphabet = new char[] { 'a', 'c' };
            EnumerateCharSetForBoyerMooreBadSymbolCompare enumeration = new EnumerateCharSetForBoyerMooreBadSymbolCompare(
                alphabet, patternLength, textLength);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BoyerMooreCompareCharSet3PatterText()
        {
            // arrange
            int patternLength = 4;
            int textLength = 10;
            char[] alphabet = new char[] { 'a', 'c', 'g' };
            EnumerateCharSetForBoyerMooreBadSymbolCompare enumeration = new EnumerateCharSetForBoyerMooreBadSymbolCompare(
                alphabet, patternLength, textLength);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BoyerMooreCompareCharSet4PatterText()
        {
            // arrange
            int patternLength = 4;
            int textLength = 7;
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            EnumerateCharSetForBoyerMooreBadSymbolCompare enumeration = new EnumerateCharSetForBoyerMooreBadSymbolCompare(
                alphabet, patternLength, textLength);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ProcessInputDataBuAdditionTestLength7Number3Pattern4Step987()
        {
            // arrange
            int step = 98798111;
            int bufferSize = 1000;
            int patternLength = 7;
            int textLength = 14;
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            StringCompareAccumulator statisticAccumulator = new StringCompareAccumulator(new StringCompareSaver(), BoyerMooreComparer.AlgorythmNameBadSymbol,
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
                BoyerMooreComparer boyerMooreCompare = new BoyerMooreComparer()
                {
                    StatisticAccumulator = statisticAccumulator
                };
                // act
                boyerMooreCompare.FindSubstringBadSymbol(text, pattern);

            }

            // assert

        }


    }

    //--------------------------------------------------------------------------------------
    // class EnumerateCharSetForBoyerMooreBadSymbolCompare 
    //--------------------------------------------------------------------------------------
    public class EnumerateCharSetForBoyerMooreBadSymbolCompare : EnumerateIntegerCharSet
    {
        protected int _patternLength;
        protected int _textLength;
        protected int _step;
        protected int _stepCounter;
        protected IStringCompareAccumulator _statisticAccumulator { get; set; }
        //--------------------------------------------------------------------------------------
        public EnumerateCharSetForBoyerMooreBadSymbolCompare(
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
            _statisticAccumulator = new StringCompareAccumulator(new StringCompareSaver(), BoyerMooreComparer.AlgorythmNameBadSymbol,
                _patternLength, _textLength, bufferSize, pCharSet.Length);
            _statisticAccumulator.Delete();
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1 && --_stepCounter == 0)
            {
                var currentSequence = _fCurrentSet.Select(i => _charSet[i]).ToList();
                string text = new string(currentSequence.Take(_textLength).ToArray());
                string pattern = new string(currentSequence.Skip(_textLength).Take(_patternLength).ToArray());
                BoyerMooreComparer boyerMooreCompare = new BoyerMooreComparer()
                {
                    StatisticAccumulator = _statisticAccumulator
                };                    // act
                boyerMooreCompare.FindSubstringBadSymbol(text, pattern);
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

