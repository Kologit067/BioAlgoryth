using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepresentativesSet.Greedy;
using RepresentativesSet;
using StatisticsStorage.Accumulators;
using StatisticsStorage.Savers;
using System;
using System.Linq;
using CommonLibrary.Helpers;
using RepresentativesSetTest.Base;
using System.Numerics;

namespace RepresentativesSetTest.Greedy
{
    [TestClass]
    public class RepresentativesGreedyAdvStepTests
    {
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void CombinationAlgorithmCompareTest()
        {
            // arrange
            for (int n = 2; n < 256; n++)
                for (int m = 1; m < n && m < 10; m++)
                {
                    // act
                    long combBigNumber = Combinatorics.CombinationByBigNumber(n, m);
                    long combRecrusive = Combinatorics.CombinationRec(n, m);
                    // assert
                    Assert.AreEqual(combBigNumber, combRecrusive);
                }

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void CombinationAlgorithmCompare_22_19_Test()
        {
            // arrange
            int n = 22;
            int m = 19;

            // act
            long combBigNumber = Combinatorics.CombinationByBigNumber(n, m);
            long combRecrusive = Combinatorics.CombinationRec(n, m);
            // assert
            Assert.AreEqual(combBigNumber, combRecrusive);
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BranchAndBoundCompareGreedyImp78Test()
        {
            // arrange
            int сardinality = 8;
            int length = 7;
            EnumerateRepresentativesGreedyAdvStepGreedyImpCompare enumeration =
                new EnumerateRepresentativesGreedyAdvStepGreedyImpCompare(сardinality, length, 100000, 500, true);
            // act
            enumeration.Execute();
            // assert
            int n = 1 << сardinality;
            long comb = Combinatorics.Combination(n, length);
        }
        [TestMethod]
        public void GetFirstPositionTest_511_9_216880080648403998()
        {
            // arrange
            int n = 511;
            int m = 9;
            long number = 216880080648403998;
            Combinatorics.SetCombinationMatrix(511, 9);
            // act
            Combinatorics.GetFirstPosition(n,m,number);
            // assert
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BranchAndBoundCompareGreedyImp88Test()
        {
            // arrange
            int сardinality = 9;
            int length = 9;
            EnumerateRepresentativesGreedyAdvStepGreedyImpCompare enumeration =
                new EnumerateRepresentativesGreedyAdvStepGreedyImpCompare(сardinality, length, 50000, 2000, true);
            // act
            enumeration.Execute();
            // assert
            int n = 1 << сardinality;
            long comb = Combinatorics.CombinationByBigNumber(n, length);
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BranchAndBoundCompareGreedyImpBigIntTest()
        {
            // arrange
            int сardinality = 10;
            int length = 10;
            EnumerateRepresentativesGreedyBigIntStepGreedyImpCompare enumeration =
                new EnumerateRepresentativesGreedyBigIntStepGreedyImpCompare(сardinality, length, 100000, 2000, true);
            // act
            enumeration.Execute();
            // assert
            int n = 1 << сardinality;
            BigInteger comb = Combinatorics.BigIntegerCombination(n, length);
        }

    }
    //--------------------------------------------------------------------------------------
    // class EnumerateRepresentativesGreedyAdvStepGreedyImpCompare
    //--------------------------------------------------------------------------------------
    public class EnumerateRepresentativesGreedyAdvStepGreedyImpCompare : EnumerateRepresentativesAdvStepTestBase
    {
        private RepresentativesStatisticAccumulator _statisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyStatisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyImpStatisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyImpRDStatisticAccumulator;

        private RepresentativesGreedy representativesGreedy;
        private RepresentativesGreedy representativesGreedyImp;
        private RepresentativesGreedy representativesGreedyImpRD;
        private RepresentativesBranchAndBoundByValue branchAndBound;

        protected long _maxCount;
        protected long number;
        //--------------------------------------------------------------------------------------
        protected long _step;
        public long Step
        {
            get
            {
                return _step;
            }
        }
        //--------------------------------------------------------------------------------------
        public EnumerateRepresentativesGreedyAdvStepGreedyImpCompare(int pCardinality, int pLength, long maxCount, int bufferSize, bool isSave, int pMinimumValue = 1, int pForwardAdditive = 1)
        {
            _fLimit = (1 << pCardinality) - 1;
            _fSize = pLength;
            _fCardinality = pCardinality;
            _maxCount = maxCount;
            number = Combinatorics.CombinationByBigNumber(_fLimit, _fSize);
            _step = (long)(number / _maxCount);

            representativesGreedy = new RepresentativesGreedySimple();
            representativesGreedyImp = new RepresentativesGreedyImprove();
            representativesGreedyImpRD = new RepresentativesGreedyImproveRD();
            branchAndBound = new RepresentativesBranchAndBoundByValue(_fCardinality);


            if (isSave)
            {
                _statisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, _step, bufferSize);
                _statisticAccumulator.DeleteAlgorithm(nameof(RepresentativesBranchAndBoundByValue), pLength, pCardinality, _step);
                _greedyStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, _step, bufferSize);
                _greedyStatisticAccumulator.DeleteAlgorithm(nameof(RepresentativesGreedySimple), pLength, pCardinality, _step);
                _greedyImpStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, _step, bufferSize);
                _greedyImpStatisticAccumulator.DeleteAlgorithm(nameof(RepresentativesGreedyImprove), pLength, pCardinality, _step);
                _greedyImpRDStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, _step, bufferSize);
                _greedyImpRDStatisticAccumulator.DeleteAlgorithm(nameof(RepresentativesGreedyImproveRD), pLength, pCardinality, _step);

                representativesGreedy.StatisticAccumulator = _greedyStatisticAccumulator;
                representativesGreedyImp.StatisticAccumulator = _greedyImpStatisticAccumulator;
                representativesGreedyImpRD.StatisticAccumulator = _greedyImpRDStatisticAccumulator;
                branchAndBound.StatisticAccumulator = _statisticAccumulator;
            }
            Combinatorics.SetCombinationMatrix(_fLimit, _fSize);
        }
        //--------------------------------------------------------------------------------------
        public bool Execute()
        {
            for (long counter = _step; counter < number; counter += _step)
            {
                _fCurrentSet = Combinatorics.SkipEnumeration(_fLimit, _fSize, counter);
                int[][] listOfSet = GetAndTestListOfSet();
                if (listOfSet != null)
                {
                    // act
                    ActAction(listOfSet);
                    // assert
                    AssertAction();
                }
            }
            PostAction();
            return false;
        }
        //--------------------------------------------------------------------------------------
        protected void ActAction(int[][] listOfSet)
        {
            branchAndBound.Execute(listOfSet);
            representativesGreedy.Execute(listOfSet);
            representativesGreedyImp.Execute(listOfSet);
            representativesGreedyImpRD.Execute(listOfSet);
            branchAndBound.OptimalSets = branchAndBound.OptimalSets.OrderBy(s => s).ToList();
            representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();
            representativesGreedyImp.Solution = representativesGreedyImp.Solution.OrderBy(s => s).ToList();
            representativesGreedyImpRD.Solution = representativesGreedyImpRD.Solution.OrderBy(s => s).ToList();

        }
        //--------------------------------------------------------------------------------------
        protected void AssertAction()
        {
            if (branchAndBound.CurrentMinimum == representativesGreedy.Solution.Count)
            {
                String solutionAsString = representativesGreedy.SolutionAsString;
                Assert.IsTrue(branchAndBound.OptimalSets.Any(o => o == solutionAsString));
            }
            else
            {
                _wrongResultCount++;
            }
            if (branchAndBound.CurrentMinimum == representativesGreedyImp.Solution.Count)
            {
                String solutionAsString = representativesGreedyImp.SolutionAsString;
                Assert.IsTrue(branchAndBound.OptimalSets.Any(o => o == solutionAsString));
            }
            else
            {
                _wrongResultImpCount++;
            }
            if (branchAndBound.CurrentMinimum == representativesGreedyImpRD.Solution.Count)
            {
                String solutionAsString = representativesGreedyImpRD.SolutionAsString;
                Assert.IsTrue(branchAndBound.OptimalSets.Any(o => o == solutionAsString));
            }
            else
            {
                _wrongResultImpRDCount++;
            }

        }
        //--------------------------------------------------------------------------------------
        protected void PostAction()
        {
            _statisticAccumulator.SaveRemain();
            _greedyStatisticAccumulator.SaveRemain();
            _greedyImpStatisticAccumulator.SaveRemain();
            _greedyImpRDStatisticAccumulator.SaveRemain();
        }
        //--------------------------------------------------------------------------------------
        public virtual string ShowString
        {
            get
            {
                if (_fCurrentSet != null && _fCurrentSet.Length > 0)
                    return string.Join(",", _fCurrentSet.Select(i => i));
                return "Empty";
            }
        }        
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
    // class EnumerateRepresentativesGreedyBigIntStepGreedyImpCompare
    //--------------------------------------------------------------------------------------
    public class EnumerateRepresentativesGreedyBigIntStepGreedyImpCompare : EnumerateRepresentativesAdvStepTestBase
    {
        private RepresentativesStatisticAccumulator _statisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyStatisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyImpStatisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyImpRDStatisticAccumulator;

