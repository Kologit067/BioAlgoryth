using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FindingRegulatoryMotifs.Enumeration;
using StatisticsStorage.Accumulators;
using System.Collections.Generic;
using System.Linq;
using CommonLibrary;
using StatisticsStorage.Savers;

namespace RegulatoryMotifsTest
{
    [TestClass]
    public class RegulatoryMotifsPatternEnumerationTest
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
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            int substringLength = 5;
            RegulatoryMotifsPatternEnumeration enumeration = new RegulatoryMotifsPatternEnumeration(alphabet, charSets, substringLength, pIsAllResult : true, pIsOptimizitaion : false, pIsSumAsCriteria : false, pAcceptibleDistance : 0)
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

            char[][] charSets = new char[][] {
                "agtagcgtaa".ToArray(),
                "tgtgcagcgt".ToArray(),
                "aagcgttacc".ToArray()
            };
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            int substringLength = 5;
            RegulatoryMotifsPatternEnumeration enumeration = new RegulatoryMotifsPatternEnumeration(alphabet, charSets, substringLength, pIsAllResult: false, pIsOptimizitaion: true, pIsSumAsCriteria: false)
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
            RegulatoryMotifsPatternEnumeration enumeration = new RegulatoryMotifsPatternEnumeration(alphabet, charSets, substringLength, pIsAllResult: false, pIsOptimizitaion: true, pIsSumAsCriteria: false)
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
            RegulatoryMotifsPatternEnumeration enumeration = new RegulatoryMotifsPatternEnumeration(alphabet, charSets, substringLength, pIsAllResult: false, pIsOptimizitaion: true, pIsSumAsCriteria: true)
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
            RegulatoryMotifsPatternEnumeration enumeration = new RegulatoryMotifsPatternEnumeration(alphabet, charSets, substringLength, pIsAllResult: false, pIsOptimizitaion: true, pIsSumAsCriteria: false)
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
            string excpectedMotif4 = "cgtca";
            string excpectedMotif5 = "cgtga";
            string excpectedMotif6 = "cgtta";
            string expectedSolutionStartPosition1 = "0,4,0";
            string expectedSolutionStartPosition2 = "3,5,1";
            string expectedSolutionStartPosition3 = "5,0,3";
            int expectedResult = 1;

            char[][] charSets = new char[][] {
                "agttgcgtaa".ToArray(),
                "cgtgaagtgt".ToArray(),
                "aatcgtaacc".ToArray()
            };
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            int substringLength = 5;
            RegulatoryMotifsPatternEnumeration enumeration = new RegulatoryMotifsPatternEnumeration(alphabet, charSets, substringLength, pIsAllResult: true, pIsOptimizitaion: true, pIsSumAsCriteria: false)
            {
                StatisticAccumulator = new FakeRegulatoryMotifsStatisticAccumulator()
            };
            // act
            enumeration.Execute();
            // assert
            List<string> solutionStartPositions = enumeration.SolutionStartPositionList.Select(s => string.Join(",", s)).ToList();
            List<string> motifsAsString = enumeration.ListOfMotif.Select(m => string.Join("", m)).ToList();
            Assert.IsTrue(motifsAsString.Any(m => m == excpectedMotif1), $"Motif1 is absent.");
            Assert.IsTrue(motifsAsString.Any(m => m == excpectedMotif2), $"Motif2 is absent.");
            Assert.IsTrue(motifsAsString.Any(m => m == excpectedMotif3), $"Motif3 is absent.");
            Assert.IsTrue(motifsAsString.Any(m => m == excpectedMotif4), $"Motif3 is absent.");
            Assert.IsTrue(motifsAsString.Any(m => m == excpectedMotif5), $"Motif3 is absent.");
            Assert.IsTrue(motifsAsString.Any(m => m == excpectedMotif6), $"Motif3 is absent.");
            Assert.IsTrue(solutionStartPositions.Any(p => p == expectedSolutionStartPosition1), $"Positions1 is absent.");
            Assert.IsTrue(solutionStartPositions.Any(p => p == expectedSolutionStartPosition2), $"Positions2 is absent.");
            Assert.IsTrue(solutionStartPositions.Any(p => p == expectedSolutionStartPosition3), $"Positions3 is absent.");
            Assert.AreEqual(enumeration.OptimalValue, expectedResult, $"Result is wrong.");
            Assert.AreEqual(enumeration.SolutionStartPositionList.Count, 6, $"Wrong number of solutions.");

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
            RegulatoryMotifsPatternEnumeration enumeration = new RegulatoryMotifsPatternEnumeration(alphabet, charSets, substringLength, pIsAllResult: true, pIsOptimizitaion: true, pIsSumAsCriteria: true)
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
            RegulatoryMotifsPatternEnumeration enumeration = new RegulatoryMotifsPatternEnumeration(alphabet, charSets, substringLength, pIsAllResult: false, pIsOptimizitaion: true, pIsSumAsCriteria: true)
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
            RegulatoryMotifsPatternEnumeration enumeration = new RegulatoryMotifsPatternEnumeration(alphabet, charSets, substringLength, pIsAllResult: true, pIsOptimizitaion: false, pIsSumAsCriteria: false, pAcceptibleDistance: 0)
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
            string excpectedMotif2 = "cgtac";
            string excpectedMotif3 = "cgtga";
            string expectedSolutionStartPosition1 = "3,5,1";
            string expectedSolutionStartPosition2 = "5,0,3";

