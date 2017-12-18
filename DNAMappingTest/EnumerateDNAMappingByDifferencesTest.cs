using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DNAMapping.Enumeration.DNA;
using DNAMapping;
using System.Linq;

namespace DNAMappingTest
{
    /// <summary>
    /// Summary description for EnumerateDNAMappingByDifferencesTest
    /// </summary>
    [TestClass]
    public class EnumerateDNAMappingByDifferencesTest
    {
        [TestMethod]
        public void ExecuteTest()
        {
            // arrange
            int[] excpectedResult = new int[] { 0, 3, 6, 7 };
            int[] pairwiseDifferences = DNAMappingBase.ProduceMatrix(excpectedResult);
            EnumerateDNAMappingByDifferences enumeration = new EnumerateDNAMappingByDifferences(pairwiseDifferences);
            // act
            enumeration.Execute();
            // assert
            var result = enumeration.ListOfSolution.FirstOrDefault(l => l.SequenceEqual(excpectedResult));
            Assert.IsNotNull(result, $"Expected result absent in solution list");

        }
    }
}
