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
    public class HeapSortTest
    {
        [TestMethod]
        public void HeapSortSorting20Test()
        {
            // arrange
            List<int> inputSequence = new List<int>() { 0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19};
            List<int> sortingSequence = inputSequence.ToList();
            var heapSort = new HeapSort<int>();
            heapSort.Sort(inputSequence);
            // act
            bool equal = inputSequence.SequenceEqual(sortingSequence);
            // assert
            Assert.IsTrue(equal);

        }        
        //--------------------------------------------------------------------------------------
        // multiple testing
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void HeapSortSortingTest()
        {
            // arrange
            int size = 10;
            EnumerateHeapSort enumeration = new EnumerateHeapSort(size);
            // act
            enumeration.Execute();
            // assert

        }
        [TestMethod]
        public void HeapSortSortingRepeatedCaseTest()
        {
            // arrange
            int size = 8;
            EnumerateHeapRepeatedSort enumeration = new EnumerateHeapRepeatedSort(size);
            // act
            enumeration.Execute();
            // assert

        }
    }
    //--------------------------------------------------------------------------------------
    // class EnumerateHeapSort 
    //--------------------------------------------------------------------------------------
    public class EnumerateHeapSort : PermitationBase
    {
        protected int _fSize;
        protected int _step;
        protected int _stepCounter;
        protected ISortingAccumulator _sortingAccumulator { get; set; }
        protected HeapSort<int> _heapSort;
        //--------------------------------------------------------------------------------------
        public EnumerateHeapSort(
            int pSize,
            int pStep = 1,
            int bufferSize = 1000)
            : base(pSize, pSize)
        {
            _fSize = pSize;
            _step = pStep;
            _stepCounter = 1;
            _sortingAccumulator = new FakeSortingAccumulator();
            _heapSort = new HeapSort<int>();
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                List<int> currentSequence = _fCurrentSet.Select(i => i).ToList();
                List<int> sortingSequence = _fCurrentSet.OrderBy(i => i).ToList();
                _heapSort.Sort(currentSequence);
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
    // class EnumerateHeapRepeatedSort 
    //--------------------------------------------------------------------------------------
    public class EnumerateHeapRepeatedSort : EnumerateIntegerFullSet
    {
        protected int _fSize;
        protected int _step;
        protected int _stepCounter;
        protected ISortingAccumulator _sortingAccumulator { get; set; }
        protected HeapSort<int> _heapSort;
        //--------------------------------------------------------------------------------------
        public EnumerateHeapRepeatedSort(
            int pSize,
            int pStep = 1,
            int bufferSize = 1000)
            : base(pSize, pSize)
        {
            _fSize = pSize;
            _step = pStep;
            _stepCounter = 1;
            _sortingAccumulator = new FakeSortingAccumulator();
            _heapSort = new HeapSort<int>();
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                List<int> currentSequence = _fCurrentSet.Select(i => i).ToList();
                List<int> sortingSequence = _fCurrentSet.OrderBy(i => i).ToList();
                _heapSort.Sort(currentSequence);
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
