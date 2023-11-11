using CommonLibrary;
using CommonLibrary.Helpers;
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
        public void RepresentativesGreedyCase1()
        {
            // arrange
            List<int> list = new List<int>() { 3, 5, 6, 7, 16 };
            int[][] listOfSet = list.Select(t => BruteForceRepresentatives.GetAsElementNumbers(t, 5).ToArray()).ToArray();
            RepresentativesGreedy representativesGreedy = new RepresentativesGreedy(listOfSet);

            // act
            representativesGreedy.ExecuteSimple();
            representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();

            // assert


        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void RepresentativesGreedyCase2()
        {
            // arrange
            List<int> list = new List<int>() { 3, 5, 7, 10, 24 };
            int[][] listOfSet = list.Select(t => BruteForceRepresentatives.GetAsElementNumbers(t, 5).ToArray()).ToArray();
            RepresentativesGreedy representativesGreedy = new RepresentativesGreedy(listOfSet);
            string solutionAsStringExpected = "1,2,4";

            // act
            representativesGreedy.ExecuteImproved();
            representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();
            string solutionAsString = string.Join(",", representativesGreedy.Solution);

            // assert
            Assert.AreEqual(solutionAsStringExpected, solutionAsString);    

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void RepresentativesGreedyCase3()
        {
            // arrange
            List<int> list = new List<int>() { 5, 11, 12, 18, 21 };
            int[][] listOfSet = list.Select(t => BruteForceRepresentatives.GetAsElementNumbers(t, 5).ToArray()).ToArray();
            RepresentativesGreedy representativesGreedy = new RepresentativesGreedy(listOfSet);
            string solutionAsStringExpected = "1,2,4";

            // act
            representativesGreedy.ExecuteImproved();
            representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();
            string solutionAsString = string.Join(",", representativesGreedy.Solution);

            // assert
            Assert.AreEqual(solutionAsStringExpected, solutionAsString);

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void RepresentativesGreedyCase4()
        {
            // arrange
            List<int> list = new List<int>() { 3, 5, 7, 10, 20 };
            int[][] listOfSet = list.Select(t => BruteForceRepresentatives.GetAsElementNumbers(t, 5).ToArray()).ToArray();
            RepresentativesGreedy representativesGreedy = new RepresentativesGreedy(listOfSet);
            string solutionAsStringExpected = "1,2,4";

            // act
            representativesGreedy.ExecuteImproved();
            representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();
            string solutionAsString = string.Join(",", representativesGreedy.Solution);

            // assert
            Assert.AreEqual(solutionAsStringExpected, solutionAsString);

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void RepresentativesGreedyImroveRDCase1()
        {
            // arrange
            List<int> list = new List<int>() { 6, 7, 9, 10, 20 };
            int[][] listOfSet = list.Select(t => BruteForceRepresentatives.GetAsElementNumbers(t, 5).ToArray()).ToArray();
            RepresentativesGreedy representativesGreedy = new RepresentativesGreedy(listOfSet);
            string solutionAsStringExpected = "2,3";

            // act
            representativesGreedy.ExecuteImprovedRD();
            representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();
            string solutionAsString = string.Join(",", representativesGreedy.Solution);

            // assert
            Assert.AreEqual(solutionAsStringExpected, solutionAsString);

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BranchAndBoundCompareGreedyTest()
        {
            // arrange
            int сardinality = 5;
            int length = 5;
            EnumerateIntegerTrangleForRepresentativesGreedyCompare enumeration = new EnumerateIntegerTrangleForRepresentativesGreedyCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert
            int n = 1 << сardinality;
            long comb = RepresentativesAsTree.Combination(n, length);
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BranchAndBoundCompareGreedyImpTest()
        {
            // arrange
            int сardinality = 5;
            int length = 5;
            EnumerateIntegerTrangleForRepresentativesGreedyImpCompare enumeration = new EnumerateIntegerTrangleForRepresentativesGreedyImpCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert
            int n = 1 << сardinality;
            long comb = RepresentativesAsTree.Combination(n, length);
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BranchAndBoundCompareGreedyImpOnlyDifTest1()
        {
            // arrange
            int сardinality = 6;
            int length = 6;
            EnumerateIntegerTrangleForRepresentativesGreedyCompareOnlyDif enumeration = new EnumerateIntegerTrangleForRepresentativesGreedyCompareOnlyDif(сardinality, length);
            // act
            enumeration.Execute();
            // assert
            int n = 1 << сardinality;
            long comb = RepresentativesAsTree.Combination(n, length);
        }
    }

    //--------------------------------------------------------------------------------------
    // class EnumerateIntegerTrangleForRepresentativesGreedyCompare
    //--------------------------------------------------------------------------------------
    public class EnumerateIntegerTrangleForRepresentativesGreedyCompare : EnumerateIntegerTrangle
    {
        private int _fCardinality;
        private RepresentativesStatisticAccumulator _statisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyStatisticAccumulator;
        //--------------------------------------------------------------------------------------
        private int _wrongResultCount = 0;
        //--------------------------------------------------------------------------------------
        public int WrongResultCount
        {
            get
            {
                return _wrongResultCount;
            }
        }
        //--------------------------------------------------------------------------------------
        private int _gapCount = 0;
        //--------------------------------------------------------------------------------------
        public int GapCount
        {
            get
            {
                return _gapCount;
            }
        }
        //--------------------------------------------------------------------------------------
        private int _oneCount = 0;
        //--------------------------------------------------------------------------------------
        public int OneCount
        {
            get
            {
                return _oneCount;
            }
        }
        //--------------------------------------------------------------------------------------
        private long _count = 0;
        //--------------------------------------------------------------------------------------
        public long Count
        {
            get
            {
                return _count;
            }
        }
        //--------------------------------------------------------------------------------------
        private List<string> _result = new List<string>();
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
            _greedyStatisticAccumulator.DeleteAlgorithm(nameof(RepresentativesGreedy));
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                // arrange
                int[][] listOfSet = _fCurrentSet.Select(t => BruteForceRepresentatives.GetAsElementNumbers(t, _fCardinality).ToArray()).ToArray();
                _count++;
                int count = listOfSet.SelectMany(l => l).Distinct().Count();
                int max = listOfSet.SelectMany(l => l).Max();
                string listAsString = listOfSet.AsString();
                if (max+1 != count)
                {
                    _gapCount++;
                    return false;
                }
                bool isAnyOne = listOfSet.Any(l => l.Count() == 1);
                if (isAnyOne)
                {
                    _oneCount++;
                    return false;
                }
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
        protected override int FirstElement(int pPosition)
        {
            if (pPosition == 0)
                return 3;
            int first = _fCurrentSet[pPosition - 1] + _forwardAdditive;
            while (BruteForceRepresentatives.DefineSumOfBitVer2(first) == 1 && (first < _fLimit))
                first += _forwardAdditive;
            return first;
        }
        //--------------------------------------------------------------------------------------
        protected override bool NextElement(int pPosition)
        {
            if (_fCurrentSet[pPosition] >= _fLimit)
                return false;
            if (BruteForceRepresentatives.DefineSumOfBitVer2(_fCurrentSet[pPosition]) == 1)
                return false;
            _fCurrentSet[pPosition]++;
            if (BruteForceRepresentatives.DefineSumOfBitVer2(_fCurrentSet[pPosition]) == 1)
                _fCurrentSet[pPosition]++;
            return true;
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
    // class EnumerateIntegerTrangleForRepresentativesGreedyCompare
    //--------------------------------------------------------------------------------------
    public class EnumerateIntegerTrangleForRepresentativesGreedyImpCompare : EnumerateIntegerTrangle
    {
        private int _fCardinality;
        private RepresentativesStatisticAccumulator _statisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyStatisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyImpStatisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyImpRDStatisticAccumulator;
        //--------------------------------------------------------------------------------------
        private int _wrongResultCount = 0;
        //--------------------------------------------------------------------------------------
        public int WrongResultCount
        {
            get
            {
                return _wrongResultCount;
            }
        }
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
        private int _gapCount = 0;
        //--------------------------------------------------------------------------------------
        public int GapCount
        {
            get
            {
                return _gapCount;
            }
        }
        //--------------------------------------------------------------------------------------
        private int _oneCount = 0;
        //--------------------------------------------------------------------------------------
        public int OneCount
        {
            get
            {
                return _oneCount;
            }
        }
        //--------------------------------------------------------------------------------------
        private long _count = 0;
        //--------------------------------------------------------------------------------------
        public long Count
        {
            get
            {
                return _count;
            }
        }
        //--------------------------------------------------------------------------------------
        private List<string> _result = new List<string>();
        //--------------------------------------------------------------------------------------
        public List<string> Result
        {
            get
            {
                return _result;
            }
        }
        //--------------------------------------------------------------------------------------
        public EnumerateIntegerTrangleForRepresentativesGreedyImpCompare(int pCardinality, int pLength, int pMinimumValue = 1, int pForwardAdditive = 1)
            : base((1 << pCardinality) - 1, pLength, pMinimumValue, pForwardAdditive)
        {
            _fBreakElement = 0;
            _fCardinality = pCardinality;
            _statisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality);
            _statisticAccumulator.DeleteAlgorithm(nameof(RepresentativesBranchAndBoundByValue));
            _greedyStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality);
            _greedyStatisticAccumulator.DeleteAlgorithm(nameof(RepresentativesGreedy) + "Simple");
            _greedyImpStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality);
            _greedyImpStatisticAccumulator.DeleteAlgorithm(nameof(RepresentativesGreedy) + "Improve");
            _greedyImpRDStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality);
            _greedyImpRDStatisticAccumulator.DeleteAlgorithm(nameof(RepresentativesGreedy) + "ImproveRD");
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                // arrange
                int[][] listOfSet = _fCurrentSet.Select(t => BruteForceRepresentatives.GetAsElementNumbers(t, _fCardinality).ToArray()).ToArray();
                _count++;
                int count = listOfSet.SelectMany(l => l).Distinct().Count();
                int max = listOfSet.SelectMany(l => l).Max();
                string listAsString = listOfSet.AsString();
                if (max + 1 != count)
                {
                    _gapCount++;
                    return false;
                }
                bool isAnyOne = listOfSet.Any(l => l.Count() == 1);
                if (isAnyOne)
                {
                    _oneCount++;
                    return false;
                }
                RepresentativesGreedy representativesGreedy = new RepresentativesGreedy(listOfSet)
                {
                    StatisticAccumulator = _greedyStatisticAccumulator
                };
                RepresentativesGreedy representativesGreedyImp = new RepresentativesGreedy(listOfSet)
                {
                    StatisticAccumulator = _greedyImpStatisticAccumulator
                };
                RepresentativesGreedy representativesGreedyImpRD = new RepresentativesGreedy(listOfSet)
                {
                    StatisticAccumulator = _greedyImpRDStatisticAccumulator
                };
                RepresentativesBranchAndBoundByValue branchAndBound = new RepresentativesBranchAndBoundByValue(_fCardinality, listOfSet)
                {
                    StatisticAccumulator = _statisticAccumulator
                };

                // act
                branchAndBound.Execute();
                representativesGreedy.ExecuteSimple();
                representativesGreedyImp.ExecuteImproved();
                representativesGreedyImpRD.ExecuteImprovedRD();
                branchAndBound.OptimalSets = branchAndBound.OptimalSets.OrderBy(s => s).ToList();
                representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();
                representativesGreedyImp.Solution = representativesGreedyImp.Solution.OrderBy(s => s).ToList();
                representativesGreedyImpRD.Solution = representativesGreedyImpRD.Solution.OrderBy(s => s).ToList();

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
            _greedyImpStatisticAccumulator.SaveRemain();
        }
        //--------------------------------------------------------------------------------------
        protected override int FirstElement(int pPosition)
        {
            if (pPosition == 0)
                return 3;
            int first = _fCurrentSet[pPosition - 1] + _forwardAdditive;
            while (BruteForceRepresentatives.DefineSumOfBitVer2(first) == 1 && (first < _fLimit))
                first += _forwardAdditive;
            return first;
        }
        //--------------------------------------------------------------------------------------
        protected override bool NextElement(int pPosition)
        {
            if (_fCurrentSet[pPosition] >= _fLimit)
                return false;
            if (BruteForceRepresentatives.DefineSumOfBitVer2(_fCurrentSet[pPosition]) == 1)
                return false;
            _fCurrentSet[pPosition]++;
            if (BruteForceRepresentatives.DefineSumOfBitVer2(_fCurrentSet[pPosition]) == 1)
                _fCurrentSet[pPosition]++;
            return true;
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
    // class EnumerateIntegerTrangleForRepresentativesGreedyCompareOnlyDif
    //--------------------------------------------------------------------------------------
    public class EnumerateIntegerTrangleForRepresentativesGreedyCompareOnlyDif : EnumerateIntegerTrangle
    {
        private int _fCardinality;
        private RepresentativesStatisticAccumulator _statisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyStatisticAccumulator;
        //--------------------------------------------------------------------------------------
        private int _wrongResultCount = 0;
        //--------------------------------------------------------------------------------------
        public int WrongResultCount
        {
            get
            {
                return _wrongResultCount;
            }
        }
        //--------------------------------------------------------------------------------------
        private int _gapCount = 0;
        //--------------------------------------------------------------------------------------
        public int GapCount
        {
            get
            {
                return _gapCount;
            }
        }
        //--------------------------------------------------------------------------------------
        private int _oneCount = 0;
        //--------------------------------------------------------------------------------------
        public int OneCount
        {
            get
            {
                return _oneCount;
            }
        }
        //--------------------------------------------------------------------------------------
        private long _count = 0;
        //--------------------------------------------------------------------------------------
        public long Count
        {
            get
            {
                return _count;
            }
        }
        //--------------------------------------------------------------------------------------
        public double WrongRelation
        {
            get
            {
                return 1.0*_wrongResultCount/_count;
            }
        }
        //--------------------------------------------------------------------------------------
        private List<string> _result = new List<string>();
        //--------------------------------------------------------------------------------------
        public List<string> Result
        {
            get
            {
                return _result;
            }
        }
        //--------------------------------------------------------------------------------------
        public EnumerateIntegerTrangleForRepresentativesGreedyCompareOnlyDif(int pCardinality, int pLength, int pMinimumValue = 1, int pForwardAdditive = 1)
            : base((1 << pCardinality) - 1, pLength, pMinimumValue, pForwardAdditive)
        {
            _fBreakElement = 0;
            _fCardinality = pCardinality;
            _statisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality);
            _statisticAccumulator.DeleteAlgorithm(nameof(RepresentativesBranchAndBoundByValue), pLength, pCardinality);
            _greedyStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality);
            _greedyStatisticAccumulator.DeleteAlgorithm(nameof(RepresentativesGreedy) + "ImproveRD", pLength, pCardinality);
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                // arrange
                int[][] listOfSet = _fCurrentSet.Select(t => BruteForceRepresentatives.GetAsElementNumbers(t, _fCardinality).ToArray()).ToArray();
                int count = listOfSet.SelectMany(l => l).Distinct().Count();
                int max = listOfSet.SelectMany(l => l).Max();
                string listAsString = listOfSet.AsString();
                if (max + 1 != count)
                {
                    _gapCount++;
                    return false;
                }
                bool isAnyOne = listOfSet.Any(l => l.Count() == 1);
                if (isAnyOne)
                {
                    _oneCount++;
                    return false;
                }
                _count++;
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
                representativesGreedy.ExecuteImprovedRD();
                branchAndBound.OptimalSets = branchAndBound.OptimalSets.OrderBy(s => s).ToList();
                representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();

                // assert
                if (branchAndBound.CurrentMinimum == representativesGreedy.Solution.Count)
                {
                    String solutionAsString = representativesGreedy.SolutionAsString;
                    Assert.IsTrue(branchAndBound.OptimalSets.Any(o => o == solutionAsString));
                    _statisticAccumulator.RemoveLastStatistic();
                    _greedyStatisticAccumulator.RemoveLastStatistic();
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
        protected override int FirstElement(int pPosition)
        {
            if (pPosition == 0)
                return 3;
            int first = _fCurrentSet[pPosition - 1] + _forwardAdditive;
            while (BruteForceRepresentatives.DefineSumOfBitVer2(first) == 1 && (first < _fLimit))
                first += _forwardAdditive;
            return first;
        }
        //--------------------------------------------------------------------------------------
        protected override bool NextElement(int pPosition)
        {
            if (_fCurrentSet[pPosition] >= _fLimit)
                return false;
            if (BruteForceRepresentatives.DefineSumOfBitVer2(_fCurrentSet[pPosition]) == 1)
                return false;
            _fCurrentSet[pPosition]++;
            if (BruteForceRepresentatives.DefineSumOfBitVer2(_fCurrentSet[pPosition]) == 1)
                _fCurrentSet[pPosition]++;
            return true;
        }
        //--------------------------------------------------------------------------------------
    }

}