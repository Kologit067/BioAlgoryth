using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DNAMapping;
using DNAMapping.Enumeration.DNA;
using System.Linq;
using StatisticsStorage.Accumulators;
using StatisticsStorage.Savers;
using System.Collections.Generic;
using CommonLibrary;

namespace DNAMappingTest
{
    //--------------------------------------------------------------------------------------
    [TestClass]
    public class EnumerateDNAMappingByIntegerTrangleTest
    {
        //--------------------------------------------------------------------------------------
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

        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ProcessInputDataTestCaseLength6Limit8()
        {
            // arrange
            EnumerateIntegerTrangleForInput enumeration = new EnumerateIntegerTrangleForInput(8, 6, 0, 0);
            // act
            enumeration.Execute();
            // assert

        }

        //--------------------------------------------------------------------------------------
    }

    //--------------------------------------------------------------------------------------
    public class EnumerateIntegerTrangleForInput : EnumerateIntegerTrangle
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
        public EnumerateIntegerTrangleForInput(int pLimit, int pLength, int pMinimumValue = 0, int pForwardAdditive = 0)
            : base(pLimit, pLength, pMinimumValue, pForwardAdditive)
        {
            _statisticAccumulator = new DNAMappingStatisticAccumulator(new DNAMappingSaver());
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                int[] pairwiseDifferences = _fCurrentSet.ToArray();
                EnumerateDNAMappingByIntegerTrangle enumeration = new EnumerateDNAMappingByIntegerTrangle(pairwiseDifferences, 0)
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
            if ((_fCurrentPosition == 0) && (_fCurrentSet[_fCurrentPosition] != 0))
            {
                TerminalAction();
                return true;
            }
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
