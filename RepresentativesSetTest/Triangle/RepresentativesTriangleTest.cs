using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepresentativesSet;
using RepresentativesSet.BranchAndBound;
using RepresentativesSet.Greedy;
using RepresentativesSet.TriangleEnumeration;
using RepresentativesSet.TriangleEnumeration.SelectElement;
using RepresentativesSetTest.Base;
using StatisticsStorage.Accumulators;
using StatisticsStorage.Savers;
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
        [TestMethod]
        public void ExecuteTestCase3()
        {
            // arrange
            // 6, 9, 12, 17, 24
            string setAsString = "((1,2)  (0,3)  (2,3)  (0,4)  (3,4)";
            RepresentativesTriangleStrategy representativesTriangleImroveStrategy;
            representativesTriangleImroveStrategy = new RepresentativesTriangleStrategy(5, new SelectElementImproveStrategy());
            string expectedResult1 = "0,1,3";
            string expectedResult2 = "1,3,4";

            // act
            representativesTriangleImroveStrategy.Execute(setAsString);

            // assert
            Assert.IsTrue(representativesTriangleImroveStrategy.OptimalSets.Contains(expectedResult1));
            Assert.IsTrue(representativesTriangleImroveStrategy.OptimalSets.Contains(expectedResult2));

        }
        [TestMethod]
        public void ExecuteTestCase4()
        {
            // arrange
            // 6, 9, 12, 17, 24
            List<int> list = new List<int>() { 6, 9, 12, 17, 24 };
            int[][] listOfSet = list.Select(t => BruteForceRepresentativesBinaryNumbders.GetAsElementNumbers(t, 5).ToArray()).ToArray();
            RepresentativesGreedyImprove representativesGreedy = new RepresentativesGreedyImprove();
            string expectedResult = "0,1,3";

            // act
            representativesGreedy.Execute(listOfSet);
            representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();
            string solutionAsString = string.Join(",", representativesGreedy.Solution);

            // assert
            Assert.AreEqual(expectedResult, solutionAsString);

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
        private RepresentativesStatisticAccumulator _statisticAccumulatorStrategy;
        private RepresentativesStatisticAccumulator _statisticAccumulatorImproveStrategy;
        private RepresentativesStatisticAccumulator _statisticAccumulatorImproveRDStrategy;
        private RepresentativesStatisticAccumulator _statisticAccumulatorRelationStrategy;
        private BruteForceRepresentativesAsTree bruteForceAsTree;
        private RepresentativesTriangle representativesTriangle;
        private RepresentativesTriangleStrategy representativesTriangleStrategy;
        private RepresentativesTriangleStrategy representativesTriangleImroveStrategy;
        private RepresentativesTriangleStrategy representativesTriangleImroveRDStrategy;
        private RepresentativesTriangleStrategy representativesTriangleRelationStrategy;
        //--------------------------------------------------------------------------------------
        public EnumerateIntegerTrangleForRepresentativesTriangleCompare(int pCardinality, int pLength, int pMinimumValue = 1, int pForwardAdditive = 1, decimal step = 1, int bufferSize = 2000)
            : base(pCardinality, pLength, pMinimumValue, pForwardAdditive)
        {
            _fBreakElement = 0;
            _fCardinality = pCardinality;

            representativesTriangle = new RepresentativesTriangle(_fCardinality);
            representativesTriangleStrategy = new RepresentativesTriangleStrategy(_fCardinality, new SelectElementSimpleStrategy());
            representativesTriangleImroveStrategy = new RepresentativesTriangleStrategy(_fCardinality, new SelectElementImproveStrategy());
            representativesTriangleImroveRDStrategy = new RepresentativesTriangleStrategy(_fCardinality, new SelectElementImproveRDStrategy());
            representativesTriangleRelationStrategy = new RepresentativesTriangleStrategy(_fCardinality, new SelectElementRelationStrategy());

            _statisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, step, bufferSize);
            _statisticAccumulator.Delete(representativesTriangle.AlgorithmName);
            _statisticAccumulatorStrategy = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, step, bufferSize);
            _statisticAccumulatorStrategy.Delete(representativesTriangleStrategy.AlgorithmName);
            _statisticAccumulatorImproveStrategy = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, step, bufferSize);
            _statisticAccumulatorImproveStrategy.Delete(representativesTriangleImroveStrategy.AlgorithmName);
            _statisticAccumulatorImproveRDStrategy = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, step, bufferSize);
            _statisticAccumulatorImproveRDStrategy.Delete(representativesTriangleImroveRDStrategy.AlgorithmName);
            _statisticAccumulatorRelationStrategy = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, step, bufferSize);
            _statisticAccumulatorRelationStrategy.Delete(representativesTriangleRelationStrategy.AlgorithmName);

            bruteForceAsTree = new BruteForceRepresentativesAsTree(_fCardinality);

            representativesTriangle.StatisticAccumulator = _statisticAccumulator;
            representativesTriangleStrategy.StatisticAccumulator = _statisticAccumulatorStrategy;
            representativesTriangleImroveStrategy.StatisticAccumulator = _statisticAccumulatorImproveStrategy;
            representativesTriangleImroveRDStrategy.StatisticAccumulator = _statisticAccumulatorImproveRDStrategy;
            representativesTriangleRelationStrategy.StatisticAccumulator = _statisticAccumulatorRelationStrategy;

        }
        //--------------------------------------------------------------------------------------
        protected override void ActAction(int[][] listOfSet)
        {
            bruteForceAsTree.Execute(listOfSet);
            bruteForceAsTree.OptimalSets = bruteForceAsTree.OptimalSets.OrderBy(s => s).ToList();

            representativesTriangle.Execute(listOfSet);
            representativesTriangle.SortSolutions();

            representativesTriangleStrategy.Execute(listOfSet);
            representativesTriangleStrategy.SortSolutions();

            representativesTriangleImroveStrategy.Execute(listOfSet);
            representativesTriangleImroveStrategy.SortSolutions();

            representativesTriangleImroveRDStrategy.Execute(listOfSet);
            representativesTriangleImroveRDStrategy.SortSolutions();

            representativesTriangleRelationStrategy.Execute(listOfSet);
            representativesTriangleRelationStrategy.SortSolutions();
        }
        //--------------------------------------------------------------------------------------
        protected override void AssertAction()
        {
            Assert.AreEqual(representativesTriangle.OptimalSets.Count, bruteForceAsTree.OptimalSets.Count, "Wrong number rows in result");
            Assert.AreEqual(representativesTriangle.OptimalSets.Count, representativesTriangleStrategy.OptimalSets.Count, "Wrong number rows in result");
            Assert.AreEqual(representativesTriangle.OptimalSets.Count, representativesTriangleImroveStrategy.OptimalSets.Count, "Wrong number rows in result");
            Assert.AreEqual(representativesTriangle.OptimalSets.Count, representativesTriangleImroveRDStrategy.OptimalSets.Count, "Wrong number rows in result");
            Assert.AreEqual(representativesTriangle.OptimalSets.Count, representativesTriangleRelationStrategy.OptimalSets.Count, "Wrong number rows in result");
            for (int i = 0; i < representativesTriangle.OptimalSets.Count; i++)
            {
                Assert.AreEqual(representativesTriangle.OptimalSets[i], bruteForceAsTree.OptimalSets[i], $"Wrong string in position {i} - {representativesTriangle.OptimalSets[i]}. Expected - {bruteForceAsTree.OptimalSets[i]}");
                Assert.AreEqual(representativesTriangle.OptimalSets[i], representativesTriangleStrategy.OptimalSets[i], $"Wrong string in position {i} - {representativesTriangle.OptimalSets[i]}. Expected - {representativesTriangleStrategy.OptimalSets[i]}");
                Assert.AreEqual(representativesTriangle.OptimalSets[i], representativesTriangleImroveStrategy.OptimalSets[i], $"Wrong string in position {i} - {representativesTriangle.OptimalSets[i]}. Expected - {representativesTriangleStrategy.OptimalSets[i]}");
                Assert.AreEqual(representativesTriangle.OptimalSets[i], representativesTriangleImroveRDStrategy.OptimalSets[i], $"Wrong string in position {i} - {representativesTriangle.OptimalSets[i]}. Expected - {representativesTriangleStrategy.OptimalSets[i]}");
                Assert.AreEqual(representativesTriangle.OptimalSets[i], representativesTriangleRelationStrategy.OptimalSets[i], $"Wrong string in position {i} - {representativesTriangle.OptimalSets[i]}. Expected - {representativesTriangleStrategy.OptimalSets[i]}");
            }
        }
        //--------------------------------------------------------------------------------------
        protected override void PostAction()
        {
            _statisticAccumulator.SaveRemain();
            _statisticAccumulatorStrategy.SaveRemain();
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

            bruteForceAsTree = new BruteForceRepresentativesAsTree(_fCardinality);
            representativesTriangle = new RepresentativesTriangleBranchAndBound(_fCardinality);

            _statisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, 1, 1000);
            _statisticAccumulator.Delete(representativesTriangle.AlgorithmName);

            representativesTriangle.StatisticAccumulator = _statisticAccumulator;

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
