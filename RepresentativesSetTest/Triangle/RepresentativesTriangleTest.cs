using CommonLibrary;
using CommonLibrary.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepresentativesSet;
using RepresentativesSet.BranchAndBound;
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
            BruteForceRepresentativesAsTree bruteForce = new BruteForceRepresentativesAsTree(5, setAsString);
            RepresentativesTriangle representativesTriangle = new RepresentativesTriangle(5, setAsString);
            List<int> expectedResultBruteForce = new List<int>() { 2,3 };
            List<int> expectedResultTriangle = new List<int>() { 2, 3 };
            string expectedResultDirect2 = "2,3";
            string expectedResult2 = "2,3";

            // act
            bruteForce.Execute();
            representativesTriangle.Execute();

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
            BruteForceRepresentativesAsTree bruteForce = new BruteForceRepresentativesAsTree(5, setAsString);
            RepresentativesTriangle representativesTriangle = new RepresentativesTriangle(5, setAsString);
            List<int> expectedResultTriangle = new List<int>() {0, 1, 3 };
            string expectedResultDirect2 = "1,2,3";
            string expectedResult2 = "1,2,3";

            // act
            bruteForce.Execute();
            representativesTriangle.Execute();

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

    }

    //--------------------------------------------------------------------------------------
    // class EnumerateIntegerTrangleForRepresentativesTriangleCompare
    //--------------------------------------------------------------------------------------
    public class EnumerateIntegerTrangleForRepresentativesTriangleCompare : EnumerateIntegerTrangle
    {
        private int _fCardinality;
        private List<string> _result = new List<string>();
        private RepresentativesStatisticAccumulator _statisticAccumulator;
        //--------------------------------------------------------------------------------------
        private int _wrongResultCount = 0;
        //--------------------------------------------------------------------------------------
        public int WrongResultCount
        {
            get
            {
                return _wrongResultCount;
            }
        }
        //--------------------------------------------------------------------------------------
        private int _gapCount = 0;
        //--------------------------------------------------------------------------------------
        public int GapCount
        {
            get
            {
                return _gapCount;
            }
        }
        //--------------------------------------------------------------------------------------
        private int _oneCount = 0;
        //--------------------------------------------------------------------------------------
        public int OneCount
        {
            get
            {
                return _oneCount;
            }
        }
        //--------------------------------------------------------------------------------------
        private long _count = 0;
        //--------------------------------------------------------------------------------------
        public long Count
        {
            get
            {
                return _count;
            }
        }
        //--------------------------------------------------------------------------------------
        public double WrongRelation
        {
            get
            {
                return 1.0 * _wrongResultCount / _count;
            }
        }
        //--------------------------------------------------------------------------------------
        public List<string> Result
        {
            get
            {
                return _result;
            }
        }
        //--------------------------------------------------------------------------------------
        public EnumerateIntegerTrangleForRepresentativesTriangleCompare(int pCardinality, int pLength, int pMinimumValue = 1, int pForwardAdditive = 1)
            : base((1 << pCardinality) - 1, pLength, pMinimumValue, pForwardAdditive)
        {
            _fBreakElement = 0;
            _fCardinality = pCardinality;
            _statisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality);
            _statisticAccumulator.Delete(nameof(RepresentativesTriangle));
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                // arrange
                int[][] listOfSet = _fCurrentSet.Select(t => BruteForceRepresentatives.GetAsElementNumbers(t, _fCardinality).ToArray()).ToArray();
                int count = listOfSet.SelectMany(l => l).Distinct().Count();
                int max = listOfSet.SelectMany(l => l).Max();
                string listAsString = listOfSet.AsString();
                if (max + 1 != count)
                {
                    _gapCount++;
                    return false;
                }
                bool isAnyOne = listOfSet.Any(l => l.Count() == 1);
                if (isAnyOne)
                {
                    _oneCount++;
                    return false;
                }
                _count++;
                BruteForceRepresentativesAsTree bruteForceAsTree = new BruteForceRepresentativesAsTree(_fCardinality, listOfSet);
                RepresentativesTriangle representativesTriangle = new RepresentativesTriangle(_fCardinality, listOfSet)
                {
                    StatisticAccumulator = _statisticAccumulator
                };

                // act
                bruteForceAsTree.Execute();
                bruteForceAsTree.OptimalSets = bruteForceAsTree.OptimalSets.OrderBy(s => s).ToList();
                representativesTriangle.Execute();
                representativesTriangle.SortSolutions();

                // assert
                Assert.AreEqual(representativesTriangle.OptimalSets.Count, bruteForceAsTree.OptimalSets.Count, "Wrong number rows in result");
                for (int i = 0; i < representativesTriangle.OptimalSets.Count; i++)
                {
                    Assert.AreEqual(representativesTriangle.OptimalSets[i], bruteForceAsTree.OptimalSets[i], $"Wrong string in position {i} - {representativesTriangle.OptimalSets[i]}. Expected - {bruteForceAsTree.OptimalSets[i]}");
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


    //--------------------------------------------------------------------------------------
    // class EnumerateIntegerTrangleForRepresentativesTriangleBBCompare
    //--------------------------------------------------------------------------------------
    public class EnumerateIntegerTrangleForRepresentativesTriangleBBCompare : EnumerateIntegerTrangle
    {
        private int _fCardinality;
        private List<string> _result = new List<string>();
        private RepresentativesStatisticAccumulator _statisticAccumulator;
        //--------------------------------------------------------------------------------------
        private int _wrongResultCount = 0;
        //--------------------------------------------------------------------------------------
        public int WrongResultCount
        {
            get
            {
                return _wrongResultCount;
            }
        }
        //--------------------------------------------------------------------------------------
        private int _gapCount = 0;
        //--------------------------------------------------------------------------------------
        public int GapCount
        {
            get
            {
                return _gapCount;
            }
        }
        //--------------------------------------------------------------------------------------
        private int _oneCount = 0;
        //--------------------------------------------------------------------------------------
        public int OneCount
        {
            get
            {
                return _oneCount;
            }
        }
        //--------------------------------------------------------------------------------------
        private long _count = 0;
        //--------------------------------------------------------------------------------------
        public long Count
        {
            get
            {
                return _count;
            }
        }
        //--------------------------------------------------------------------------------------
        public double WrongRelation
        {
            get
            {
                return 1.0 * _wrongResultCount / _count;
            }
        }
        //--------------------------------------------------------------------------------------
        public List<string> Result
        {
            get
            {
                return _result;
            }
        }
        //--------------------------------------------------------------------------------------
        public EnumerateIntegerTrangleForRepresentativesTriangleBBCompare(int pCardinality, int pLength, int pMinimumValue = 1, int pForwardAdditive = 1)
            : base((1 << pCardinality) - 1, pLength, pMinimumValue, pForwardAdditive)
        {
            _fBreakElement = 0;
            _fCardinality = pCardinality;
            _statisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, 1000);
            _statisticAccumulator.Delete(nameof(RepresentativesTriangleBranchAndBound));
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                // arrange
                int[][] listOfSet = _fCurrentSet.Select(t => BruteForceRepresentatives.GetAsElementNumbers(t, _fCardinality).ToArray()).ToArray();
                int count = listOfSet.SelectMany(l => l).Distinct().Count();
                int max = listOfSet.SelectMany(l => l).Max();
                string listAsString = listOfSet.AsString();
                if (max + 1 != count)
                {
                    _gapCount++;
                    return false;
                }
                bool isAnyOne = listOfSet.Any(l => l.Count() == 1);
                if (isAnyOne)
                {
                    _oneCount++;
                    return false;
                }
                _count++;
                BruteForceRepresentativesAsTree bruteForceAsTree = new BruteForceRepresentativesAsTree(_fCardinality, listOfSet);
                RepresentativesTriangleBranchAndBound representativesTriangle = new RepresentativesTriangleBranchAndBound(_fCardinality, listOfSet)
                {
                    StatisticAccumulator = _statisticAccumulator
                };

                // act
                bruteForceAsTree.Execute();
                bruteForceAsTree.OptimalSets = bruteForceAsTree.OptimalSets.OrderBy(s => s).ToList();
                representativesTriangle.Execute();
                representativesTriangle.SortSolutions();

                // assert
                
                Assert.AreEqual(representativesTriangle.OptimalSets.Count, bruteForceAsTree.OptimalSets.Count, "Wrong number rows in result");
                for (int i = 0; i < representativesTriangle.OptimalSets.Count; i++)
                {
                    Assert.AreEqual(representativesTriangle.OptimalSets[i], bruteForceAsTree.OptimalSets[i], $"Wrong string in position {i} - {representativesTriangle.OptimalSets[i]}. Expected - {bruteForceAsTree.OptimalSets[i]}");
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
