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
    public class SelectionSortTest
    {
        [TestMethod]
        public void SelectionSortSorting20Test()
        {
            // arrange
            List<int> inputSequence = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 8, 9, 7 };
            List<int> sortingSequence = inputSequence.OrderBy(i => i).ToList();
            var selectionSort = new SelectionSort<int>();
            selectionSort.Sort(inputSequence);
            // act
            bool equal = inputSequence.SequenceEqual(sortingSequence);
            // assert
            Assert.IsTrue(equal);

        }
        //--------------------------------------------------------------------------------------
        // multiple testing
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SelectionSortSortingTest()
        {
            // arrange
            int size = 10;
            EnumerateSelectionSort enumeration = new EnumerateSelectionSort(size);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        // multiple testing
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SelectionSortSortingRepeatedCaseTest()
        {
            // arrange
            int size = 8;
            EnumerateSelectionRepeatedSort enumeration = new EnumerateSelectionRepeatedSort(size);
            // act
            enumeration.Execute();
            // assert

        }
    }
    //--------------------------------------------------------------------------------------
    // class EnumerateSelectionSort 
    //--------------------------------------------------------------------------------------
    public class EnumerateSelectionSort : PermitationBase
    {
        protected int _fSize;
        protected int _step;
        protected int _stepCounter;
        protected ISortingAccumulator _sortingAccumulator { get; set; }
        protected SelectionSort<int> _selectionSort;
        //--------------------------------------------------------------------------------------
        public EnumerateSelectionSort(
            int pSize,
            int pStep = 1,
            int bufferSize = 1000)
            : base(pSize, pSize)
        {
            _fSize = pSize;
            _step = pStep;
            _stepCounter = 1;
            _sortingAccumulator = new FakeSortingAccumulator();
            _selectionSort = new SelectionSort<int>();
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                List<int> currentSequence = _fCurrentSet.Select(i => i).ToList();
                List<int> sortingSequence = _fCurrentSet.OrderBy(i => i).ToList();
                _selectionSort.Sort(currentSequence);
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
    // class EnumerateSelectionRepeatedSort 
    //--------------------------------------------------------------------------------------
    public class EnumerateSelectionRepeatedSort : EnumerateIntegerFullSet
    {
        protected int _fSize;
        protected int _step;
        protected int _stepCounter;
        protected ISortingAccumulator _sortingAccumulator { get; set; }
        protected SelectionSort<int> _selectionSort;
        //--------------------------------------------------------------------------------------
        public EnumerateSelectionRepeatedSort(
            int pSize,
            int pStep = 1,
            int bufferSize = 1000)
            : base(pSize, pSize)
        {
            _fSize = pSize;
            _step = pStep;
            _stepCounter = 1;
            _sortingAccumulator = new FakeSortingAccumulator();
            _selectionSort = new SelectionSort<int>();
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                List<int> currentSequence = _fCurrentSet.Select(i => i).ToList();
                List<int> sortingSequence = _fCurrentSet.OrderBy(i => i).ToList();
                _selectionSort.Sort(currentSequence);
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
