using System;
using System.Collections.Generic;
using System.Linq;
using CommonLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepresentativesSet;

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
            RepresentativesBranchAndBound bruteForce = new RepresentativesBranchAndBound(5, listOfSet);
            List<int> expectedResult = new List<int>() { 1, 3, 4 };
            string expectedResult2 = "0,2,4";

            // act
            bruteForce.Execute();

            // assert
            Assert.AreEqual(expectedResult.Count, bruteForce.Result.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], bruteForce.Result[i], $"Wrong string in position {i} - {bruteForce.Result[i]}. Expected - {expectedResult[i]}");
            }

            Assert.IsTrue(bruteForce.OptimalSets.Contains(expectedResult2));

        }

        [TestMethod]
        public void ExecuteTestCase2()
        {
            // arrange
            int[][] listOfSet = new int[][] { new int[] { 0, 1, 3 }, new int[] { 0, 2, 3 }, new int[] { 0, 3, 4 } }; ;
            RepresentativesBranchAndBound bruteForce = new RepresentativesBranchAndBound(5, listOfSet);
            List<int> expectedResult = new List<int>() { 3 };
            string expectedResult2 = "0";

            // act
            bruteForce.Execute();

            // assert
            Assert.AreEqual(expectedResult.Count, bruteForce.Result.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], bruteForce.Result[i], $"Wrong string in position {i} - {bruteForce.Result[i]}. Expected - {expectedResult[i]}");
            }

            Assert.IsTrue(bruteForce.OptimalSets.Contains(expectedResult2));

        }

        [TestMethod]
        public void ExecuteTestCase3()
        {
            // arrange
            int[][] listOfSet = new int[][] { new int[] { 0, 1 }, new int[] { 2, 3 }, new int[] { 0, 4 } }; ;
            RepresentativesBranchAndBound bruteForce = new RepresentativesBranchAndBound(5, listOfSet);
            List<int> expectedResult = new List<int>() { 0, 3 };
            string expectedResult2 = "0,2";

            // act
            bruteForce.Execute();

            // assert
            Assert.AreEqual(expectedResult.Count, bruteForce.Result.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], bruteForce.Result[i], $"Wrong string in position {i} - {bruteForce.Result[i]}. Expected - {expectedResult[i]}");
            }

            Assert.IsTrue(bruteForce.OptimalSets.Contains(expectedResult2));

        }

        [TestMethod]
        public void ExecuteTestCase4()
        {
            // arrange
            int[][] listOfSet = new int[][] { new int[] { 0, 1 }, new int[] { 2, 3 }, new int[] { 4 }, new int[] { 1, 3 } };
            RepresentativesBranchAndBound bruteForce = new RepresentativesBranchAndBound(5, listOfSet);
            List<int> expectedResult = new List<int>() { 1, 3, 4 };
            string expectedResult2 = "1,2,4";

            // act
            bruteForce.Execute();

            // assert
            Assert.AreEqual(expectedResult.Count, bruteForce.Result.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], bruteForce.Result[i], $"Wrong string in position {i} - {bruteForce.Result[i]}. Expected - {expectedResult[i]}");
            }

            Assert.IsTrue(bruteForce.OptimalSets.Contains(expectedResult2));

        }

        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase1()
        {
            // arrange
            int сardinality = 4;
            int length = 4;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase2()
        {
            // arrange
            int сardinality = 5;
            int length = 4;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase3()
        {
            // arrange
            int сardinality = 4;
            int length = 5;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase4()
        {
            // arrange
            int сardinality = 5;
            int length = 5;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase5()
        {
            // arrange
            int сardinality = 6;
            int length = 4;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase6()
        {
            // arrange
            int сardinality = 4;
            int length = 6;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

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
        public void BruteForceCompareTestCase8()
        {
            // arrange
            int сardinality = 6;
            int length = 5;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase9()
        {
            // arrange
            int сardinality = 5;
            int length = 6;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase10()
        {
            // arrange
            int сardinality = 5;
            int length = 7;
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
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase12()
        {
            // arrange
            int сardinality = 4;
            int length = 8;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase13()
        {
            // arrange
            int сardinality = 5;
            int length = 8;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase14()
        {
            // arrange
            int сardinality = 4;
            int length = 9;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase15()
        {
            // arrange
            int сardinality = 4;
            int length = 10;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase16()
        {
            // arrange
            int сardinality = 4;
            int length = 11;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert



        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase17()
        {
            // arrange
            int сardinality = 4;
            int length = 12;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert



        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase18()
        {
            // arrange
            int сardinality = 4;
            int length = 13;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase19()
        {
            // arrange
            int сardinality = 4;
            int length = 15;
            EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare enumeration = new EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
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
    }

    //--------------------------------------------------------------------------------------
    // class EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare
    //--------------------------------------------------------------------------------------
    public class EnumerateIntegerTrangleForBranchAndBoundRepresentativesCompare : EnumerateIntegerTrangle
    {
        private int _fCardinality;
        private List<string> _result = new List<string>();
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
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                // arrange
                int[][] listOfSet = _fCurrentSet.Select(t => BruteForceRepresentatives.GetAsElementNumbers(t, _fCardinality).ToArray()).ToArray();
                BruteForceRepresentatives bruteForce = new BruteForceRepresentatives();
                RepresentativesBranchAndBound branchAndBound = new RepresentativesBranchAndBound(_fCardinality, listOfSet);

                // act
                List<int> result = bruteForce.ExecuteByBinary(listOfSet);
                branchAndBound.Execute();
                bruteForce.OptimalSets = bruteForce.OptimalSets.OrderBy(s => s).ToList();
                branchAndBound.OptimalSets = branchAndBound.OptimalSets.OrderBy(s => s).ToList();

                // assert
                Assert.AreEqual(branchAndBound.OptimalSets.Count, bruteForce.OptimalSets.Count, "Wrong number rows in result");
                for (int i = 0; i < branchAndBound.OptimalSets.Count; i++)
                {
                    Assert.AreEqual(branchAndBound.OptimalSets[i], bruteForce.OptimalSets[i], $"Wrong string in position {i} - {branchAndBound.OptimalSets[i]}. Expected - {bruteForce.OptimalSets[i]}");
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
