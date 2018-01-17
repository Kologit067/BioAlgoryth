using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FindingRegulatoryMotifs.Enumeration;
using StatisticsStorage.Accumulators;

namespace RegulatoryMotifsTest
{
    [TestClass]
    public class RegulatoryMotifsSubSequencesEnumerationTset
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ExecuteTestCasepAllResultDecisionSumAsCriteriaAcceptible0()
        {
            // arrange
            string excpectedMotif = "agcgt";
            string expectedSolutionStartPosition = "3,5,1";
            int expectedResult = 0;

            char[][] charSets = new char[][] { new char[] {'a','g','t','a','g','c','g','t','a','a' },
            new char[] {'t','g','t','g','c','a','g','c','g','t' },
            new char[] {'a','a','g','c','g','t','t','a','c','c' }};
            char[] alphabet = new char[] { 'a','c','g','t'};
            int substringLength = 5;
            RegulatoryMotifsSubSequencesEnumeration enumeration = new RegulatoryMotifsSubSequencesEnumeration(charSets, alphabet, substringLength)
            {
                StatisticAccumulator = new FakeRegulatoryMotifsStatisticAccumulator()
            };
            // act
            enumeration.Execute();
            // assert
            string solutionStartPosition = string.Join(",", enumeration.SolutionStartPosition);
            string motifAsString = string.Join("", enumeration.Motif);
            Assert.AreEqual(motifAsString, excpectedMotif, $"Motif is wrong.");
            Assert.AreEqual(solutionStartPosition, expectedSolutionStartPosition, $"Positions are wrong.");
//            Assert.AreEqual(enumeration.OptimalValue, expectedResult, $"Result is wrong.");

        }
    }
}