        private RepresentativesGreedy representativesGreedy;
        private RepresentativesGreedy representativesGreedyImp;
        private RepresentativesGreedy representativesGreedyImpRD;
        private RepresentativesBranchAndBoundByValue branchAndBound;

        protected BigInteger _maxCount;
        protected BigInteger number;
        //--------------------------------------------------------------------------------------
        protected BigInteger _step;
        public BigInteger Step
        {
            get
            {
                return _step;
            }
        }
        //--------------------------------------------------------------------------------------
        public EnumerateRepresentativesGreedyBigIntStepGreedyImpCompare(int pCardinality, int pLength, long maxCount, int bufferSize, bool isSave, int pMinimumValue = 1, int pForwardAdditive = 1)
        {
            _fLimit = (1 << pCardinality) - 1;
            _fSize = pLength;
            _fCardinality = pCardinality;
            _maxCount = maxCount;
            number = Combinatorics.BigIntegerCombination(_fLimit, _fSize);
            _step = BigInteger.Divide(number, _maxCount);

            representativesGreedy = new RepresentativesGreedySimple();
            representativesGreedyImp = new RepresentativesGreedyImprove();
            representativesGreedyImpRD = new RepresentativesGreedyImproveRD();
            branchAndBound = new RepresentativesBranchAndBoundByValue(_fCardinality);


            if (isSave)
            {
                _statisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, (decimal)_step, bufferSize);
                _statisticAccumulator.DeleteAlgorithm(nameof(RepresentativesBranchAndBoundByValue), pLength, pCardinality, (decimal)_step);
                _greedyStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, (decimal)_step, bufferSize);
                _greedyStatisticAccumulator.DeleteAlgorithm(nameof(RepresentativesGreedySimple), pLength, pCardinality, (decimal)_step);
                _greedyImpStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, (decimal)_step, bufferSize);
                _greedyImpStatisticAccumulator.DeleteAlgorithm(nameof(RepresentativesGreedyImprove), pLength, pCardinality, (decimal)_step);
                _greedyImpRDStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, (decimal)_step, bufferSize);
                _greedyImpRDStatisticAccumulator.DeleteAlgorithm(nameof(RepresentativesGreedyImproveRD), pLength, pCardinality, (decimal)_step);

