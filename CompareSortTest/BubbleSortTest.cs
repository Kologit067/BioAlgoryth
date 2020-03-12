using System;
using System.Collections.Generic;
using System.Linq;
using BaseContract;
using CommonLibrary;
using CompareSort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatisticsStorage.Accumulators;
using StatisticsStorage.Savers;

namespace CompareSortTest
{
    [TestClass]
    public class BubbleSortTest
    {
        //--------------------------------------------------------------------------------------
        // multiple testing
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BubbleSortSortingTest()
        {
            // arrange
            int size = 10;
            EnumerateBubbleSort enumeration = new EnumerateBubbleSort(size);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        // multiple testing
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BubbleSortSortingRepeatedCaseTest()
        {
            // arrange
            int size = 8;
            EnumerateBubbleRepeatedSort enumeration = new EnumerateBubbleRepeatedSort(size);
            // act            
            enumeration.Execute();
            // assert

        }
    }
    //--------------------------------------------------------------------------------------
    // class EnumerateBubbleSort 
    //--------------------------------------------------------------------------------------
    public class EnumerateBubbleSort : PermitationBase
    {
        protected int _fSize;
        protected int _step;
        protected int _stepCounter;
        protected ISortingAccumulator _sortingAccumulator { get; set; }
        protected BubbleSort<int> _bubbleSort;
        //--------------------------------------------------------------------------------------
        public EnumerateBubbleSort(
            int pSize,
            int pStep = 1,
            int bufferSize = 1000)
            : base(pSize, pSize)
        {
            _fSize = pSize;
            _step = pStep;
            _stepCounter = 1;
            _sortingAccumulator = new FakeSortingAccumulator();
            _bubbleSort = new BubbleSort<int>();
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1 )
            {
                List<int> currentSequence = _fCurrentSet.Select(i => i).ToList();
                List<int> currentSequence2 = _fCurrentSet.Select(i => i).ToList();
                List<int> sortingSequence = _fCurrentSet.OrderBy(i => i).ToList();
                _bubbleSort.Sort(currentSequence);
                _bubbleSort.SortImproved(currentSequence2);
                // act
                bool equal = currentSequence.SequenceEqual(sortingSequence);
                bool equal2 = currentSequence2.SequenceEqual(sortingSequence);
                // assert
                if (!equal)
                    throw new Exception(string.Join(",", currentSequence));
                if (!equal2)
                    throw new Exception(string.Join(",", currentSequence2));
                Assert.IsTrue(equal);
                Assert.IsTrue(equal2);
                _stepCounter = _step;
            }

            return false;
        }
        //--------------------------------------------------------------------------------------
        protected override void PostAction()
        {
            _sortingAccumulator.SaveRemain();
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
    // class EnumerateBubbleRepeatedSort 
    //--------------------------------------------------------------------------------------
    public class EnumerateBubbleRepeatedSort : EnumerateIntegerFullSet
    {
        protected int _fSize;
        protected int _step;
        protected int _stepCounter;
        protected ISortingAccumulator _sortingAccumulator { get; set; }
        protected BubbleSort<int> _bubbleSort;
        //--------------------------------------------------------------------------------------
        public EnumerateBubbleRepeatedSort(
            int pSize,
            int pStep = 1,
            int bufferSize = 1000)
            : base(pSize, pSize)
        {
            _fSize = pSize;
            _step = pStep;
            _stepCounter = 1;
            _sortingAccumulator = new FakeSortingAccumulator();
            _bubbleSort = new BubbleSort<int>();
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                List<int> currentSequence = _fCurrentSet.Select(i => i).ToList();
                List<int> currentSequence2 = _fCurrentSet.Select(i => i).ToList();
                List<int> sortingSequence = _fCurrentSet.OrderBy(i => i).ToList();
                _bubbleSort.Sort(currentSequence);
                _bubbleSort.Sort(currentSequence2);
                // act
                bool equal = currentSequence.SequenceEqual(sortingSequence);
                bool equal2 = currentSequence2.SequenceEqual(sortingSequence);
                // assert
                if (!equal)
                    throw new Exception(string.Join(",", currentSequence));
                if (!equal2)
                    throw new Exception(string.Join(",", currentSequence2));
                Assert.IsTrue(equal);
                Assert.IsTrue(equal2);
                _stepCounter = _step;
            }

            return false;
        }
        //--------------------------------------------------------------------------------------
        protected override void PostAction()
        {
            _sortingAccumulator.SaveRemain();
        }
        //--------------------------------------------------------------------------------------
    }
}

