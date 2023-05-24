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
                int result = BruteForceRepresentatives.DefineSumOfBit(i, 64, 6);
                // assert
                Assert.AreEqual(expected[i], result, $"Unexpected DefineSumOfBit result for {i} - {result}, Expected - {expected[i]}");
            }
        }

        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void DefineSumOfBitCompareTest()
        {
            // arrange

            for (long i = 0; i < (long)1 << 13; i++)
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
            long[] expected = { 0, 1, 1, 2, 1, 2, 2, 3, 1, 2, 2, 3, 2, 3, 3, 4, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5, 1 };

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
            BruteForceRepresentatives bruteForceVer2 = new BruteForceRepresentatives();
            List<int> expectedResult = new List<int>() { 0, 2, 4};
            string expectedResult2 = "1,3,4";

            // act
            List<int> result = bruteForce.ExecuteByBinary(listOfSet);
            List<int> resultVer2 = bruteForceVer2.ExecuteByBinaryVer2(listOfSet);

            // assert
            Assert.AreEqual(expectedResult.Count, result.Count, "Wrong number rows in result");
            Assert.AreEqual(expectedResult.Count, resultVer2.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], result[i], $"Wrong string in position {i} - {result[i]}. Expected - {expectedResult[i]}");
                Assert.AreEqual(expectedResult[i], resultVer2[i], $"Wrong string in position {i} - {resultVer2[i]}. Expected - {expectedResult[i]}");
            }

            Assert.IsTrue(bruteForce.OptimalSets.Contains(expectedResult2));
            Assert.IsTrue(bruteForceVer2.OptimalSets.Contains(expectedResult2));

        }

        [TestMethod]
        public void ExecuteTestCase2()
        {
            // arrange
            int[][] listOfSet = new int[][] { new int[] { 0, 1, 3 }, new int[] { 0, 2, 3 }, new int[] { 0, 3, 4 } }; ;
            BruteForceRepresentatives bruteForce = new BruteForceRepresentatives();
            BruteForceRepresentatives bruteForceVer2 = new BruteForceRepresentatives();
            List<int> expectedResult = new List<int>() { 0 };
            string expectedResult2 = "3";

            // act
            List<int> result = bruteForce.ExecuteByBinary(listOfSet);
            List<int> resultVer2 = bruteForceVer2.ExecuteByBinaryVer2(listOfSet);

            // assert
            Assert.AreEqual(expectedResult.Count, result.Count, "Wrong number rows in result");
            Assert.AreEqual(expectedResult.Count, resultVer2.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], result[i], $"Wrong string in position {i} - {result[i]}. Expected - {expectedResult[i]}");
                Assert.AreEqual(expectedResult[i], resultVer2[i], $"Wrong string in position {i} - {resultVer2[i]}. Expected - {expectedResult[i]}");
            }

            Assert.IsTrue(bruteForce.OptimalSets.Contains(expectedResult2));
            Assert.IsTrue(bruteForceVer2.OptimalSets.Contains(expectedResult2));

        }

        [TestMethod]
        public void ExecuteTestCase3()
        {
            // arrange
            int[][] listOfSet = new int[][] { new int[] { 0, 1 }, new int[] { 2, 3 }, new int[] { 0, 4 } }; ;
            BruteForceRepresentatives bruteForce = new BruteForceRepresentatives();
            BruteForceRepresentatives bruteForceVer2 = new BruteForceRepresentatives();
            List<int> expectedResult = new List<int>() {0, 2 };
            string expectedResult2 = "0,3";

            // act
            List<int> result = bruteForce.ExecuteByBinary(listOfSet);
            List<int> resultVer2 = bruteForceVer2.ExecuteByBinaryVer2(listOfSet);

            // assert
            Assert.AreEqual(expectedResult.Count, result.Count, "Wrong number rows in result");
            Assert.AreEqual(expectedResult.Count, resultVer2.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], result[i], $"Wrong string in position {i} - {result[i]}. Expected - {expectedResult[i]}");
                Assert.AreEqual(expectedResult[i], resultVer2[i], $"Wrong string in position {i} - {resultVer2[i]}. Expected - {expectedResult[i]}");
            }

            Assert.IsTrue(bruteForce.OptimalSets.Contains(expectedResult2));
            Assert.IsTrue(bruteForceVer2.OptimalSets.Contains(expectedResult2));

        }

        [TestMethod]
        public void ExecuteTestCase4()
        {
            // arrange
            int[][] listOfSet = new int[][] { new int[] { 0, 1 }, new int[] { 2, 3 }, new int[] { 4 }, new int[] { 1, 3 } };
            BruteForceRepresentatives bruteForce = new BruteForceRepresentatives();
            BruteForceRepresentatives bruteForceVer2 = new BruteForceRepresentatives();
            List<int> expectedResult = new List<int>() { 1, 2, 4 };
            string expectedResult2 = "1,3,4";

            // act
            List<int> result = bruteForce.ExecuteByBinary(listOfSet);
            List<int> resultVer2 = bruteForceVer2.ExecuteByBinaryVer2(listOfSet);

            // assert
            Assert.AreEqual(expectedResult.Count, result.Count, "Wrong number rows in result");
            Assert.AreEqual(expectedResult.Count, resultVer2.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], result[i], $"Wrong string in position {i} - {result[i]}. Expected - {expectedResult[i]}");
                Assert.AreEqual(expectedResult[i], resultVer2[i], $"Wrong string in position {i} - {resultVer2[i]}. Expected - {expectedResult[i]}");
            }

            Assert.IsTrue(bruteForce.OptimalSets.Contains(expectedResult2));
            Assert.IsTrue(bruteForceVer2.OptimalSets.Contains(expectedResult2));

        }

        [TestMethod]
        public void ElementNumbersToLongAsBinaryVectorTest()
        {
            // arrange
            int[] vector1  = new int[] { 0};
            int[] vector2  = new int[] { 1};
            int[] vector3  = new int[] { 0, 1 };
            int[] vector4  = new int[] { 2 };
            int[] vector5  = new int[] { 0, 2 };
            int[] vector6  = new int[] { 1, 2 };
            int[] vector7  = new int[] { 0, 1, 2 };
            int[] vector8  = new int[] { 3 };
            int[] vector9  = new int[] { 0, 3 };
            int[] vector10 = new int[] { 1, 3 };
            int[] vector11 = new int[] { 0, 1, 3 };
            int[] vector12 = new int[] { 2, 3 };
            int[] vector13 = new int[] { 0, 2, 3 };
            int[] vector14 = new int[] { 1, 2, 3 };
            int[] vector15 = new int[] { 0, 1, 2, 3 };
            long expectedResult1  = 1;
            long expectedResult2  = 2;
            long expectedResult3  = 3;
            long expectedResult4  = 4;
            long expectedResult5  = 5;
            long expectedResult6  = 6;
            long expectedResult7  = 7;
            long expectedResult8  = 8;
            long expectedResult9  = 9;
            long expectedResult10 = 10;
            long expectedResult11 = 11;
            long expectedResult12 = 12;
            long expectedResult13 = 13;
            long expectedResult14 = 14;
            long expectedResult15 = 15;
            BruteForceRepresentatives bruteForce = new BruteForceRepresentatives();

            // act
            long result1  = BruteForceRepresentatives.ElementNumbersToLongAsBinaryVector(vector1);
            long result2  = BruteForceRepresentatives.ElementNumbersToLongAsBinaryVector(vector2);
            long result3  = BruteForceRepresentatives.ElementNumbersToLongAsBinaryVector(vector3);
            long result4  = BruteForceRepresentatives.ElementNumbersToLongAsBinaryVector(vector4);
            long result5  = BruteForceRepresentatives.ElementNumbersToLongAsBinaryVector(vector5);
            long result6  = BruteForceRepresentatives.ElementNumbersToLongAsBinaryVector(vector6);
            long result7  = BruteForceRepresentatives.ElementNumbersToLongAsBinaryVector(vector7);
            long result8  = BruteForceRepresentatives.ElementNumbersToLongAsBinaryVector(vector8);
            long result9  = BruteForceRepresentatives.ElementNumbersToLongAsBinaryVector(vector9);
            long result10 = BruteForceRepresentatives.ElementNumbersToLongAsBinaryVector(vector10);
            long result11 = BruteForceRepresentatives.ElementNumbersToLongAsBinaryVector(vector11);
            long result12 = BruteForceRepresentatives.ElementNumbersToLongAsBinaryVector(vector12);
            long result13 = BruteForceRepresentatives.ElementNumbersToLongAsBinaryVector(vector13);
            long result14 = BruteForceRepresentatives.ElementNumbersToLongAsBinaryVector(vector14);
            long result15 = BruteForceRepresentatives.ElementNumbersToLongAsBinaryVector(vector15);

            // assert
            Assert.AreEqual(expectedResult1,  result1,  $"Wrong result: {result1}.  Expected: {expectedResult1}");
            Assert.AreEqual(expectedResult2,  result2,  $"Wrong result: {result2}.  Expected: {expectedResult2}");
            Assert.AreEqual(expectedResult3,  result3,  $"Wrong result: {result3}.  Expected: {expectedResult3}");
            Assert.AreEqual(expectedResult4,  result4,  $"Wrong result: {result4}.  Expected: {expectedResult4}");
            Assert.AreEqual(expectedResult5,  result5,  $"Wrong result: {result5}.  Expected: {expectedResult5}");
            Assert.AreEqual(expectedResult6,  result6,  $"Wrong result: {result6}.  Expected: {expectedResult6}");
            Assert.AreEqual(expectedResult7,  result7,  $"Wrong result: {result7}.  Expected: {expectedResult7}");
            Assert.AreEqual(expectedResult8,  result8,  $"Wrong result: {result8}.  Expected: {expectedResult8}");
            Assert.AreEqual(expectedResult9,  result9,  $"Wrong result: {result9}.  Expected: {expectedResult9}");
            Assert.AreEqual(expectedResult10, result10, $"Wrong result: {result10}. Expected: {expectedResult10}");
            Assert.AreEqual(expectedResult11, result11, $"Wrong result: {result11}. Expected: {expectedResult11}");
            Assert.AreEqual(expectedResult12, result12, $"Wrong result: {result12}. Expected: {expectedResult12}");
            Assert.AreEqual(expectedResult13, result13, $"Wrong result: {result13}. Expected: {expectedResult13}");
            Assert.AreEqual(expectedResult14, result14, $"Wrong result: {result14}. Expected: {expectedResult14}");
            Assert.AreEqual(expectedResult15, result15, $"Wrong result: {result15}. Expected: {expectedResult15}");
        }

    }
}
