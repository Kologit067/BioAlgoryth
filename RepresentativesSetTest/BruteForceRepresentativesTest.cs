using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepresentativesSet;

namespace RepresentativesSetTest
{
    [TestClass]
    public class BruteForceRepresentativesTest
    {
        [TestMethod]
        public void ExecuteTestCase1()
        {
            // arrange
            int[][] listOfSet = new int[][] {new int[] { 0, 1 }, new int[] { 2, 3}, new int[] { 4 } };
            BruteForceRepresentatives bruteForce = new BruteForceRepresentatives();
            List<int> expectedResult = new List<int>() { 0, 2, 4};
            string expectedResult2 = "1,3,4";

            // act
            List<int> result = bruteForce.ExecuteByBinary(listOfSet);

            // assert
            Assert.AreEqual(expectedResult.Count, result.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], result[i], $"Wrong string in position {i} - {result[i]}. Expected - {expectedResult[i]}");
            }

            Assert.IsTrue(bruteForce.OptimalSets.Contains(expectedResult2));

        }

        [TestMethod]
        public void ExecuteTestCase2()
        {
            // arrange
            int[][] listOfSet = new int[][] { new int[] { 0, 1, 3 }, new int[] { 0, 2, 3 }, new int[] { 0, 3, 4 } }; ;
            BruteForceRepresentatives bruteForce = new BruteForceRepresentatives();
            List<int> expectedResult = new List<int>() { 0 };
            string expectedResult2 = "3";

            // act
            List<int> result = bruteForce.ExecuteByBinary(listOfSet);

            // assert
            Assert.AreEqual(expectedResult.Count, result.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], result[i], $"Wrong string in position {i} - {result[i]}. Expected - {expectedResult[i]}");
            }

            Assert.IsTrue(bruteForce.OptimalSets.Contains(expectedResult2));

        }

        [TestMethod]
        public void ExecuteTestCase3()
        {
            // arrange
            int[][] listOfSet = new int[][] { new int[] { 0, 1 }, new int[] { 2, 3 }, new int[] { 0, 4 } }; ;
            BruteForceRepresentatives bruteForce = new BruteForceRepresentatives();
            List<int> expectedResult = new List<int>() {0, 2 };
            string expectedResult2 = "0,3";

            // act
            List<int> result = bruteForce.ExecuteByBinary(listOfSet);

            // assert
            Assert.AreEqual(expectedResult.Count, result.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], result[i], $"Wrong string in position {i} - {result[i]}. Expected - {expectedResult[i]}");
            }

            Assert.IsTrue(bruteForce.OptimalSets.Contains(expectedResult2));

        }

        [TestMethod]
        public void ExecuteTestCase4()
        {
            // arrange
            int[][] listOfSet = new int[][] { new int[] { 0, 1 }, new int[] { 2, 3 }, new int[] { 4 }, new int[] { 1, 3 } };
            BruteForceRepresentatives bruteForce = new BruteForceRepresentatives();
            List<int> expectedResult = new List<int>() { 1, 2, 4 };
            string expectedResult2 = "1,3,4";

            // act
            List<int> result = bruteForce.ExecuteByBinary(listOfSet);

            // assert
            Assert.AreEqual(expectedResult.Count, result.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], result[i], $"Wrong string in position {i} - {result[i]}. Expected - {expectedResult[i]}");
            }

            Assert.IsTrue(bruteForce.OptimalSets.Contains(expectedResult2));

        }
    }
}
