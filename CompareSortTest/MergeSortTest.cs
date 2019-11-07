using BaseContract;
using CommonLibrary;
using CompareSort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatisticsStorage.Accumulators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompareSortTest
{
    [TestClass]
    public class MergeSortTest
    {
        [TestMethod]
        public void MergeSortSorting20Test()
        {
            // arrange
            List<int> inputSequence = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 8, 9, 7 };
            List<int> sortingSequence = inputSequence.OrderBy(i => i).ToList();
            var mergeSort = new MergeSort<int>();
            mergeSort.Sort(inputSequence);
            // act
            bool equal = inputSequence.SequenceEqual(sortingSequence);
            // assert
            Assert.IsTrue(equal);

        }
        [TestMethod]
        public void QuickSortSorting20Test()
        {
            // arrange
            List<int> inputSequence = new List<int>() { 0, 1, 2, 3, 4, 5, 8, 6, 7, 9 };
            List<int> sortingSequence = inputSequence.OrderBy(i => i).ToList();
            var mergeSort = new MergeSort<int>();
            mergeSort.Sort(inputSequence);
            // act
            bool equal = inputSequence.SequenceEqual(sortingSequence);
            // assert
            Assert.IsTrue(equal);

        }
        [TestMethod]
        public void QuickSortSorting21Test()
        {
            // arrange
            List<int> inputSequence = new List<int>() { 0, 1, 2, 3, 5, 4, 6, 7, 9, 8 };
            List<int> sortingSequence = inputSequence.OrderBy(i => i).ToList();
            var mergeSort = new MergeSort<int>();
            mergeSort.Sort(inputSequence);
            // act
            bool equal = inputSequence.SequenceEqual(sortingSequence);
            // assert
            Assert.IsTrue(equal);

        }
        [TestMethod]
        public void QuickSortSorting22Test()
        {
            // arrange
            List<int> inputSequence = new List<int>() { 0, 1, 2, 3, 6, 7, 4, 5, 8, 9 };
            List<int> sortingSequence = inputSequence.OrderBy(i => i).ToList();
            var mergeSort = new MergeSort<int>();
            mergeSort.Sort(inputSequence);
            // act
            bool equal = inputSequence.SequenceEqual(sortingSequence);
            // assert
            Assert.IsTrue(equal);

        }
        [TestMethod]
        public void QuickSortSorting23Test()
        {
            // arrange
            List<int> inputSequence = new List<int>() { 0, 1, 2, 3, 6, 7, 4, 5, 8, 9 };
            List<int> sortingSequence = inputSequence.OrderBy(i => i).ToList();
            var mergeSort = new MergeSort<int>();
            mergeSort.Sort(inputSequence);
            // act
            bool equal = inputSequence.SequenceEqual(sortingSequence);
            // assert
            Assert.IsTrue(equal);

        }
        //--------------------------------------------------------------------------------------
        // multiple testing
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void MergeSortSortingTest()
        {
            // arrange
            int size = 10;
            EnumerateMergeSort enumeration = new EnumerateMergeSort(size);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        // multiple testing Repeated Case
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void MergeSortSortingRepeatedCaseTest()
        {
            // arrange
            int size = 8;
            RepeatedEnumerateMergeSort enumeration = new RepeatedEnumerateMergeSort(size);
            // act
            enumeration.Execute();
            // assert

        }
    }
    //--------------------------------------------------------------------------------------
    // class EnumerateMergeSort 
    //--------------------------------------------------------------------------------------
    public class EnumerateMergeSort : PermitationBase
    {
        protected int _fSize;
        protected int _step;
        protected int _stepCounter;
        protected ISortingAccumulator _sortingAccumulator { get; set; }
        protected MergeSort<int> _mergeSort;
        //--------------------------------------------------------------------------------------
        public EnumerateMergeSort(
            int pSize,
            int pStep = 1,
            int bufferSize = 1000)
            : base(pSize, pSize)
        {
            _fSize = pSize;
            _step = pStep;
            _stepCounter = 1;
            _sortingAccumulator = new FakeSortingAccumulator();
            _mergeSort = new MergeSort<int>();
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                List<int> currentSequence = _fCurrentSet.Select(i => i).ToList();
                List<int> sortingSequence = _fCurrentSet.OrderBy(i => i).ToList();
                _mergeSort.Sort(currentSequence);
                // act
                bool equal = currentSequence.SequenceEqual(sortingSequence);
                // assert
                if (!equal)
                    throw new Exception(string.Join(",", currentSequence));
                Assert.IsTrue(equal);
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
    // class RepeatedEnumerateMergeSort 
    //--------------------------------------------------------------------------------------
    public class RepeatedEnumerateMergeSort : EnumerateIntegerFullSet
    {
        protected int _fSize;
        protected int _step;
        protected int _stepCounter;
        protected ISortingAccumulator _sortingAccumulator { get; set; }
        protected MergeSort<int> _mergeSort;
        //--------------------------------------------------------------------------------------
        public RepeatedEnumerateMergeSort(
            int pSize,
            int pStep = 1,
            int bufferSize = 1000)
            : base(pSize, pSize)
        {
            _fSize = pSize;
            _step = pStep;
            _stepCounter = 1;
            _sortingAccumulator = new FakeSortingAccumulator();
            _mergeSort = new MergeSort<int>();
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                List<int> currentSequence = _fCurrentSet.Select(i => i).ToList();
                List<int> sortingSequence = _fCurrentSet.OrderBy(i => i).ToList();
                _mergeSort.Sort(currentSequence);
                // act
                bool equal = currentSequence.SequenceEqual(sortingSequence);
                // assert
                if (!equal)
                    throw new Exception(string.Join(",", currentSequence));
                Assert.IsTrue(equal);
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
