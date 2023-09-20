using CommonLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepresentativesSet.Greedy;
using RepresentativesSetTest;
using StatisticsStorage.Accumulators;
using StatisticsStorage.Savers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativesSet.Greedy.Tests
{
    [TestClass()]
    public class RepresentativesGreedyTests
    {
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BruteForceCompareTestCase21()
        {
            // arrange
            int сardinality = 5;
            int length = 5;
            EnumerateIntegerTrangleForRepresentativesGreedyCompare enumeration = new EnumerateIntegerTrangleForRepresentativesGreedyCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert

        }
    }

    //--------------------------------------------------------------------------------------
    // class EnumerateIntegerTrangleForRepresentativesGreedyCompare
    //--------------------------------------------------------------------------------------
    public class EnumerateIntegerTrangleForRepresentativesGreedyCompare : EnumerateIntegerTrangle
    {
        private int _fCardinality;
        private List<string> _result = new List<string>();
        private RepresentativesStatisticAccumulator _statisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyStatisticAccumulator;
        private int _wrongResultCount = 0;
        //--------------------------------------------------------------------------------------
        public List<string> Result
        {
            get
            {
                return _result;
            }
        }
        //--------------------------------------------------------------------------------------
        public EnumerateIntegerTrangleForRepresentativesGreedyCompare(int pCardinality, int pLength, int pMinimumValue = 1, int pForwardAdditive = 1)
            : base((1 << pCardinality) - 1, pLength, pMinimumValue, pForwardAdditive)
        {
            _fBreakElement = 0;
            _fCardinality = pCardinality;
            _statisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality);
            _statisticAccumulator.DeleteAlgorithm(nameof(RepresentativesBranchAndBoundByValue));
            _greedyStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality);
            _greedyStatisticAccumulator.DeleteAlgorithm(nameof(RepresentativesBranchAndBoundByValue));
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                // arrange
                int[][] listOfSet = _fCurrentSet.Select(t => BruteForceRepresentatives.GetAsElementNumbers(t, _fCardinality).ToArray()).ToArray();
                RepresentativesGreedy representativesGreedy = new RepresentativesGreedy(listOfSet)
                {
                    StatisticAccumulator = _greedyStatisticAccumulator
                };
                RepresentativesBranchAndBoundByValue branchAndBound = new RepresentativesBranchAndBoundByValue(_fCardinality, listOfSet)
                {
                    StatisticAccumulator = _statisticAccumulator
                };

                // act
                branchAndBound.Execute();
                representativesGreedy.ExecuteSimple();
                branchAndBound.OptimalSets = branchAndBound.OptimalSets.OrderBy(s => s).ToList();
                representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();

                // assert
                if (branchAndBound.CurrentMinimum == representativesGreedy.Solution.Count)
                {
                    String solutionAsString = representativesGreedy.SolutionAsString;
                    Assert.IsTrue(branchAndBound.OptimalSets.Any(o => o == solutionAsString));
                }
                else
                {
                    _wrongResultCount++;
                }

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
            _greedyStatisticAccumulator.SaveRemain();
        }
        //--------------------------------------------------------------------------------------
                representativesGreedyImp.ExecuteImproved();

    }
}