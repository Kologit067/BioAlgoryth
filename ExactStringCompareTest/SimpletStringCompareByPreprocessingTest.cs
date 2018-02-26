using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonLibrary;
using System.Linq;
using ExactStringCompare;
using StatisticsStorage.Accumulators;
using StatisticsStorage.Savers;
using BaseContract;

namespace ExactStringCompareTest
{
    //--------------------------------------------------------------------------------------
    // lass SimpletStringCompareByPreprocessingTest
    //--------------------------------------------------------------------------------------
    [TestClass]
    public class SimpletStringCompareByPreprocessingTest
    {
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SimpletStringCompareByPreprocessingCase1Text()
        {

            // arrange
            char[] alphabet = new char[] { 'a', 'c' };
            string pattern = "aacaac";
            string text = "aaaaaaaacaaacaac";
            SimpletStringCompareByPreprocessing simpletStringCompareByPreprocessing = new SimpletStringCompareByPreprocessing()
            {
                StatisticAccumulator = new FakeStringCompareAccumulator()
            };                    
            // act
            simpletStringCompareByPreprocessing.FindSubstring(text, pattern);
            // assert
            string expected = "10";
            Assert.AreEqual(simpletStringCompareByPreprocessing.OutputPresentation, expected, $"Wrong result:{simpletStringCompareByPreprocessing.OutputPresentation}, expected:{expected}");

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SimpletStringCompareByPreprocessingCase2Text()
        {

            // arrange
            char[] alphabet = new char[] { 'a', 'c' };
            string pattern = "aacaac";
            string text = "aaaaaaacaaacaaca";
            SimpletStringCompareByPreprocessing simpletStringCompareByPreprocessing = new SimpletStringCompareByPreprocessing()
            {
                StatisticAccumulator = new FakeStringCompareAccumulator()
            };
            // act
            simpletStringCompareByPreprocessing.FindSubstring(text, pattern);
            // assert
            string expected = "9";
            Assert.AreEqual(simpletStringCompareByPreprocessing.OutputPresentation, expected, $"Wrong result:{simpletStringCompareByPreprocessing.OutputPresentation}, expected:{expected}");

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SimpletStringCompareByPreprocessingCase3Text()
        {

            // arrange
            char[] alphabet = new char[] { 'a', 'c' };
            string pattern = "acaccc";
            string text = "aaaaaaacaaccaccc";
            SimpletStringCompareByPreprocessing simpletStringCompareByPreprocessing = new SimpletStringCompareByPreprocessing()
            {
                StatisticAccumulator = new FakeStringCompareAccumulator()
            };
            // act
            simpletStringCompareByPreprocessing.FindSubstring(text, pattern);
            // assert
            string expected = "";
            Assert.AreEqual(simpletStringCompareByPreprocessing.OutputPresentation, expected, $"Wrong result:{simpletStringCompareByPreprocessing.OutputPresentation}, expected:{expected}");

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SimpletStringCompareByPreprocessingCase4Text()
        {

            // arrange
            char[] alphabet = new char[] { 'a', 'c' };
            string pattern = "aacaac";
            string text = "aaaaacaaacaacaac";
            SimpletStringCompareByPreprocessing simpletStringCompareByPreprocessing = new SimpletStringCompareByPreprocessing()
            {
                StatisticAccumulator = new FakeStringCompareAccumulator()
            };
            // act
            simpletStringCompareByPreprocessing.FindSubstring(text, pattern);
            // assert
            string expected = "7,10";
            Assert.AreEqual(simpletStringCompareByPreprocessing.OutputPresentation, expected, $"Wrong result:{simpletStringCompareByPreprocessing.OutputPresentation}, expected:{expected}");

        }
        //--------------------------------------------------------------------------------------
        // multiple testing
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SimpletStringCompareByPreprocessingCharSet2PatterText()
        {
            // arrange
            int patternLength = 6;
            int textLength = 16;
            char[] alphabet = new char[] { 'a', 'c' };
            EnumerateCharSetForSimpleStringCompareByPreprocessing enumeration = new EnumerateCharSetForSimpleStringCompareByPreprocessing(
                alphabet, patternLength, textLength );
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SimpletStringCompareByPreprocessingCharSet3PatterText()
        {
            // arrange
            int patternLength = 4;
            int textLength = 10;
            char[] alphabet = new char[] { 'a', 'c', 'g' };
            EnumerateCharSetForSimpleStringCompareByPreprocessing enumeration = new EnumerateCharSetForSimpleStringCompareByPreprocessing(
                alphabet, patternLength, textLength);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SimpletStringCompareByPreprocessingCharSet4PatterText()
        {
            // arrange
            int patternLength = 4;
            int textLength = 7;
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            EnumerateCharSetForSimpleStringCompareByPreprocessing enumeration = new EnumerateCharSetForSimpleStringCompareByPreprocessing(
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
            int step = 987981;
            int bufferSize = 1000;
            int patternLength = 7;
            int textLength = 14;
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            StringCompareAccumulator statisticAccumulator = new StringCompareAccumulator(new StringCompareSaver(), SimpletStringCompareByPreprocessing.AlgorythmName,
                patternLength, textLength, bufferSize, alphabet.Length);
            statisticAccumulator.Delete();
            int size = patternLength + textLength;
            long max = 1L << ( 2 * size); 
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
                SimpletStringCompareByPreprocessing simpletStringCompareByPreprocessing = new SimpletStringCompareByPreprocessing()
                {
                    StatisticAccumulator = statisticAccumulator
                };
                // act
                simpletStringCompareByPreprocessing.FindSubstring(text, pattern);

            }

            // assert

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
                int pStep = 1,
                int bufferSize = 1000)
                : base(pCharSet, pTextLength + pPatternLength, 0)
            {
                _patternLength = pPatternLength;
                _textLength = pTextLength;
                _step = pStep;
                _stepCounter = 1;
                _statisticAccumulator = new StringCompareAccumulator(new StringCompareSaver(), SimpletStringCompareByPreprocessing.AlgorythmName, 
                    _patternLength, _textLength, bufferSize, pCharSet.Length);
                _statisticAccumulator.Delete();
            }
            //--------------------------------------------------------------------------------------
            protected override bool MakeAction()
            {
                if (_fCurrentPosition == _fSize - 1 && --_stepCounter == 0)
                {
                    var currentSequence = _fCurrentSet.Select(i => _charSet[i]).ToList();
                    string pattern = new string( currentSequence.Take(_patternLength).ToArray());
                    string text = new string (currentSequence.Skip(_patternLength).Take(_textLength).ToArray());
                    SimpletStringCompareByPreprocessing simpletStringCompareByPreprocessing = new SimpletStringCompareByPreprocessing()
                    {
                        StatisticAccumulator = _statisticAccumulator
                    };                    // act
                    simpletStringCompareByPreprocessing.FindSubstring(text,pattern);
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
