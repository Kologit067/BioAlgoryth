using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepresentativesSet.Greedy;
using RepresentativesSet;
using RepresentativesSetTest.Base;
using StatisticsStorage.Accumulators;
using StatisticsStorage.Savers;
using System;
using System.Linq;
using CommonLibrary.Helpers;

namespace RepresentativesSetTest.Greedy
{
    [TestClass]
    public class RepresentativesGreedySimpleStepTests
    {
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BranchAndBoundCompareGreedyImpTest()
        {
            // arrange
            int сardinality = 8;
            int length = 7;
            EnumerateIntegerTrangleForRepresentativesGreedyImpCompare enumeration =
                new EnumerateIntegerTrangleForRepresentativesGreedyImpCompare(сardinality, length, 100000, 500);
            // act
            enumeration.Execute();
            // assert
            int n = 1 << сardinality;
            long comb = Combinatorics.Combination(n, length);
        }       
        //--------------------------------------------------------------------------------------
        // class EnumerateIntegerTrangleForRepresentativesGreedyCompare
        //--------------------------------------------------------------------------------------
        public class EnumerateIntegerTrangleForRepresentativesGreedyImpCompare : EnumerateRepresentativesSimpleStepTestBase
        {
            private RepresentativesStatisticAccumulator _statisticAccumulator;
            private RepresentativesStatisticAccumulator _greedyStatisticAccumulator;
            private RepresentativesStatisticAccumulator _greedyImpStatisticAccumulator;
            private RepresentativesStatisticAccumulator _greedyImpRDStatisticAccumulator;

            private RepresentativesGreedy representativesGreedy;
            private RepresentativesGreedy representativesGreedyImp;
            private RepresentativesGreedy representativesGreedyImpRD;
            private RepresentativesBranchAndBoundByValue branchAndBound;
            //--------------------------------------------------------------------------------------
            private int _wrongResultImpCount = 0;
            //--------------------------------------------------------------------------------------
            public int WrongResultImpCount
            {
                get
                {
                    return _wrongResultImpCount;
                }
            }
            //--------------------------------------------------------------------------------------
            private int _wrongResultImpRDCount = 0;
            //--------------------------------------------------------------------------------------
            public int WrongResultImpRDCount
            {
                get
                {
                    return _wrongResultImpRDCount;
                }
            }
            //--------------------------------------------------------------------------------------
            public EnumerateIntegerTrangleForRepresentativesGreedyImpCompare(int pCardinality, int pLength, long maxCount, int bufferSize, int pMinimumValue = 1, int pForwardAdditive = 1)
                : base(pCardinality, pLength, maxCount, pMinimumValue, pForwardAdditive)
            {
                _fBreakElement = 0;
                _fCardinality = pCardinality;
                _statisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, _step, bufferSize);
                _statisticAccumulator.DeleteAlgorithm(nameof(RepresentativesBranchAndBoundByValue));
                _greedyStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, _step, bufferSize);
                _greedyStatisticAccumulator.DeleteAlgorithm(nameof(RepresentativesGreedySimple));
                _greedyImpStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, _step, bufferSize);
                _greedyImpStatisticAccumulator.DeleteAlgorithm(nameof(RepresentativesGreedyImprove));
                _greedyImpRDStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, _step, bufferSize);
                _greedyImpRDStatisticAccumulator.DeleteAlgorithm(nameof(RepresentativesGreedyImproveRD));

                representativesGreedy = new RepresentativesGreedySimple()
                {
                    StatisticAccumulator = _greedyStatisticAccumulator
                };
                representativesGreedyImp = new RepresentativesGreedyImprove()
                {
                    StatisticAccumulator = _greedyImpStatisticAccumulator
                };
                representativesGreedyImpRD = new RepresentativesGreedyImproveRD()
                {
                    StatisticAccumulator = _greedyImpRDStatisticAccumulator
                };
                branchAndBound = new RepresentativesBranchAndBoundByValue(_fCardinality)
                {
                    StatisticAccumulator = _statisticAccumulator
                };

            }
            //--------------------------------------------------------------------------------------
            protected override void ActAction(int[][] listOfSet)
            {
                branchAndBound.Execute(listOfSet);
                representativesGreedy.Execute(listOfSet);
                representativesGreedyImp.Execute(listOfSet);
                representativesGreedyImpRD.Execute(listOfSet);
                branchAndBound.OptimalSets = branchAndBound.OptimalSets.OrderBy(s => s).ToList();
                representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();
                representativesGreedyImp.Solution = representativesGreedyImp.Solution.OrderBy(s => s).ToList();
                representativesGreedyImpRD.Solution = representativesGreedyImpRD.Solution.OrderBy(s => s).ToList();

            }
            //--------------------------------------------------------------------------------------
            protected override void AssertAction()
            {
                if (branchAndBound.CurrentMinimum == representativesGreedy.Solution.Count)
                {
                    String solutionAsString = representativesGreedy.SolutionAsString;
                    Assert.IsTrue(branchAndBound.OptimalSets.Any(o => o == solutionAsString));
                }
                else
                {
                    _wrongResultCount++;
                }
                if (branchAndBound.CurrentMinimum == representativesGreedyImp.Solution.Count)
                {
                    String solutionAsString = representativesGreedyImp.SolutionAsString;
                    Assert.IsTrue(branchAndBound.OptimalSets.Any(o => o == solutionAsString));
                }
                else
                {
                    _wrongResultImpCount++;
                }
                if (branchAndBound.CurrentMinimum == representativesGreedyImpRD.Solution.Count)
                {
                    String solutionAsString = representativesGreedyImpRD.SolutionAsString;
                    Assert.IsTrue(branchAndBound.OptimalSets.Any(o => o == solutionAsString));
                }
                else
                {
                    _wrongResultImpRDCount++;
                }

            }
            //--------------------------------------------------------------------------------------
            protected override void PostAction()
            {
                _statisticAccumulator.SaveRemain();
                _greedyStatisticAccumulator.SaveRemain();
                _greedyImpStatisticAccumulator.SaveRemain();
                _greedyImpRDStatisticAccumulator.SaveRemain();
            }
            //--------------------------------------------------------------------------------------
        }

    }
}
