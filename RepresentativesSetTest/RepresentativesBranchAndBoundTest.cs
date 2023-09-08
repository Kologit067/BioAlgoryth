﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepresentativesSet;
using StatisticsStorage.Accumulators;
using StatisticsStorage.Savers;

namespace RepresentativesSet.Tests
{
    [TestClass()]
    public class RepresentativesBranchAndBoundTest
    {


        [TestMethod()]
        public void CombinationTest()
        {
            // arrange
            long expectedC32 = 3;
            long expectedC42 = 6;
            long expectedC52 = 10;
            long expectedC74 = 35;

            // act
            long realC32 = RepresentativesBranchAndBound.Combination(3, 2);
            long realC42 = RepresentativesBranchAndBound.Combination(4, 2);
            long realC52 = RepresentativesBranchAndBound.Combination(5, 2);
            long realC74 = RepresentativesBranchAndBound.Combination(7, 4);

            // assert
            Assert.AreEqual(expectedC32, realC32);
            Assert.AreEqual(expectedC42, realC42);
            Assert.AreEqual(expectedC52, realC52);
            Assert.AreEqual(expectedC74, realC74);
        }

        [TestMethod()]
        public void CombinationRecTest()
        {
            // arrange
            for (int n = 1; n < 20; n++)
                for (int k = 1; k <= n; k++)
                {

                    // act
                    long direct = RepresentativesBranchAndBound.Combination(n, k);
                    long rec = RepresentativesBranchAndBound.CombinationRec(n, k);

                    // assert
                    Assert.AreEqual(direct, rec);

                }
        }

        [TestMethod()]
        public void CombinationRecTest32()
        {
            // arrange
            long expected = 3;

            // act
            long rec = RepresentativesBranchAndBound.CombinationRec(3, 2);

            // assert
            Assert.AreEqual(expected, rec);

        }
    }
}

