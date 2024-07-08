using CommonLibrary;
using CommonLibrary.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepresentativesSet;
using RepresentativesSet.BranchAndBound;
using RepresentativesSetTest.Base;
using StatisticsStorage.Accumulators;
using StatisticsStorage.Savers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepresentativesSetTest.Triangle
{
    [TestClass]
    public class RepresentativesTriangleTest
    {
        [TestMethod]
        public void ExecuteTestCase1()
        {
            // arrange
            string setAsString = "(0,2)  (1,3)  (2,4)  (1,2,4)  (3,4)  (0,3,4)";
            BruteForceRepresentativesAsTree bruteForce = new BruteForceRepresentativesAsTree(5);
            RepresentativesTriangle representativesTriangle = new RepresentativesTriangle(5);
            List<int> expectedResultBruteForce = new List<int>() { 2,3 };
            List<int> expectedResultTriangle = new List<int>() { 2, 3 };
            string expectedResultDirect2 = "2,3";
            string expectedResult2 = "2,3";

            // act
            bruteForce.Execute(setAsString);
            representativesTriangle.Execute(setAsString);

            // assert
            Assert.AreEqual(expectedResultTriangle.Count, bruteForce.Result.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResultTriangle.Count; i++)
            {
                Assert.AreEqual(expectedResultTriangle[i], bruteForce.Result[i], $"Wrong string in position {i} - {bruteForce.Result[i]}. Expected - {expectedResultTriangle[i]}");
            }
            Assert.AreEqual(expectedResultBruteForce.Count, representativesTriangle.Result.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResultBruteForce.Count; i++)
            {
                Assert.AreEqual(expectedResultBruteForce[i], representativesTriangle.Result[i], $"Wrong string in position {i} - {representativesTriangle.Result[i]}. Expected - {expectedResultBruteForce[i]}");
            }

            Assert.IsTrue(bruteForce.OptimalSets.Contains(expectedResult2));
            Assert.IsTrue(representativesTriangle.OptimalSets.Contains(expectedResultDirect2));

        }
        [TestMethod]
        public void ExecuteTestCase2()
        {
            // arrange
            // 3,5,6,7,24
            string setAsString = "(0,1)  (0,2)  (1,2)  (0,1,2)  (3,4)";
            BruteForceRepresentativesAsTree bruteForce = new BruteForceRepresentativesAsTree(5);
            RepresentativesTriangle representativesTriangle = new RepresentativesTriangle(5);
            List<int> expectedResultTriangle = new List<int>() {0, 1, 3 };
            string expectedResultDirect2 = "1,2,3";
            string expectedResult2 = "1,2,3";

            // act
            bruteForce.Execute(setAsString);
            representativesTriangle.Execute(setAsString);

            // assert
            Assert.AreEqual(expectedResultTriangle.Count, representativesTriangle.Result.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResultTriangle.Count; i++)
            {
                Assert.AreEqual(expectedResultTriangle[i], representativesTriangle.Result[i], $"Wrong string in position {i} - {representativesTriangle.Result[i]}. Expected - {expectedResultTriangle[i]}");
            }

            Assert.IsTrue(representativesTriangle.OptimalSets.Contains(expectedResult2));
            Assert.IsTrue(representativesTriangle.OptimalSets.Contains(expectedResultDirect2));

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void RepresentativesTriangleCompareTestCase5_5()
        {
            // arrange
            int сardinality = 5;
            int length = 5;
            EnumerateIntegerTrangleForRepresentativesTriangleCompare enumeration = new EnumerateIntegerTrangleForRepresentativesTriangleCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void RepresentativesTriangleBBCompareTestCase5_5()
        {
            // arrange
            int сardinality = 5;
            int length = 5;
            EnumerateIntegerTrangleForRepresentativesTriangleBBCompare enumeration = new EnumerateIntegerTrangleForRepresentativesTriangleBBCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void RepresentativesTriangleBBCompareTestCase5_8()
        {
            // arrange
            int сardinality = 5;
            int length = 8;
            EnumerateIntegerTrangleForRepresentativesTriangleBBCompare enumeration = new EnumerateIntegerTrangleForRepresentativesTriangleBBCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void RepresentativesTriangleBBCompareTestCase6_6()
        {
            // arrange
            int сardinality = 6;
            int length = 6;
            EnumerateIntegerTrangleForRepresentativesTriangleBBCompare enumeration = new EnumerateIntegerTrangleForRepresentativesTriangleBBCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------

    }

    //--------------------------------------------------------------------------------------
    // class EnumerateIntegerTrangleForRepresentativesTriangleCompare
    //--------------------------------------------------------------------------------------
    public class EnumerateIntegerTrangleForRepresentativesTriangleCompare : EnumerateRepresentativesTestBase
    {
        private RepresentativesStatisticAccumulator _statisticAccumulator;
        private BruteForceRepresentativesAsTree bruteForceAsTree;
        private RepresentativesTriangle representativesTriangle;
        //--------------------------------------------------------------------------------------
        public EnumerateIntegerTrangleForRepresentativesTriangleCompare(int pCardinality, int pLength, int pMinimumValue = 1, int pForwardAdditive = 1)
            : base(pCardinality, pLength, pMinimumValue, pForwardAdditive)
        {
            _fBreakElement = 0;
            _fCardinality = pCardinality;
            _statisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality);
            _statisticAccumulator.Delete(nameof(RepresentativesTriangle));

            bruteForceAsTree = new BruteForceRepresentativesAsTree(_fCardinality);
            representativesTriangle = new RepresentativesTriangle(_fCardinality)
            {
                StatisticAccumulator = _statisticAccumulator
            };

        }
        //--------------------------------------------------------------------------------------
        protected override void ActAction(int[][] listOfSet)
        {
            bruteForceAsTree.OptimalSets = bruteForceAsTree.OptimalSets.OrderBy(s => s).ToList();
            bruteForceAsTree.Execute(listOfSet);
            representativesTriangle.Execute(listOfSet);
            representativesTriangle.SortSolutions();
        }
        //--------------------------------------------------------------------------------------
        protected override void AssertAction()
        {
            Assert.AreEqual(representativesTriangle.OptimalSets.Count, bruteForceAsTree.OptimalSets.Count, "Wrong number rows in result");
            for (int i = 0; i < representativesTriangle.OptimalSets.Count; i++)
            {
                Assert.AreEqual(representativesTriangle.OptimalSets[i], bruteForceAsTree.OptimalSets[i], $"Wrong string in position {i} - {representativesTriangle.OptimalSets[i]}. Expected - {bruteForceAsTree.OptimalSets[i]}");
            }
        }
        //--------------------------------------------------------------------------------------
        protected override void PostAction()
        {
            _statisticAccumulator.SaveRemain();
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
    // class EnumerateIntegerTrangleForRepresentativesTriangleBBCompare
    //--------------------------------------------------------------------------------------
    public class EnumerateIntegerTrangleForRepresentativesTriangleBBCompare : EnumerateRepresentativesTestBase
    {
        private List<string> _result = new List<string>();
        private RepresentativesStatisticAccumulator _statisticAccumulator;
        private BruteForceRepresentativesAsTree bruteForceAsTree;
        private RepresentativesTriangleBranchAndBound representativesTriangle;
        //--------------------------------------------------------------------------------------
        public EnumerateIntegerTrangleForRepresentativesTriangleBBCompare(int pCardinality, int pLength, int pMinimumValue = 1, int pForwardAdditive = 1)
            : base(pCardinality, pLength, pMinimumValue, pForwardAdditive)
        {
            _fBreakElement = 0;
            _fCardinality = pCardinality;
            _statisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, 1, 1000);
            _statisticAccumulator.Delete(nameof(RepresentativesTriangleBranchAndBound));

            bruteForceAsTree = new BruteForceRepresentativesAsTree(_fCardinality);
            representativesTriangle = new RepresentativesTriangleBranchAndBound(_fCardinality)
            {
                StatisticAccumulator = _statisticAccumulator
            };

        }
        //--------------------------------------------------------------------------------------
        protected override void ActAction(int[][] listOfSet)
        {
            bruteForceAsTree.Execute(listOfSet);
            bruteForceAsTree.OptimalSets = bruteForceAsTree.OptimalSets.OrderBy(s => s).ToList();
            representativesTriangle.Execute(listOfSet);
            representativesTriangle.SortSolutions();
        }
        //--------------------------------------------------------------------------------------
        protected override void AssertAction()
        {
            Assert.AreEqual(representativesTriangle.OptimalSets.Count, bruteForceAsTree.OptimalSets.Count, "Wrong number rows in result");
            for (int i = 0; i < representativesTriangle.OptimalSets.Count; i++)
            {
                Assert.AreEqual(representativesTriangle.OptimalSets[i], bruteForceAsTree.OptimalSets[i], $"Wrong string in position {i} - {representativesTriangle.OptimalSets[i]}. Expected - {bruteForceAsTree.OptimalSets[i]}");
            }
        }
        //--------------------------------------------------------------------------------------
        protected override void PostAction()
        {
            _statisticAccumulator.SaveRemain();
        }
        //--------------------------------------------------------------------------------------

    }

}
