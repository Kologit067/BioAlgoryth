﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DNAMapping;
using DNAMapping.Enumeration.DNA;
using System.Linq;

namespace DNAMapping1Test.csproj
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
            EnumerateDNAMappingByIntegerTrangle enumeration = new EnumerateDNAMappingByIntegerTrangle(pairwiseDifferences, 0);
            // act
            enumeration.Execute();
            // assert
            var result = enumeration.ListOfSolution.FirstOrDefault(l => l.SequenceEqual(excpectedResult));
            Assert.IsNotNull(result, $"Expected result absent in solution list");

        }
    }
}
