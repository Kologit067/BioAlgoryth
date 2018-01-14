using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegulatoryMotifsTest
{
    [TestClass]
    public class RegulatoryMotifsBoundaryBranchEnumerationTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ExecuteTestCase1()
        {
            // arrange
            int[] excpectedResult = new int[] { 0, 3, 6, 7 };
            int[] pairwiseDifferences = DNAMappingBase.ProduceMatrix(excpectedResult);
            string pairwiseDifferencesAsString = string.Join(",", pairwiseDifferences.OrderBy(p => p));
            EnumerateDNAMappingByIntegerTrangle enumeration = new EnumerateDNAMappingByIntegerTrangle(pairwiseDifferences, 0)
            {
                StatisticAccumulator = new FakeDNAMappingStatisticAccumulator()
            };
            // act
            enumeration.Execute();
            // assert
            var result = enumeration.ListOfSolution.FirstOrDefault(l => l.SequenceEqual(excpectedResult));
            Assert.IsNotNull(result, $"Expected result absent in solution list");

        }
    }
}
