using BaseContract;
using CommonLibrary;
using CompareSort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatisticsStorage.Accumulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareSortTest
{
    [TestClass]
    public class InsertionSortTest
    {
        [TestMethod]
        public void InsertionSortSorting20Test()
        {
            // arrange
            List<int> inputSequence = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 8, 9, 7 };
            List<int> sortingSequence = inputSequence.OrderBy(i => i).ToList();
            var insertionSort = new InsertionSort<int>();
            insertionSort.Sort(inputSequence);
            // act
            bool equal = inputSequence.SequenceEqual(sortingSequence);
            // assert
            Assert.IsTrue(equal);

        }
        //--------------------------------------------------------------------------------------
        // multiple testing
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void InsertionSortSortingTest()
        {
            // arrange
            int size = 10;
            EnumerateInsertionSort enumeration = new EnumerateInsertionSort(size);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        // multiple testing
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void InsertionSortSortingRepeatedCaseTest()
        {
            // arrange
            int size = 8;
            RepeatedEnumerateInsertionSort enumeration = new RepeatedEnumerateInsertionSort(size);
            // act
            enumeration.Execute();
            // assert

        }

    }

    //--------------------------------------------------------------------------------------
    // class EnumerateInsertionSort 
    //--------------------------------------------------------------------------------------
    public class EnumerateInsertionSort : PermitationBase
    {
        protected int _fSize;
        protected int _step;
        protected int _stepCounter;
        protected ISortingAccumulator _sortingAccumulator { get; set; }
        protected InsertionSort<int> _insertionSort;
        //--------------------------------------------------------------------------------------
        public EnumerateInsertionSort(
            int pSize,
            int pStep = 1,
            int bufferSize = 1000)
            : base(pSize, pSize)
        {
            _fSize = pSize;
            _step = pStep;
            _stepCounter = 1;
            _sortingAccumulator = new FakeSortingAccumulator();
            _insertionSort = new InsertionSort<int>();
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                List<int> currentSequence = _fCurrentSet.Select(i => i).ToList();
                List<int> sortingSequence = _fCurrentSet.OrderBy(i => i).ToList();
                _insertionSort.Sort(currentSequence);
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
    // class RepeatedEnumerateInsertionSort 
    //--------------------------------------------------------------------------------------
    public class RepeatedEnumerateInsertionSort : EnumerateIntegerFullSet
    {
        protected int _fSize;
        protected int _step;
        protected int _stepCounter;
        protected ISortingAccumulator _sortingAccumulator { get; set; }
        protected InsertionSort<int> _insertionSort;
        //--------------------------------------------------------------------------------------
        public RepeatedEnumerateInsertionSort(
            int pSize,
            int pStep = 1,
            int bufferSize = 1000)
            : base(pSize, pSize)
        {
            _fSize = pSize;
            _step = pStep;
            _stepCounter = 1;
            _sortingAccumulator = new FakeSortingAccumulator();
            _insertionSort = new InsertionSort<int>();
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                List<int> currentSequence = _fCurrentSet.Select(i => i).ToList();
                List<int> sortingSequence = _fCurrentSet.OrderBy(i => i).ToList();
                _insertionSort.Sort(currentSequence);
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
