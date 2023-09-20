using CommonLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepresentativesSet;
using StatisticsStorage.Accumulators;
using StatisticsStorage.Savers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace RepresentativesSetTest
{
    [TestClass]
    public class RepresentativesCompareWithSkipTest
    {
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void CompareTestCase1()
        {
            // arrange
            int сardinality = 4;
            int length = 4;
            long number = BruteForceRepresentatives.GetNumberLeafOfTriangleTree(length, сardinality);
            int step = (int)(number/100);
            EnumerateIntegerTrangleRepresentativesCompare enumeration = new EnumerateIntegerTrangleRepresentativesCompare(сardinality, length, step);
            // act
            enumeration.Execute();
            // assert
            File.WriteAllLines("selected.txt",enumeration.Selected);

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void CompareTestCase2()
        {
            // arrange
            int сardinality = 5;
            int length = 6;
            long number = BruteForceRepresentatives.GetNumberLeafOfTriangleTree(length, сardinality);
            int step = (int)(number / 100);
            EnumerateIntegerTrangleRepresentativesCompare enumeration = new EnumerateIntegerTrangleRepresentativesCompare(сardinality, length, step);
            // act
            enumeration.Execute();
            // assert
            File.WriteAllLines("selected.txt", enumeration.Selected);
//            File.WriteAllLines("result.txt", enumeration.Result);

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void CompareCombination()
        {
            // arrange
            int n = 43;
            int m = 11;

            // act
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            long[,] matrix = RepresentativesBranchAndBoundByValue.CreateCombinationMatrix(n, m);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            long ticks = stopWatch.ElapsedTicks;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            stopWatch.Restart();
            long[,] matrixRec = RepresentativesBranchAndBoundByValue.CreateCombinationMatrixByRec(n, m);
            stopWatch.Stop();
            TimeSpan tsRec = stopWatch.Elapsed;
            long ticksRec = stopWatch.ElapsedTicks;
            string elapsedTimeRec = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", tsRec.Hours, tsRec.Minutes, tsRec.Seconds, tsRec.Milliseconds / 10);

            // assert
            for(int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    Assert.AreEqual(matrix[i,j], matrixRec[i,j],$"i={i+1}, j={j+1} {matrix[i,j]} {matrixRec[i,j]}");
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void CompareSkipTestCase1()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            // arrange
            int limit = 43;
            int length = 11;
            long number = RepresentativesAsTree.Combination(limit, length);
            int step = (int)(number / 100000);
            //step = 678;
            EnumerateIntegerTrangleForSkipCalculation enumeration = new EnumerateIntegerTrangleForSkipCalculation(limit, length, step);
            // act
            enumeration.Execute();
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            long ticks = stopWatch.ElapsedTicks;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            // assert
            File.WriteAllLines("selected.txt", enumeration.Selected);

        }
    }

    //--------------------------------------------------------------------------------------
    // class EnumerateIntegerTrangleRepresentativesCompare
    //--------------------------------------------------------------------------------------
    public class EnumerateIntegerTrangleRepresentativesCompare : EnumerateIntegerTrangle
    {
        private int _fCardinality;
        private int _step;
        private int _counter = 0;
        private List<string> _result = new List<string>();
        private List<string> _selected = new List<string>();
        private RepresentativesStatisticAccumulator _statisticAccumulator;
        public List<string> Result
        { 
            get { return _result; } 
        }
        public List<string> Selected
        {
            get { return _selected; }
        }
        //--------------------------------------------------------------------------------------
        public EnumerateIntegerTrangleRepresentativesCompare(int pCardinality, int pLength, int step, int pMinimumValue = 1, int pForwardAdditive = 1)
            : base((1 << pCardinality) - 1, pLength, pMinimumValue, pForwardAdditive)
        {
            _fBreakElement = 0;
            _fCardinality = pCardinality;
            _step= step;
            _statisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality);
            _statisticAccumulator.Delete(nameof(RepresentativesBranchAndBoundByValue));
            _result = new List<string>();   
            _selected = new List<string>();
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                _counter++;
                string strRepresenttion = string.Join(",", _fCurrentSet);
                if (_counter == _step)
                {
                    _counter = 0;
                    _selected.Add(strRepresenttion);
                }
                _result.Add(strRepresenttion);
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
        protected override bool IsCompleteCondition()
        {
            return base.IsCompleteCondition();
        }
        //--------------------------------------------------------------------------------------
        protected override void PostAction()
        {
            _statisticAccumulator.SaveRemain();
        }
        //--------------------------------------------------------------------------------------

    }
    //--------------------------------------------------------------------------------------
    // class EnumerateIntegerTrangleForSkipCalculation
    //--------------------------------------------------------------------------------------
    public class EnumerateIntegerTrangleForSkipCalculation : EnumerateIntegerTrangle
    {
        private int _step;
        private long _counter = 0;
        private int _stepCounter = 0;
        private List<string> _result = new List<string>();
        private List<string> _selected = new List<string>();
        public List<string> Result
        {
            get { return _result; }
        }
        public List<string> Selected
        {
            get { return _selected; }
        }
        //--------------------------------------------------------------------------------------
        public EnumerateIntegerTrangleForSkipCalculation(int pLimit, int pLength, int step, int pMinimumValue = 1, int pForwardAdditive = 1)
            : base(pLimit, pLength, pMinimumValue, pForwardAdditive)
        {
            _fBreakElement = 0;
            _step = step;
            _result = new List<string>();
            _selected = new List<string>();
            RepresentativesBranchAndBoundByValue.SetCombinationMatrix(pLimit,pLength);
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                _counter++;
                _stepCounter++;
                if (_stepCounter == _step)
                {
                    _stepCounter = 0;
                    var skipList = RepresentativesBranchAndBoundByValue.SkipEnumeration(_fLimit, _fSize, _counter);
                    string strRepresenttion = string.Join(",", _fCurrentSet);
                    string strSkipList = string.Join(",", skipList);
                    _selected.Add(strRepresenttion);
                    _result.Add(strRepresenttion);
                    Assert.AreEqual(strRepresenttion, strSkipList);
                }
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
        protected override bool IsCompleteCondition()
        {
            return base.IsCompleteCondition();
        }
        //--------------------------------------------------------------------------------------
        protected override void PostAction()
        {
        }
        //--------------------------------------------------------------------------------------

    }
}