                representativesGreedy.StatisticAccumulator = _greedyStatisticAccumulator;
                representativesGreedyImp.StatisticAccumulator = _greedyImpStatisticAccumulator;
                representativesGreedyImpRD.StatisticAccumulator = _greedyImpRDStatisticAccumulator;
                branchAndBound.StatisticAccumulator = _statisticAccumulator;
            }
            Combinatorics.SetCombinationBigIntegerMatrix(_fLimit, _fSize);
        }
        //--------------------------------------------------------------------------------------
        public bool Execute()
        {
            for (BigInteger counter = _step; counter < number; counter += _step)
            {
                _fCurrentSet = Combinatorics.SkipEnumerationBigInteger(_fLimit, _fSize, counter);
                int[][] listOfSet = GetAndTestListOfSet();
                if (listOfSet != null)
                {
                    // act
                    ActAction(listOfSet);
                    // assert
                    AssertAction();
                }
            }
            PostAction();
            return false;
        }
        //--------------------------------------------------------------------------------------
        protected void ActAction(int[][] listOfSet)
        {
            branchAndBound.Execute(listOfSet);
            representativesGreedy.Execute(listOfSet);
            representativesGreedyImp.Execute(listOfSet);
            representativesGreedyImpRD.Execute(listOfSet);
            branchAndBound.OptimalSets = branchAndBound.OptimalSets.OrderBy(s => s).ToList();
            representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();
            representativesGreedyImp.Solution = representativesGreedyImp.Solution.OrderBy(s => s).ToList();
            representativesGreedyImpRD.Solution = representativesGreedyImpRD.Solution.OrderBy(s => s).ToList();

        }
        //--------------------------------------------------------------------------------------
        protected void AssertAction()
        {
            if (branchAndBound.CurrentMinimum == representativesGreedy.Solution.Count)
            {
                String solutionAsString = representativesGreedy.SolutionAsString;
                Assert.IsTrue(branchAndBound.OptimalSets.Any(o => o == solutionAsString));
            }
            else
            {
                _wrongResultCount++;
            }
            if (branchAndBound.CurrentMinimum == representativesGreedyImp.Solution.Count)
            {
                String solutionAsString = representativesGreedyImp.SolutionAsString;
                Assert.IsTrue(branchAndBound.OptimalSets.Any(o => o == solutionAsString));
            }
            else
            {
                _wrongResultImpCount++;
            }
            if (branchAndBound.CurrentMinimum == representativesGreedyImpRD.Solution.Count)
            {
                String solutionAsString = representativesGreedyImpRD.SolutionAsString;
                Assert.IsTrue(branchAndBound.OptimalSets.Any(o => o == solutionAsString));
            }
            else
            {
                _wrongResultImpRDCount++;
            }

        }
        //--------------------------------------------------------------------------------------
        protected void PostAction()
        {
            _statisticAccumulator.SaveRemain();
            _greedyStatisticAccumulator.SaveRemain();
            _greedyImpStatisticAccumulator.SaveRemain();
            _greedyImpRDStatisticAccumulator.SaveRemain();
        }
        //--------------------------------------------------------------------------------------
        public virtual string ShowString
        {
            get
            {
                if (_fCurrentSet != null && _fCurrentSet.Length > 0)
                    return string.Join(",", _fCurrentSet.Select(i => i));
                return "Empty";
            }
        }
        //--------------------------------------------------------------------------------------
    }

}
