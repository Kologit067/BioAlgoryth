using BaseContract;
using CommonLibrary;
using CommonLibrary.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindingRegulatoryMotifs.Enumeration
{
    //--------------------------------------------------------------------------------------
    // class RegulatoryMotifsSubSequencesEnumeration 
    //--------------------------------------------------------------------------------------
    // First/all
    // satisfiability/optimization
    // summa/worst
    public class RegulatoryMotifsSubSequencesEnumeration : EnumerateintegervariableSubsequence
    {
        protected int _acceptibleDistance;
        protected List<char> _motif = null;
        protected char[] _candidateMotif = null;
        protected List<List<char>> _listOfMotif = new List<List<char>>();
        protected Dictionary<char,int> _alphabetDatas;
//        protected List<char> _alphabetDatasKeys;
        protected char[] _alphabet;
        protected int _currentBestValue;
        protected int[] _solutionStartPosition;
        protected List<int[]> _solutionStartPositionList = new List<int[]>();
        protected bool _isOptimizitaion;
        protected bool _isSumAsCriteria;
        protected bool _isAllResult;
        //--------------------------------------------------------------------------------------
        public List<char> Motif
        {
            get
            {
                return _motif;
            }
        }
        //--------------------------------------------------------------------------------------
        public List<List<char>> ListOfMotif
        {
            get
            {
                return _listOfMotif;
            }
        }
        //--------------------------------------------------------------------------------------
        public List<int[]> SolutionStartPositionList
        {
            get
            {
                return _solutionStartPositionList;
            }
        }
        //--------------------------------------------------------------------------------------
        public int[] SolutionStartPosition
        {
            get
            {
                return _solutionStartPosition;
            }
        }
        //--------------------------------------------------------------------------------------
        public IRegulatoryMotifsStatisticAccumulator StatisticAccumulator { get; set; }
        //--------------------------------------------------------------------------------------
        public RegulatoryMotifsSubSequencesEnumeration(char[][] pCharSets, char[] pAlphabet, int pSubstringLength, bool pIsAllResult = true, bool pIsOptimizitaion = false, bool pIsSumAsCriteria = false, int pAcceptibleDistance = 0)
            : base(pCharSets, pSubstringLength, null)
        {
            _candidateMotif = new char[pSubstringLength];
            _acceptibleDistance = pAcceptibleDistance;
            _alphabet = pAlphabet;
            _alphabetDatas = _alphabet.ToDictionary(a => a, a => 0);
            _isOptimizitaion = pIsOptimizitaion;
            _isSumAsCriteria = pIsSumAsCriteria;
            _isAllResult = pIsAllResult;
            _currentBestValue = int.MaxValue;
        }
    //--------------------------------------------------------------------------------------
    protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                CalculateCandidateMotif();
                int currentDistance = 0;
                if (!_isSumAsCriteria)
                    currentDistance = Enumerable.Range(0, _fSize).Max(i => DefineLocalDistance(i));
                else
                    currentDistance = Enumerable.Range(0, _fSize).Sum(i => DefineLocalDistance(i));
                if ( !_isOptimizitaion)
                {
                    if (currentDistance <= _acceptibleDistance)
                    {
                        StatisticAccumulator.UpdateOptcountInc();
                        _motif = _candidateMotif.ToList();
                        _listOfMotif.Add(_motif);
                        _solutionStartPosition = _fCurrentSet.ToArray();
                        _solutionStartPositionList.Add(_solutionStartPosition);
                        StatisticAccumulator.AddRegulatoryMotifOptimalValueChange(stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds,
                        _currentBestValue, string.Join(",", _solutionStartPosition.Select(s => s.ToString())), string.Join(",", _motif.Select(s => s.ToString())));
                        return !_isAllResult;
                    }
                }
                else
                {
                    if (currentDistance < _currentBestValue)
                    {
                        StatisticAccumulator.UpdateOptcountInc();
                        _currentBestValue = currentDistance;
                        _motif = _candidateMotif.ToList();
                        _listOfMotif.Clear();
                        _listOfMotif.Add(_motif);
                        _solutionStartPosition = _fCurrentSet.ToArray();
                        _solutionStartPositionList.Clear();
                        _solutionStartPositionList.Add(_solutionStartPosition);
                        StatisticAccumulator.AddRegulatoryMotifOptimalValueChange(stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds,
                        _currentBestValue, string.Join(",", _solutionStartPosition.Select(s => s.ToString())), string.Join(",", _motif.Select(s => s.ToString())));
                    }
                    else if (_isAllResult && currentDistance == _currentBestValue)
                    {
                        StatisticAccumulator.UpdateOptcountInc();
                        _listOfMotif.Add(_candidateMotif.ToList());
                        _solutionStartPositionList.Add(_fCurrentSet.ToArray());
                    }
                    return !_isAllResult && _currentBestValue == 0;
                }
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
        protected override void IterationAction()
        {
            StatisticAccumulator.IterationCountInc();
        }
        //--------------------------------------------------------------------------------------
        protected override void TerminalAction()
        {
            StatisticAccumulator.TerminalCountInc();
        }
        //--------------------------------------------------------------------------------------
        private void CalculateCandidateMotif()
        {
            for (int i = 0; i < _substringLength; i++)
            {
                Parallel.ForEach(_alphabet, k => {
                    _alphabetDatas[k] = 0;
                });

                for (int j = 0; j < _fSize; j++)
                {
                    char curChar = _charSets[j][_fCurrentSet[j] + i];
                    _alphabetDatas[curChar]++;
                }
                var maxPair = _alphabetDatas.OrderByDescending(a => a.Value).First();
                _candidateMotif[i] = maxPair.Key;
            }

        }
        //--------------------------------------------------------------------------------------
        private int DefineLocalDistance(int pNumberSequence)
        {
            int distance = 0;
            for (int i = 0; i < _substringLength; i++)
            {
                char curChar = _charSets[pNumberSequence][_fCurrentSet[pNumberSequence] + i];
                if (_candidateMotif[i] != curChar)
                    distance++;
            }
            return distance;
        }
        //-----------------------------------------------------------------------------------
        protected override void SupplementInitial()
        {
            StatisticAccumulator.CreateStatistics(_fSize, string.Join(",", _charSets.Select(s => new string(s))), "RegulatoryMotifsSubSequencesEnumeration", _charSets.Length,
                string.Join(",",_charSets.Select(s => s.Length)), _substringLength, new AlgorythmParameters()
                {
                    IsOptimizitaion = _isOptimizitaion,
                    IsSumAsCriteria = _isSumAsCriteria,
                    IsAllResult = _isAllResult
                });
        }
        //-----------------------------------------------------------------------------------
        protected override void PostAction()
        {
            StatisticAccumulator.SaveStatisticData(OutputPresentation, _currentBestValue, ElapsedTicks, DurationMilliSeconds,
                DateTime.Now, IsComplete, CurrentSetAsString, OptimalRouteAsString, _listOfMotif, _solutionStartPositionList);
        }
        //-----------------------------------------------------------------------------------
        public override string OptimalRouteAsString
        {
            get
            {
                if (_fCurrentSet != null && _fCurrentSet.Count > 0)
                    return string.Join(",", _fCurrentSet.Select(i => i.ToString()));
                return "Empty";
            }
        }
        //-----------------------------------------------------------------------------------
        public override string OutputPresentation
        {
            get
            {
                if (_motif != null && _motif.Count > 0)
                    return string.Join("", _motif.Select(i => i.ToString()));
                return "Empty";
            }
        }

        //--------------------------------------------------------------------------------------
        public override int OptimalValue
        {
            get
            {
                return _currentBestValue;
            }
        }
        //--------------------------------------------------------------------------------------
    }

    public class AlphabetData
    {
        public char Data { get; set; }
        public int Count { get; set; }
    }

}
