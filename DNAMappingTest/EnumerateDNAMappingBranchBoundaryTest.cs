using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DNAMapping;
using DNAMapping.Enumeration.DNA;
using System.Linq;

namespace DNAMappingTest
{
    [TestClass]
    public class EnumerateDNAMappingBranchBoundaryTest
    {
        [TestMethod]
        public void ExecuteTest()
        {
            // arrange
            int[] excpectedResult = new int[] { 0, 3, 6, 7 };
            int[] pairwiseDifferences = DNAMappingBase.ProduceMatrix(excpectedResult);
            EnumerateDNAMappingBranchBoundary enumeration = new EnumerateDNAMappingBranchBoundary(pairwiseDifferences);
            // act
            enumeration.Execute();
            // assert
            var result = enumeration.ListOfSolution.FirstOrDefault(l => l.SequenceEqual(excpectedResult));
            Assert.IsNotNull(result, $"Expected result absent in solution list");

        }
    }
}
