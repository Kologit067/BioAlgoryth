﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DNAMapping;
using DNAMapping.Enumeration.DNA;
using System.Linq;
using StatisticsStorage.Accumulators;
using StatisticsStorage.Savers;

namespace DNAMappingTest
{
    [TestClass]
    public class EnumerateDNAMappingByIntegerTrangleTest
    {
        [TestMethod]
        public void ExecuteTest()
        {
            // arrange
            int[] excpectedResult = new int[] { 0, 3, 6, 7};
            int[] pairwiseDifferences = DNAMappingBase.ProduceMatrix(excpectedResult);
            EnumerateDNAMappingByIntegerTrangle enumeration = new EnumerateDNAMappingByIntegerTrangle(pairwiseDifferences, 0)
            {
                StatisticAccumulator = new DNAMappingStatisticAccumulator(new DNAMappingSaver())
            };
            // act
            enumeration.Execute();
            // assert
            var result = enumeration.ListOfSolution.FirstOrDefault(l => l.SequenceEqual(excpectedResult));
            Assert.IsNotNull(result, $"Expected result absent in solution list");

        }
    }
}
