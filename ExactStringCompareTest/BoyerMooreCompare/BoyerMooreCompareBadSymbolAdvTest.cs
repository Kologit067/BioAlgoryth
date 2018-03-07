using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonLibrary;
using BaseContract;
using StatisticsStorage.Accumulators;
using StatisticsStorage.Savers;
using ExactStringCompare;
using System.Linq;
using System.Collections.Generic;

namespace ExactStringCompareTest
{
    [TestClass]
    public class BoyerMooreCompareBadSymbolAdvTest
    {
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SimpletStringCompareByPreprocessingCase1Text()
        {

            // arrange
            char[] alphabet = new char[] { 'a', 'c' };
            string pattern = "aaaaaa";
            string text = "aaccaaacaaaaaaaa";
            BoyerMooreComparer boyerMooreCompare = new BoyerMooreComparer()
            {
                StatisticAccumulator = new FakeStringCompareAccumulator()
            };
            // act
            boyerMooreCompare.FindSubstringBadSymbolAdv(text, pattern);
            boyerMooreCompare.FindSubstringBadSymbol(text, pattern);
            // assert
            string expected = "8,9,10";
            Assert.AreEqual(boyerMooreCompare.OutputPresentation, expected, $"Wrong result:{boyerMooreCompare.OutputPresentation}, expected:{expected}");

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SimpletStringCompareByPreprocessingCase2Text()
        {

            // arrange
            char[] alphabet = new char[] { 'a', 'c' };
            string pattern = "aaaaaa";
            string text = "aaccaaaccaacacaa";
            BoyerMooreComparer boyerMooreCompare = new BoyerMooreComparer()
            {
                StatisticAccumulator = new FakeStringCompareAccumulator()
            };
            // act
            boyerMooreCompare.FindSubstringBadSymbolAdv(text, pattern);
            boyerMooreCompare.FindSubstringBadSymbol(text, pattern);
            // assert
            string expected = "8,9,10";
//            Assert.AreEqual(boyerMooreCompare.OutputPresentation, expected, $"Wrong result:{boyerMooreCompare.OutputPresentation}, expected:{expected}");

        }
        public class BadSymbolStatistics
        {
            public string Pattern { get; set; }
            public string Text { get; set; }
            public string OutputPresentation { get; set; }
            public long ElapsedTicks { get; set; }
            public long DurationMilliSeconds { get; set; }
#if (DEBUG)
            public List<long> ElapsedTicksList { get; set; }
            public long CoreProcess { get; set; }
            public long DictionaryProcess { get; set; }
            public long OuterLoop { get; set; }
#endif
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SimpletStringCompareByPreprocessingCaseWorstBestText()
        {

            // arrange
            char[] alphabet = new char[] { 'a', 'c' };
            string[] worstPatterns = { "aaaaac",
"aaaaac",
"aaaaac",
"aaacac",
"aaaaac",
"aaacac",
"aaacaa",
"aaacac",
"aaacac",
"aaacaa"};
            
            string[] bestPatterns = { "aaaaaa",
"aaaaaa",
"aaaaaa",
"aaaaaa",
"aaaaaa",
"aaaaaa",
"aaaaaa",
"aaaaaa",
"aaaaaa",
"aaaaaa"};

            string[] worstTexts = {"acacaaaaaccacacc","acacaaaaaccaccca",

"accccaaaacacacaa",
"aaccacacaacaacac",
"accccaaaacccacac",
"aaccacacaacaacca",
"cacccaacaaacacca",
"aaccacacaacacaac",
"aaccacacaacacaaa",
"cacccaacaaacaccc"
            };

            string[] bestTexts = { "aaaacccacacaacca",
"acaacacaacccacac",
"acaaacccccaacaca",
"acaacccccaaacaca",
"acacacaaacaaccca",
"acacaccacaaacccc",
"acccaaaaccacccca",
"aaaaccccacaaccca",
"aaaccacaacaaacca",
"aacacaacacaacaca"
            };

            List<BadSymbolStatistics> badSymbolStatistics = new List<BadSymbolStatistics>(50);
            BoyerMooreComparer boyerMooreCompare = new BoyerMooreComparer()
            {
                StatisticAccumulator = new FakeStringCompareAccumulator()
            };
            // act
            for (int i = 0; i < 10; i++)
            {
                boyerMooreCompare = new BoyerMooreComparer()
                {
                    StatisticAccumulator = new FakeStringCompareAccumulator()
                };
                boyerMooreCompare.FindSubstringBadSymbolAdv(worstTexts[i], worstPatterns[i]);
                badSymbolStatistics.Add(new BadSymbolStatistics()
                {
                    Pattern = worstPatterns[i],
                    Text = worstTexts[i],
                    OutputPresentation = boyerMooreCompare.OutputPresentation,
                    ElapsedTicks = boyerMooreCompare.ElapsedTicks,
                    DurationMilliSeconds = boyerMooreCompare.DurationMilliSeconds,
#if (DEBUG)
                    ElapsedTicksList = boyerMooreCompare.ElapsedTicksList.ToList(),
                    CoreProcess = boyerMooreCompare.CoreProcess,
                    DictionaryProcess = boyerMooreCompare.DictionaryProcess,
                    OuterLoop = boyerMooreCompare.OuterLoop
#endif
                });
                boyerMooreCompare = new BoyerMooreComparer()
                {
                    StatisticAccumulator = new FakeStringCompareAccumulator()
                };
                boyerMooreCompare.FindSubstringBadSymbolAdv(bestTexts[i], bestPatterns[i]);
                badSymbolStatistics.Add(new BadSymbolStatistics()
                {
                    Pattern = worstPatterns[i],
                    Text = worstTexts[i],
                    OutputPresentation = boyerMooreCompare.OutputPresentation,
                    ElapsedTicks = boyerMooreCompare.ElapsedTicks,
                    DurationMilliSeconds = boyerMooreCompare.DurationMilliSeconds,
#if (DEBUG)
                    ElapsedTicksList = boyerMooreCompare.ElapsedTicksList.ToList(),
                    CoreProcess = boyerMooreCompare.CoreProcess,
                    DictionaryProcess = boyerMooreCompare.DictionaryProcess,
                    OuterLoop = boyerMooreCompare.OuterLoop
#endif
                });
            }
            // assert
            string expected = "8,9,10";
            //Assert.AreEqual(boyerMooreCompare.OutputPresentation, expected, $"Wrong result:{boyerMooreCompare.OutputPresentation}, expected:{expected}");

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
            EnumerateCharSetForBoyerMooreBadSymbolAdvCompare enumeration = new EnumerateCharSetForBoyerMooreBadSymbolAdvCompare(
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
            EnumerateCharSetForBoyerMooreBadSymbolAdvCompare enumeration = new EnumerateCharSetForBoyerMooreBadSymbolAdvCompare(
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
            EnumerateCharSetForBoyerMooreBadSymbolAdvCompare enumeration = new EnumerateCharSetForBoyerMooreBadSymbolAdvCompare(
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
            int step = 9879;
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
                BoyerMooreComparer boyerMooreCompare = new BoyerMooreComparer()
                {
                    StatisticAccumulator = statisticAccumulator
                };
                // act
                boyerMooreCompare.FindSubstringBadSymbolAdv(text, pattern, false);

            }
            statisticAccumulator.SaveRemain();

            // assert

        }


    }

    //--------------------------------------------------------------------------------------
    // class EnumerateCharSetForBoyerMooreBadSymbolAdvCompare 
    //--------------------------------------------------------------------------------------
    public class EnumerateCharSetForBoyerMooreBadSymbolAdvCompare : EnumerateIntegerCharSet
    {
        protected int _patternLength;
        protected int _textLength;
        protected int _step;
        protected int _stepCounter;
        protected BoyerMooreComparer boyerMooreCompare;
        protected IStringCompareAccumulator _statisticAccumulator { get; set; }
        //--------------------------------------------------------------------------------------
        public EnumerateCharSetForBoyerMooreBadSymbolAdvCompare(
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
            _statisticAccumulator = new StringCompareAccumulator(new StringCompareSaver(), BoyerMooreComparer.AlgorythmNameBadSymbolAdv,
                _patternLength, _textLength, bufferSize, pCharSet.Length);
            _statisticAccumulator.Delete();
            boyerMooreCompare = new BoyerMooreComparer()
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
                boyerMooreCompare.FindSubstringBadSymbolAdv(text, pattern);
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