namespace RepresentativesSetTest
{
    //--------------------------------------------------------------------------------------
    // class RepresentativesBranchAndBoundTest
    //--------------------------------------------------------------------------------------
    [TestClass]
    public class RepresentativesBranchAndBoundTest
    {
        [TestMethod]
        public void ExecuteTestCase1()
        {
            // arrange
            int[][] listOfSet = new int[][] { new int[] { 0, 1 }, new int[] { 2, 3 }, new int[] { 4 } };
            RepresentativesBranchAndBound branchAndBound = new RepresentativesBranchAndBound(5, listOfSet);
            List<int> expectedResult = new List<int>() { 0, 2, 4 };
            string expectedResult2 = "1,3,4";
            int expectedCount = 4;

            // act
            branchAndBound.Execute();

            // assert
            Assert.AreEqual(expectedResult.Count, branchAndBound.Result.Count, "Wrong number rows in result");
            Assert.AreEqual(branchAndBound.OptimalSets.Count, expectedCount, "Wrong number of optimal results");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], branchAndBound.Result[i], $"Wrong string in position {i} - {branchAndBound.Result[i]}. Expected - {expectedResult[i]}");
            }

            Assert.IsTrue(branchAndBound.OptimalSets.Contains(expectedResult2));

        }

        [TestMethod]
        public void ExecuteTestCase2()
        {
            // arrange
            int[][] listOfSet = new int[][] { new int[] { 0, 1, 3 }, new int[] { 0, 2, 3 }, new int[] { 0, 3, 4 } }; ;
            RepresentativesBranchAndBound branchAndBound = new RepresentativesBranchAndBound(5, listOfSet);
            List<int> expectedResult = new List<int>() { 0 };
            string expectedResult2 = "3";

            // act
            branchAndBound.Execute();

            // assert
            Assert.AreEqual(expectedResult.Count, branchAndBound.Result.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], branchAndBound.Result[i], $"Wrong string in position {i} - {branchAndBound.Result[i]}. Expected - {expectedResult[i]}");
            }

            Assert.IsTrue(branchAndBound.OptimalSets.Contains(expectedResult2));

        }

        [TestMethod]
        public void ExecuteTestCase3()
        {
            // arrange
            int[][] listOfSet = new int[][] { new int[] { 0, 1 }, new int[] { 2, 3 }, new int[] { 0, 4 } }; ;
            RepresentativesBranchAndBound branchAndBound = new RepresentativesBranchAndBound(5, listOfSet);
            List<int> expectedResult = new List<int>() { 0, 2 };
            string expectedResult2 = "0,3";

            // act
            branchAndBound.Execute();

            // assert
            Assert.AreEqual(expectedResult.Count, branchAndBound.Result.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], branchAndBound.Result[i], $"Wrong string in position {i} - {branchAndBound.Result[i]}. Expected - {expectedResult[i]}");
            }

            Assert.IsTrue(branchAndBound.OptimalSets.Contains(expectedResult2));

        }

        [TestMethod]
        public void ExecuteTestCase4()
        {
            // arrange
            int[][] listOfSet = new int[][] { new int[] { 0, 1 }, new int[] { 2, 3 }, new int[] { 4 }, new int[] { 1, 3 } };
            RepresentativesBranchAndBound branchAndBound = new RepresentativesBranchAndBound(5, listOfSet);
            List<int> expectedResult = new List<int>() { 0, 3, 4 };
            string expectedResult2 = "1,3,4";

            // act
            branchAndBound.Execute();

            // assert
            Assert.AreEqual(expectedResult.Count, branchAndBound.Result.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], branchAndBound.Result[i], $"Wrong string in position {i} - {branchAndBound.Result[i]}. Expected - {expectedResult[i]}");
            }

            Assert.IsTrue(branchAndBound.OptimalSets.Contains(expectedResult2));

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase7()
        {
            // arrange
            int сardinality = 7;
            int length = 4;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase11()
        {
            // arrange
            int сardinality = 6;
            int length = 6;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //-------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase20()
        {
            // arrange
            int сardinality = 5;
            int length = 9;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase21()
        {
            // arrange
            int сardinality = 5;
            int length = 10;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase22()
        {
            // arrange
            int сardinality = 5;
            int length = 11;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void TestMethod1()
        {
            int y = 10;

            var task = Method1(y);

            y++;
            y++;

            y += task.Result;
            Console.WriteLine(y);
        }

        private async Task<int> Method1(int x)
        {

            int y = 10;

            var task = Method2(x);

            y++;
            y++;

            y += await task;
            return y;


        }
        private async Task<int> Method2(int x)
        {
            int y = 5;

            var task = Method3(y);

            y++;
            y++;

            y += await task;
            return y;
        }

        private async Task<int> Method3(int x)
        {
            int y = 3;

            var task = Task.FromResult(3 + x);
            return await task;
        }
    }

    //--------------------------------------------------------------------------------------
    // class EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare
    //--------------------------------------------------------------------------------------
    public class EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare : EnumerateIntegerTrangle
    {
        private int _fCardinality;
        private List<string> _result = new List<string>();
        private RepresentativesStatisticAccumulator _statisticAccumulator;
        //--------------------------------------------------------------------------------------
        public List<string> Result
        {
            get
            {
                return _result;
            }
        }
        //--------------------------------------------------------------------------------------
        public EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(int pCardinality, int pLength, int pMinimumValue = 1, int pForwardAdditive = 1)
            : base((1 << pCardinality) - 1, pLength, pMinimumValue, pForwardAdditive)
        {
            _fBreakElement = 0;
            _fCardinality = pCardinality;
            _statisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality);
            _statisticAccumulator.Delete(nameof(RepresentativesBranchAndBoundByValue));
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                // arrange
                int[][] listOfSet = _fCurrentSet.Select(t => BruteForceRepresentatives.GetAsElementNumbers(t, _fCardinality).ToArray()).ToArray();
                BruteForceRepresentatives bruteForce = new BruteForceRepresentatives();
//                BruteForceRepresentatives bruteForceVer2 = new BruteForceRepresentatives();
                BruteForceRepresentativesAsTree bruteForceAsTree = new BruteForceRepresentativesAsTree(_fCardinality, listOfSet);
                RepresentativesBranchAndBound branchAndBound = new RepresentativesBranchAndBound(_fCardinality, listOfSet)
                {
                    StatisticAccumulator = _statisticAccumulator
                };

                // act
                List<int> result = bruteForce.ExecuteByBinary(listOfSet);
//                List<int> resultVer2 = bruteForceVer2.ExecuteByBinaryVer2(listOfSet);
                branchAndBound.Execute();
                bruteForce.OptimalSets = bruteForce.OptimalSets.OrderBy(s => s).ToList();
//                bruteForceVer2.OptimalSets = bruteForceVer2.OptimalSets.OrderBy(s => s).ToList();
                branchAndBound.OptimalSets = branchAndBound.OptimalSets.OrderBy(s => s).ToList();

                // assert
                Assert.AreEqual(branchAndBound.OptimalSets.Count, bruteForce.OptimalSets.Count, "Wrong number rows in result");
//                Assert.AreEqual(branchAndBound.OptimalSets.Count, bruteForceVer2.OptimalSets.Count, "Wrong number rows in result");
                for (int i = 0; i < branchAndBound.OptimalSets.Count; i++)
                {
                    Assert.AreEqual(branchAndBound.OptimalSets[i], bruteForce.OptimalSets[i], $"Wrong string in position {i} - {branchAndBound.OptimalSets[i]}. Expected - {bruteForce.OptimalSets[i]}");
 //                   Assert.AreEqual(branchAndBound.OptimalSets[i], bruteForceVer2.OptimalSets[i], $"Wrong string in position {i} - {branchAndBound.OptimalSets[i]}. Expected - {bruteForceVer2.OptimalSets[i]}");
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
            _statisticAccumulator.SaveRemain();
        }
        //--------------------------------------------------------------------------------------

    }
}
