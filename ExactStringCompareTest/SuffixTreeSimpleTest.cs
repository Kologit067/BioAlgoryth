using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonLibrary;
using BaseContract;
using StatisticsStorage.Accumulators;
using ExactStringCompare;
using StatisticsStorage.Savers;
using System.Linq;

namespace ExactStringCompareTest
{
    [TestClass]
    public class SuffixTreeSimpleTest
    {
        [TestMethod]
        public void SuffixTreeSimpleVariant1()
        {
            // arrange
            string text = "banana"; 
            string expectedTree = "[0-0]([1-1]([6-6](),[2-3]([6-6](),[4-6]())),[0-6](),[2-3]([6-6](),[4-6]()))";
            SuffixTreeSimple suffixTree = new SuffixTreeSimple()
            {
                StatisticAccumulator = new FakeSuffixTreeAccumulator()
            };
            // act
            suffixTree.Execute(text);
            // assert
            var result = suffixTree.NodePresentationAsString();
            Assert.AreEqual(result, expectedTree, "Wrong tree.");

        }

        [TestMethod]
        public void SuffixTreeSimpleVariant2()
        {
            // arrange
            string text = "aaaaa"; 
            string expectedTree = "[0-0]([0-0]([5-5](),[1-1]([5-5](),[2-2]([5-5](),[3-3]([5-5](),[4-5]())))))";
            SuffixTreeSimple suffixTree = new SuffixTreeSimple()
            {
                StatisticAccumulator = new FakeSuffixTreeAccumulator()
            };
            // act
            suffixTree.Execute(text);
            // assert
            var result = suffixTree.NodePresentationAsString();
            Assert.AreEqual(result, expectedTree, "Wrong tree.");

        }

        [TestMethod]
        public void SuffixTreeSimpleVariant3()
        {
            // arrange
            string text = "aaaca";
            string expectedTree = "[0-0]([0-0]([5-5](),[1-1]([2-5](),[3-5]()),[3-5]()),[3-5]())";
            SuffixTreeSimple suffixTree = new SuffixTreeSimple()
            {
                StatisticAccumulator = new FakeSuffixTreeAccumulator()
            };
            // act
            suffixTree.Execute(text);
            // assert
            var result = suffixTree.NodePresentationAsString();
            Assert.AreEqual(result, expectedTree, "Wrong tree.");

        }
        //--------------------------------------------------------------------------------------
        // multiple testing
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SuffixTreeSimpleCharSet2Text20()
        {
            // arrange
            int textLength = 20;
            char[] alphabet = new char[] { 'a', 'c' };
            EnumerateCharSetForSuffixTreeSimple enumeration = new EnumerateCharSetForSuffixTreeSimple(
                alphabet, textLength);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SuffixTreeSimpleCharSet3Text14()
        {
            // arrange
            int textLength = 14;
            char[] alphabet = new char[] { 'a', 'c', 'g' };
            EnumerateCharSetForSuffixTreeSimple enumeration = new EnumerateCharSetForSuffixTreeSimple(
                alphabet, textLength);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SuffixTreeSimpleCharSet4Text11()
        {
            // arrange
            int textLength = 11;
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            EnumerateCharSetForSuffixTreeSimple enumeration = new EnumerateCharSetForSuffixTreeSimple(
                alphabet, textLength);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SuffixTreeSimpleCharSet4Text21WithStep()
        {
            // arrange
            int step = 9879;
            int bufferSize = 1000;
            int textLength = 21;
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            SuffixTreeAccumulator statisticAccumulator = new SuffixTreeAccumulator(new SuffixTreeSaver(), BruteForceStringCompare.AlgorythmName,
                textLength, bufferSize, alphabet.Length);
            statisticAccumulator.Delete();
            int size = textLength;
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
                string text = new string(charSequence);
                SuffixTreeSimple suffixTreeSimple = new SuffixTreeSimple()
                {
                    StatisticAccumulator = statisticAccumulator
                };
                // act
                suffixTreeSimple.Execute(text);

            }
            statisticAccumulator.SaveRemain();

            // assert

        }
        //--------------------------------------------------------------------------------------
        // class EnumerateCharSetForSuffixTreeSimple 
        //--------------------------------------------------------------------------------------
        public class EnumerateCharSetForSuffixTreeSimple : EnumerateIntegerCharSet
        {
            protected int _textLength;
            protected int _step;
            protected int _stepCounter;
            protected ISuffixTreeAccumulator _statisticAccumulator { get; set; }
            //--------------------------------------------------------------------------------------
            public EnumerateCharSetForSuffixTreeSimple(
                char[] pCharSet,
                int pTextLength,
                int pStep = 1,
                int bufferSize = 1000)
                : base(pCharSet, pTextLength, 0)
            {
                _textLength = pTextLength;
                _step = pStep;
                _stepCounter = 1;
                _statisticAccumulator = new SuffixTreeAccumulator(new SuffixTreeSaver(), BruteForceStringCompare.AlgorythmName,
                    _textLength, bufferSize, pCharSet.Length);
                _statisticAccumulator.Delete();
            }
            //--------------------------------------------------------------------------------------
            protected override bool MakeAction()
            {
                if (_fCurrentPosition == _fSize - 1 && --_stepCounter == 0)
                {
                    string text = new string(_fCurrentSet.Select(i => _charSet[i]).ToArray());
                    SuffixTreeSimple suffixTreeSimple = new SuffixTreeSimple()
                    {
                        StatisticAccumulator = _statisticAccumulator
                    };
                    // act
                    suffixTreeSimple.Execute(text);
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
