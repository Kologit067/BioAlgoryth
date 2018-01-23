﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FindingRegulatoryMotifs.Enumeration;
using StatisticsStorage.Accumulators;
using System.Collections.Generic;
using System.Linq;
using CommonLibrary;
using BaseContract;
using StatisticsStorage.Savers;

namespace RegulatoryMotifsTest
{
    [TestClass]
    public class RegulatoryMotifsSubSequencesEnumerationTset
    {
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ExecuteTestCasepAllResultDecisionMaxAsCriteriaAcceptible0()
        {
            // arrange
            string excpectedMotif = "agcgt";
            string expectedSolutionStartPosition = "3,5,1";

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

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ExecuteTestCasepNotAllResultOptimizationMaxAsCriteriaExpexted0()
        {
            // arrange
            string excpectedMotif = "agcgt";
            string expectedSolutionStartPosition = "3,5,1";
            int expectedResult = 0;

            char[][] charSets = new char[][] { new char[] {'a','g','t','a','g','c','g','t','a','a' },
            new char[] {'t','g','t','g','c','a','g','c','g','t' },
            new char[] {'a','a','g','c','g','t','t','a','c','c' }};
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            int substringLength = 5;
            RegulatoryMotifsSubSequencesEnumeration enumeration = new RegulatoryMotifsSubSequencesEnumeration(charSets, alphabet, substringLength, pIsAllResult : false, pIsOptimizitaion : true, pIsSumAsCriteria : false)
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
            Assert.AreEqual(enumeration.OptimalValue, expectedResult, $"Result is wrong.");

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ExecuteTestCasepNotAllResultOptimizationMaxAsCriteriaExpexted1()
        {
            // arrange
            string excpectedMotif = "agcgt";
            string expectedSolutionStartPosition = "3,5,1";
            int expectedResult = 1;

            char[][] charSets = new char[][] {
                new char[] {'a','g','t','t','g','c','g','t','a','a' },
                new char[] {'c','g','t','g','c','a','g','t','g','t' },
                new char[] {'a','a','t','c','g','t','a','a','c','c' }};
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            int substringLength = 5;
            RegulatoryMotifsSubSequencesEnumeration enumeration = new RegulatoryMotifsSubSequencesEnumeration(charSets, alphabet, substringLength, pIsAllResult: false, pIsOptimizitaion: true, pIsSumAsCriteria: false)
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
            Assert.AreEqual(enumeration.OptimalValue, expectedResult, $"Result is wrong.");

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ExecuteTestCasepNotAllResultOptimizationSumAsCriteriaExpexted2()
        {
            // arrange
            string excpectedMotif = "cgtaa";
            string expectedSolutionStartPosition = "5,0,3";
            int expectedResult = 2;

            char[][] charSets = new char[][] {
                new char[] {'a','g','t','t','g','c','g','t','a','a' },
                new char[] {'c','g','t','g','c','a','g','t','g','t' },
                new char[] {'a','a','t','c','g','t','a','a','c','c' }};
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            int substringLength = 5;
            RegulatoryMotifsSubSequencesEnumeration enumeration = new RegulatoryMotifsSubSequencesEnumeration(charSets, alphabet, substringLength, pIsAllResult: false, pIsOptimizitaion: true, pIsSumAsCriteria: true)
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
            Assert.AreEqual(enumeration.OptimalValue, expectedResult, $"Result is wrong.");

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ExecuteTestCasepNotAllResultOptimizationMaxAsCriteriaExpexted040()
        {
            // arrange
            string excpectedMotif = "aattg";
            string expectedSolutionStartPosition = "0,4,0";
            int expectedResult = 1;

            char[][] charSets = new char[][] {
                new char[] {'a','g','t','t','g','c','g','t','a','a' },
                new char[] {'c','g','t','g','a','a','g','t','g','t' },
                new char[] {'a','a','t','c','g','t','a','a','c','c' }};
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            int substringLength = 5;
            RegulatoryMotifsSubSequencesEnumeration enumeration = new RegulatoryMotifsSubSequencesEnumeration(charSets, alphabet, substringLength, pIsAllResult: false, pIsOptimizitaion: true, pIsSumAsCriteria: false)
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
            Assert.AreEqual(enumeration.OptimalValue, expectedResult, $"Result is wrong.");
            Assert.AreEqual(enumeration.SolutionStartPositionList.Count, 1, $"More than one solution.");

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ExecuteTestCasepAllResultOptimizationMaxAsCriteriaExpexted040_351()
        {
            // arrange
            string excpectedMotif1 = "aattg";
            string excpectedMotif2 = "agcgt";
            string excpectedMotif3 = "cgtaa";
            string expectedSolutionStartPosition1 = "0,4,0";
            string expectedSolutionStartPosition2 = "3,5,1";
            string expectedSolutionStartPosition3 = "5,0,3";
            int expectedResult = 1;

            char[][] charSets = new char[][] {
                new char[] {'a','g','t','t','g','c','g','t','a','a' },
                new char[] {'c','g','t','g','a','a','g','t','g','t' },
                new char[] {'a','a','t','c','g','t','a','a','c','c' }};
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            int substringLength = 5;
            RegulatoryMotifsSubSequencesEnumeration enumeration = new RegulatoryMotifsSubSequencesEnumeration(charSets, alphabet, substringLength, pIsAllResult: true, pIsOptimizitaion: true, pIsSumAsCriteria: false)
            {
                StatisticAccumulator = new FakeRegulatoryMotifsStatisticAccumulator()
            };
            // act
            enumeration.Execute();
            // assert
            List<string> solutionStartPositions = enumeration.SolutionStartPositionList.Select(s => string.Join(",", s)).ToList();
            List<string> motifsAsString = enumeration.ListOfMotif.Select( m => string.Join("", m)).ToList();
            Assert.IsTrue(motifsAsString.Any(m => m == excpectedMotif1), $"Motif1 is absent.");
            Assert.IsTrue(motifsAsString.Any(m => m == excpectedMotif2), $"Motif2 is absent.");
            Assert.IsTrue(motifsAsString.Any(m => m == excpectedMotif3), $"Motif3 is absent.");
            Assert.IsTrue(solutionStartPositions.Any(p => p == expectedSolutionStartPosition1), $"Positions1 is absent.");
            Assert.IsTrue(solutionStartPositions.Any(p => p == expectedSolutionStartPosition2), $"Positions2 is absent.");
            Assert.IsTrue(solutionStartPositions.Any(p => p == expectedSolutionStartPosition3), $"Positions3 is absent.");
            Assert.AreEqual(enumeration.OptimalValue, expectedResult, $"Result is wrong.");
            Assert.AreEqual(enumeration.SolutionStartPositionList.Count, 3, $"Wrong number of solutions.");

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ExecuteTestCasepNotAllResultOptimizationSumAsCriteriaExpexted351()
        {
            // arrange
            string excpectedMotif1 = "agcgt";
            string excpectedMotif2 = "cgtaa";
            string expectedSolutionStartPosition1 = "3,5,1";
            string expectedSolutionStartPosition2 = "5,0,3";
            int expectedResult = 2;

            char[][] charSets = new char[][] {
                new char[] {'a','g','t','a','g','c','g','t','a','a' },
                new char[] {'c','g','t','g','c','a','g','t','g','t' },
                new char[] {'a','a','t','c','g','t','a','a','c','c' }};
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            int substringLength = 5;
            RegulatoryMotifsSubSequencesEnumeration enumeration = new RegulatoryMotifsSubSequencesEnumeration(charSets, alphabet, substringLength, pIsAllResult: true, pIsOptimizitaion: true, pIsSumAsCriteria: true)
            {
                StatisticAccumulator = new FakeRegulatoryMotifsStatisticAccumulator()
            };
            // act
            // cgtaa
            // cgtgc
            // cgtaa
            enumeration.Execute();
            // assert
            string solutionStartPosition = string.Join(",", enumeration.SolutionStartPosition);
            string motifAsString = string.Join("", enumeration.Motif);
            List<string> solutionStartPositions = enumeration.SolutionStartPositionList.Select(s => string.Join(",", s)).ToList();
            List<string> motifsAsString = enumeration.ListOfMotif.Select(m => string.Join("", m)).ToList();
            Assert.IsTrue(motifsAsString.Any(m => m == excpectedMotif1), $"Motif1 is absent.");
            Assert.IsTrue(motifsAsString.Any(m => m == excpectedMotif2), $"Motif2 is absent.");
            Assert.IsTrue(solutionStartPositions.Any(p => p == expectedSolutionStartPosition1), $"Positions1 is absent.");
            Assert.IsTrue(solutionStartPositions.Any(p => p == expectedSolutionStartPosition2), $"Positions2 is absent.");
            Assert.AreEqual(enumeration.OptimalValue, expectedResult, $"Result is wrong.");
            Assert.AreEqual(enumeration.SolutionStartPositionList.Count, 2, $"Wrong number of solutions.");

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ExecuteTestCasepAllResultOptimizationSumAsCriteriaExpexted351()
        {
            // arrange
            string excpectedMotif = "agcgt";
            string expectedSolutionStartPosition = "3,5,1";
            int expectedResult = 2;

            char[][] charSets = new char[][] {
                new char[] {'a','g','t','a','g','c','g','t','a','a' },
                new char[] {'c','g','t','g','c','a','g','t','g','t' },
                new char[] {'a','a','t','c','g','t','a','a','c','c' }};
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            int substringLength = 5;
            RegulatoryMotifsSubSequencesEnumeration enumeration = new RegulatoryMotifsSubSequencesEnumeration(charSets, alphabet, substringLength, pIsAllResult: false, pIsOptimizitaion: true, pIsSumAsCriteria: true)
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
            Assert.AreEqual(enumeration.OptimalValue, expectedResult, $"Result is wrong.");

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ExecuteTestCasepAllResultDecisionMaxAsCriteriaAccept0()
        {
            // arrange

            char[][] charSets = new char[][] {
                "agtagcgtaa".ToArray(),
                "cgtgcagtgt".ToArray(),
                "aatcgtaacc".ToArray()};
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            int substringLength = 5;
            RegulatoryMotifsSubSequencesEnumeration enumeration = new RegulatoryMotifsSubSequencesEnumeration(charSets, alphabet, substringLength, pIsAllResult: true, pIsOptimizitaion: false, pIsSumAsCriteria: false, pAcceptibleDistance : 0)
            {
                StatisticAccumulator = new FakeRegulatoryMotifsStatisticAccumulator()
            };
            // act
            // cgtaa
            // cgtgc
            // cgtaa

            // agcgt
            // agtgt
            // atcgt

            enumeration.Execute();
            // assert
            Assert.AreEqual(enumeration.SolutionStartPositionList.Count, 0, $"Wrong number of solutions.");
            Assert.AreEqual(enumeration.ListOfMotif.Count, 0, $"Wrong number of motifs.");

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ExecuteTestCasepAllResultDecisionMaxAsCriteriaAccept1()
        {
            // arrange
            string excpectedMotif1 = "agcgt";
//            string excpectedMotif2 = "cgtaa";
            string expectedSolutionStartPosition1 = "3,5,1";
//            string expectedSolutionStartPosition2 = "5,0,3";

            char[][] charSets = new char[][] {
                "agtagcgtaa".ToArray(),
                "cgtgcagtgt".ToArray(),
                "aatcgtaacc".ToArray()};
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            int substringLength = 5;
            RegulatoryMotifsSubSequencesEnumeration enumeration = new RegulatoryMotifsSubSequencesEnumeration(charSets, alphabet, substringLength, pIsAllResult: true, pIsOptimizitaion: false, pIsSumAsCriteria: false, pAcceptibleDistance: 1)
            {
                StatisticAccumulator = new FakeRegulatoryMotifsStatisticAccumulator()
            };
            // act
            // cgtaa
            // cgtgc
            // cgtaa

            // agcgt
            // agtgt
            // atcgt

            enumeration.Execute();
            // assert
            string solutionStartPosition = string.Join(",", enumeration.SolutionStartPosition);
            string motifAsString = string.Join("", enumeration.Motif);
            List<string> solutionStartPositions = enumeration.SolutionStartPositionList.Select(s => string.Join(",", s)).ToList();
            List<string> motifsAsString = enumeration.ListOfMotif.Select(m => string.Join("", m)).ToList();
            Assert.IsTrue(motifsAsString.Any(m => m == excpectedMotif1), $"Motif1 is absent.");
            Assert.IsTrue(solutionStartPositions.Any(p => p == expectedSolutionStartPosition1), $"Positions1 is absent.");
            Assert.AreEqual(enumeration.SolutionStartPositionList.Count, 1, $"Wrong number of solutions.");
            Assert.AreEqual(enumeration.ListOfMotif.Count, 1, $"Wrong number of motifs.");

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ExecuteTestCasepAllResultDecisionMaxAsCriteriaAccept2()
        {
            // arrange
            string excpectedMotif1 = "agcgt";
            string excpectedMotif2 = "cgtaa";
            string expectedSolutionStartPosition1 = "3,5,1";
            string expectedSolutionStartPosition2 = "5,0,3";

            char[][] charSets = new char[][] {
                "agtagcgtaa".ToArray(),
                "cgtgcagtgt".ToArray(),
                "aatcgtaacc".ToArray()};
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            int substringLength = 5;
            RegulatoryMotifsSubSequencesEnumeration enumeration = new RegulatoryMotifsSubSequencesEnumeration(charSets, alphabet, substringLength, pIsAllResult: true, pIsOptimizitaion: false, pIsSumAsCriteria: false, pAcceptibleDistance: 2)
            {
                StatisticAccumulator = new FakeRegulatoryMotifsStatisticAccumulator()
            };

            enumeration.Execute();
            // assert
            string solutionStartPosition = string.Join(",", enumeration.SolutionStartPosition);
            string motifAsString = string.Join("", enumeration.Motif);
            List<string> solutionStartPositions = enumeration.SolutionStartPositionList.Select(s => string.Join(",", s)).ToList();
            List<string> motifsAsString = enumeration.ListOfMotif.Select(m => string.Join("", m)).ToList();
            Assert.IsTrue(motifsAsString.Any(m => m == excpectedMotif1), $"Motif1 is absent.");
            Assert.IsTrue(motifsAsString.Any(m => m == excpectedMotif2), $"Motif2 is absent.");
            Assert.IsTrue(solutionStartPositions.Any(p => p == expectedSolutionStartPosition1), $"Positions1 is absent.");
            Assert.IsTrue(solutionStartPositions.Any(p => p == expectedSolutionStartPosition2), $"Positions2 is absent.");
            Assert.IsTrue(enumeration.SolutionStartPositionList.Count >= 2, $"Wrong number of solutions.");
            Assert.IsTrue(enumeration.ListOfMotif.Count >= 2, $"Wrong number of motifs.");

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ExecuteTestCasepAllResultDecisionSumAsCriteriaAccept2()
        {
            // arrange
//            string excpectedMotif1 = "agcgt";
            string excpectedMotif2 = "cgtaa";
//            string expectedSolutionStartPosition1 = "3,5,1";
            string expectedSolutionStartPosition2 = "5,0,3";

            char[][] charSets = new char[][] {
                "agtagcgtaa".ToArray(),
                "cgtgcagtgc".ToArray(),
                "aatcgtaacc".ToArray()};
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            int substringLength = 5;
            RegulatoryMotifsSubSequencesEnumeration enumeration = new RegulatoryMotifsSubSequencesEnumeration(charSets, alphabet, substringLength, pIsAllResult: true, pIsOptimizitaion: false, pIsSumAsCriteria: true, pAcceptibleDistance: 2)
            {
                StatisticAccumulator = new FakeRegulatoryMotifsStatisticAccumulator()
            };
            // act
            // cgtaa
            // cgtgc
            // cgtaa

            // agcgt
            // agtgc
            // atcgt

            enumeration.Execute();
            // assert
            string solutionStartPosition = string.Join(",", enumeration.SolutionStartPosition);
            string motifAsString = string.Join("", enumeration.Motif);
            List<string> solutionStartPositions = enumeration.SolutionStartPositionList.Select(s => string.Join(",", s)).ToList();
            List<string> motifsAsString = enumeration.ListOfMotif.Select(m => string.Join("", m)).ToList();
            Assert.IsTrue(motifsAsString.Any(m => m == excpectedMotif2), $"Motif2 is absent.");
            Assert.IsTrue(solutionStartPositions.Any(p => p == expectedSolutionStartPosition2), $"Positions2 is absent.");
            Assert.AreEqual(enumeration.SolutionStartPositionList.Count, 1, $"Wrong number of solutions.");
            Assert.AreEqual(enumeration.ListOfMotif.Count, 1, $"Wrong number of motifs.");

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ProcessInputDataTestCaseLength6Limit8()
        {
            // arrange
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            EnumerateCharSetForMotifsSubSequences enumeration = new EnumerateCharSetForMotifsSubSequences(alphabet, 7, 3, 
                4, pIsAllResult : false, pIsOptimizitaion : false, pIsSumAsCriteria : false, pAcceptibleDistance : 0);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
    // class EnumerateCharSetForMotifsSubSequences 
    //--------------------------------------------------------------------------------------
    public class EnumerateCharSetForMotifsSubSequences : EnumerateIntegerCharSet
    {
        protected int _acceptibleDistance;
        protected bool _isAllResult;
        protected bool _isOptimizitaion;
        protected bool _isSumAsCriteria;
        protected int _sequenceLength;
        protected int _numberOfSequence;
        protected int _patternLength;
        protected RegulatoryMotifsStatisticAccumulator _statisticAccumulator { get; set; }
        //--------------------------------------------------------------------------------------
        public EnumerateCharSetForMotifsSubSequences(char[] pCharSet, int pSequenceLength, int pNumberOfSequence, int pPatternLength, bool pIsAllResult, bool pIsOptimizitaion = false, bool pIsSumAsCriteria = false, int pAcceptibleDistance = 0)
            : base(pCharSet, pSequenceLength * pNumberOfSequence, 0)
        {
            _patternLength = pPatternLength;
            _acceptibleDistance = pAcceptibleDistance;
            _isOptimizitaion = pIsOptimizitaion;
            _isSumAsCriteria = pIsSumAsCriteria;
            _isAllResult = pIsAllResult;
            _numberOfSequence = pNumberOfSequence;
            _sequenceLength = pSequenceLength;
            var sequenceLengthes = string.Join(",", Enumerable.Repeat(_sequenceLength, _numberOfSequence));
            _statisticAccumulator = new RegulatoryMotifsStatisticAccumulator(new RegulatoryMotifSaver(), _patternLength,sequenceLengthes, 
                _isOptimizitaion, _isSumAsCriteria, _isAllResult, _acceptibleDistance);
            _statisticAccumulator.Delete("RegulatoryMotifsSubSequencesEnumeration");
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                var currentSequence = _fCurrentSet.Select(i => _charSet[i]).ToList();
                char[][] charSets = Enumerable.Range(0, _numberOfSequence).Select(i => currentSequence.Skip(i* _sequenceLength).Take(_sequenceLength).ToArray()).ToArray();
                RegulatoryMotifsSubSequencesEnumeration enumeration = new RegulatoryMotifsSubSequencesEnumeration(charSets, _charSet, _patternLength, pIsAllResult: false, pIsOptimizitaion: _isOptimizitaion, pIsSumAsCriteria: _isSumAsCriteria, pAcceptibleDistance: _acceptibleDistance)
                {
                    StatisticAccumulator = _statisticAccumulator
                };
                // act
                enumeration.Execute();
                // assert


//                _result.Add(string.Join(",", _fCurrentSet.Select(t => t.ToString())));
            }

            return false;
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
