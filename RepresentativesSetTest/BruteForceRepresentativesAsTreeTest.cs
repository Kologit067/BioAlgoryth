using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepresentativesSet;

namespace RepresentativesSetTest
{
    [TestClass]
    public class BruteForceRepresentativesAsTreeTest
    {
        [TestMethod]
        public void ExecuteTestCase1()
        {
            // arrange
            int[][] listOfSet = new int[][] { new int[] { 0, 1 }, new int[] { 2, 3 }, new int[] { 4 } };
            BruteForceRepresentativesAsTree bruteForce = new BruteForceRepresentativesAsTree(5,listOfSet);
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
            BruteForceRepresentativesAsTree bruteForce = new BruteForceRepresentativesAsTree(5, listOfSet);
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
            BruteForceRepresentativesAsTree bruteForce = new BruteForceRepresentativesAsTree(5, listOfSet);
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
            BruteForceRepresentativesAsTree bruteForce = new BruteForceRepresentativesAsTree(5, listOfSet);
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
    }
}

