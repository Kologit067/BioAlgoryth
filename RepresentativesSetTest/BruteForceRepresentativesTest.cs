using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepresentativesSet;

namespace RepresentativesSetTest
{
    [TestClass]
    public class BruteForceRepresentativesTest
    {
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void DefineSumOfBitTest()
        {
            // arrange
            int[] expected = { 0, 1, 1, 2, 1, 2, 2, 3, 1, 2, 2, 3, 2, 3, 3, 4, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5, 1};

            for (int i = 0; i < expected.Length; i++)
            {
                // act 
                int result = BruteForceRepresentatives.DefineSumOfBit(i, 32, 6);
                // assert
                Assert.AreEqual(expected[i], result, $"Unexpected DefineSumOfBit result for {i} - {result}, Expected - {expected[i]}");
            }
        }

        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void DefineSumOfBitCompareTest()
        {
            // arrange

            for (long i = 0; i < (long)1 << 12; i++)
            {
                // act 
                long result1 = BruteForceRepresentatives.DefineSumOfBit((int)i, 1 << 12, 64);
                long result2 = BruteForceRepresentatives.DefineSumOfBitVer2(i);
                // assert
                Assert.AreEqual(result1, result2, $"Unexpected  result for {i} - DefineSumOfBit - {result1}, Expected - {result2}");
            }
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void DefineSumOfBitVer2Test()
        {
            // arrange
            long[] expected = { 0, 1, 1, 2, 1, 2, 2, 3, 1, 2, 2, 3, 2, 3, 3, 4, 1 };

            for (long i = 0; i < (long)expected.Length; i++)
            {
                // act 
                long result = BruteForceRepresentatives.DefineSumOfBitVer2(i);
                // assert
                Assert.AreEqual(expected[i], result, $"Unexpected DefineSumOfBitVer2 result for {i} - {result}, Expected - {expected[i]}");
            }
        }

        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void DefineSumOfBitVer2Case3Test()
        {
            // arrange
            long expected = 2;
            long inputValue = 3;

            // act 
            long result = BruteForceRepresentatives.DefineSumOfBitVer2(inputValue);
            // assert
            Assert.AreEqual(expected, result, $"Unexpected DefineSumOfBitVer2 result for {inputValue} - {result}, Expected - {expected}");
        }

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
