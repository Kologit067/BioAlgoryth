using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DNAMapping;
using DNAMapping.Enumeration.DNA;
using System.Linq;
using StatisticsStorage.Accumulators;
using StatisticsStorage.Savers;
using CommonLibrary;
using System.Collections.Generic;

namespace DNAMappingTest
{
    [TestClass]
    //--------------------------------------------------------------------------------------
    // class EnumerateDNAMappingBranchBoundaryTest
    //--------------------------------------------------------------------------------------
    public class EnumerateDNAMappingBranchBoundaryTest
    {
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ExecuteTest()
        {
            // arrange
            int[] excpectedResult = new int[] { 0, 3, 6, 7 };
            int[] pairwiseDifferences = DNAMappingBase.ProduceMatrix(excpectedResult);
            EnumerateDNAMappingBranchBoundary enumeration = new EnumerateDNAMappingBranchBoundary(pairwiseDifferences)
            {
                StatisticAccumulator = new FakeDNAMappingStatisticAccumulator()
            };            // act
            enumeration.Execute();
            // assert
            var result = enumeration.ListOfSolution.FirstOrDefault(l => l.SequenceEqual(excpectedResult));
            Assert.IsNotNull(result, $"Expected result absent in solution list");

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ProcessInputDataTestCaseLength6Limit8()
        {
            // arrange
            EnumerateIntegerTrangleForByBranchBoundary enumeration = new EnumerateIntegerTrangleForByBranchBoundary(8, 6);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
    public class EnumerateIntegerTrangleForByBranchBoundary : EnumerateIntegerTrangle
    {
        private List<string> _result = new List<string>();
        private DNAMappingStatisticAccumulator _statisticAccumulator;
        //--------------------------------------------------------------------------------------
        public List<string> Result
        {
            get
            {
                return _result;
            }
        }
        //--------------------------------------------------------------------------------------
        public EnumerateIntegerTrangleForByBranchBoundary(int pLimit, int pLength, int pMinimumValue = 1, int pForwardAdditive = 0)
            : base(pLimit, pLength, pMinimumValue, pForwardAdditive)
        {
            int size = DNAMappingBase.DefineRestrictionMapSizeFromDifferencesSize(pLength);
            _statisticAccumulator = new DNAMappingStatisticAccumulator(new DNAMappingSaver(), size, pLimit);
            _statisticAccumulator.Delete("EnumerateDNAMappingBranchBoundary");
            _fBreakElement = 0;
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                int[] pairwiseDifferences = _fCurrentSet.ToArray();
                EnumerateDNAMappingBranchBoundary enumeration = new EnumerateDNAMappingBranchBoundary(pairwiseDifferences)
                {
                    StatisticAccumulator = _statisticAccumulator
                };
                enumeration.Execute();

                _result.Add(string.Join(",", _fCurrentSet.Select(t => t.ToString())));
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
        protected override bool IsCompleteCondition()
        {
            return base.IsCompleteCondition();
        }
        //--------------------------------------------------------------------------------------
        protected override void PostAction()
        {
            _statisticAccumulator.SaveRemain();
        }
        //--------------------------------------------------------------------------------------
    }    
    //--------------------------------------------------------------------------------------
}
