using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepresentativesSet;
using System;
using System.Collections.Generic;

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

            //Assert.IsTrue(bruteForce.OptimalSets.Contains(expectedResult2));
            //Assert.IsTrue(representativesTriangle.OptimalSets.Contains(expectedResultDirect2));

        }

    }
}