            char[][] charSets = new char[][] {
                "agtagcgtaa".ToArray(),
                "cgtgcagtgt".ToArray(),
                "aatcgtaacc".ToArray()};
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            int substringLength = 5;
            RegulatoryMotifsPatternEnumeration enumeration = new RegulatoryMotifsPatternEnumeration(alphabet, charSets, substringLength, pIsAllResult: true, pIsOptimizitaion: false, pIsSumAsCriteria: false, pAcceptibleDistance: 1)
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
            Assert.IsTrue(motifsAsString.Any(m => m == excpectedMotif2), $"Motif2 is absent.");
            Assert.IsTrue(motifsAsString.Any(m => m == excpectedMotif3), $"Motif3 is absent.");
            Assert.IsTrue(solutionStartPositions.Any(p => p == expectedSolutionStartPosition1), $"Positions1 is absent.");
            Assert.IsTrue(solutionStartPositions.Any(p => p == expectedSolutionStartPosition2), $"Positions2 is absent.");
            Assert.AreEqual(enumeration.SolutionStartPositionList.Count, 3, $"Wrong number of solutions.");
            Assert.AreEqual(enumeration.ListOfMotif.Count, 3, $"Wrong number of motifs.");

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
            RegulatoryMotifsPatternEnumeration enumeration = new RegulatoryMotifsPatternEnumeration(alphabet, charSets, substringLength, pIsAllResult: true, pIsOptimizitaion: false, pIsSumAsCriteria: false, pAcceptibleDistance: 2)
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
            RegulatoryMotifsPatternEnumeration enumeration = new RegulatoryMotifsPatternEnumeration(alphabet, charSets, substringLength, pIsAllResult: true, pIsOptimizitaion: false, pIsSumAsCriteria: true, pAcceptibleDistance: 2)
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
        // multiple testing
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ProcessInputDataTestCaseLength5Number3Pattern3()
        {
            // arrange
            char[] alphabet = new char[] { 'a', 'c' };
            EnumerateCharSetForMotifsPattern enumeration = new EnumerateCharSetForMotifsPattern(alphabet, 7, 3,
                3, pIsAllResult: false, pIsOptimizitaion: false, pIsSumAsCriteria: false, pAcceptibleDistance: 0);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ProcessInputDataTestCaseLength7Number3Pattern2()
        {
            // arrange
            char[] alphabet = new char[] { 'a', 'c', 'g' };
            EnumerateCharSetForMotifsPattern enumeration = new EnumerateCharSetForMotifsPattern(alphabet, 4, 3,
                3, pIsAllResult: false, pIsOptimizitaion: false, pIsSumAsCriteria: false, pAcceptibleDistance: 0);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ProcessInputDataTestCaseLength6Number2Pattern4Step987()
        {
            // arrange
            char[] alphabet = new char[] { 'a', 'c', 'g' };
            EnumerateCharSetForMotifsPattern enumeration = new EnumerateCharSetForMotifsPattern(alphabet, 6, 2,
                4, pIsAllResult: false, pIsOptimizitaion: false, pIsSumAsCriteria: false,
                pAcceptibleDistance: 0, pStep: 10);
            // act
            enumeration.Execute();
            // assert

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void ProcessInputDataBuAdditionTestLength7Number3Pattern4Step987()
        {
            // arrange
            int patternLength = 4;
            int acceptibleDistance = 0;
            bool isOptimizitaion = false;
            bool isSumAsCriteria = false;
            bool isAllResult = true;
            int step = 98798111;
            int numberOfSequence = 3;
            int sequenceLength = 7;
            var sequenceLengthes = string.Join(",", Enumerable.Repeat(sequenceLength, numberOfSequence));
            var statisticAccumulator = new RegulatoryMotifsStatisticAccumulator(new RegulatoryMotifSaver(), patternLength, sequenceLengthes,
                isOptimizitaion, isSumAsCriteria, isAllResult, acceptibleDistance, bufferSize: 1000);
            statisticAccumulator.Delete("RegulatoryMotifsPatternEnumeration");
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            long max = 1L << 42; //Convert.ToInt64(Math.Pow(4,21));
            long sequenceAsNumber = 0;
            int size = numberOfSequence * sequenceLength;
            int[] sequence = new int[size];
            char[] charSequence = new char[size];
            long[] masks = new long[size];

            long mask = 3;
            for (int i = 0; i < size; i++)
            {
                masks[i] = mask;
                mask <<= 2;
            }
            // act
            while (sequenceAsNumber < max)
            {
                int shift = 0;
                for (int i = 0; i < size; i++)
                {
                    sequence[i] = (int)((sequenceAsNumber & masks[i]) >> shift);
                    shift += 2;
                }
                sequenceAsNumber += step;
                charSequence = sequence.Select(j => alphabet[j]).ToArray();
                char[][] charSets = Enumerable.Range(0, numberOfSequence).Select(i => Enumerable.Range(0, sequenceLength).Select(j => charSequence[j * numberOfSequence + i]).ToArray()).ToArray();
                RegulatoryMotifsPatternEnumeration enumeration = new RegulatoryMotifsPatternEnumeration(alphabet, charSets, patternLength, pIsAllResult: false, pIsOptimizitaion: isOptimizitaion, pIsSumAsCriteria: isSumAsCriteria, pAcceptibleDistance: acceptibleDistance)
                {
                    StatisticAccumulator = statisticAccumulator
                };
                // act
                enumeration.Execute();
            }

            // assert

        }
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
    // class EnumerateCharSetForMotifsSubSequences 
    //--------------------------------------------------------------------------------------
    public class EnumerateCharSetForMotifsPattern : EnumerateIntegerCharSet
    {
        protected int _acceptibleDistance;
        protected bool _isAllResult;
        protected bool _isOptimizitaion;
        protected bool _isSumAsCriteria;
        protected int _sequenceLength;
        protected int _numberOfSequence;
        protected int _patternLength;
        protected int _step;
        protected int _stepCounter;
        protected RegulatoryMotifsStatisticAccumulator _statisticAccumulator { get; set; }
        //--------------------------------------------------------------------------------------
        public EnumerateCharSetForMotifsPattern(char[] pCharSet, int pSequenceLength,
            int pNumberOfSequence, int pPatternLength, bool pIsAllResult,
            bool pIsOptimizitaion = false, bool pIsSumAsCriteria = false,
            int pAcceptibleDistance = 0, int pStep = 1)
            : base(pCharSet, pSequenceLength * pNumberOfSequence, 0)
        {
            _patternLength = pPatternLength;
            _acceptibleDistance = pAcceptibleDistance;
            _isOptimizitaion = pIsOptimizitaion;
            _isSumAsCriteria = pIsSumAsCriteria;
            _isAllResult = pIsAllResult;
            _step = pStep;
            _stepCounter = 1;
            _numberOfSequence = pNumberOfSequence;
            _sequenceLength = pSequenceLength;
            var sequenceLengthes = string.Join(",", Enumerable.Repeat(_sequenceLength, _numberOfSequence));
            _statisticAccumulator = new RegulatoryMotifsStatisticAccumulator(new RegulatoryMotifSaver(), _patternLength, sequenceLengthes,
                _isOptimizitaion, _isSumAsCriteria, _isAllResult, _acceptibleDistance, bufferSize: 1000);
            _statisticAccumulator.Delete("RegulatoryMotifsPatternEnumeration");
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1 && --_stepCounter == 0)
            {
                var currentSequence = _fCurrentSet.Select(i => _charSet[i]).ToList();
                char[][] charSets_ = Enumerable.Range(0, _numberOfSequence).Select(i => currentSequence.Skip(i * _sequenceLength).Take(_sequenceLength).ToArray()).ToArray();
                int[][] indecies = Enumerable.Range(0, _numberOfSequence).Select(i => Enumerable.Range(0, _sequenceLength).Select(j => j * _numberOfSequence + i).ToArray()).ToArray();
                char[][] charSets = Enumerable.Range(0, _numberOfSequence).Select(i => Enumerable.Range(0, _sequenceLength).Select(j => currentSequence[j * _numberOfSequence + i]).ToArray()).ToArray();
                RegulatoryMotifsPatternEnumeration enumeration = new RegulatoryMotifsPatternEnumeration(_charSet, charSets, _patternLength, pIsAllResult: false, pIsOptimizitaion: _isOptimizitaion, pIsSumAsCriteria: _isSumAsCriteria, pAcceptibleDistance: _acceptibleDistance)
                {
                    StatisticAccumulator = _statisticAccumulator
                };
                // act
                enumeration.Execute();
                // assert

                //                _result.Add(string.Join(",", _fCurrentSet.Select(t => t.ToString())));
                _stepCounter = _step;
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
