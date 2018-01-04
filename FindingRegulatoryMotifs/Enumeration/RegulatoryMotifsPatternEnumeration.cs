using BaseContract;
using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindingRegulatoryMotifs.Enumeration
{
    //--------------------------------------------------------------------------------------
    // class RegulatoryMotifsPatternEnumeration 
    //--------------------------------------------------------------------------------------
    public class RegulatoryMotifsPatternEnumeration : EnumerateIntegerCharSet
    {
        protected int _acceptibleDistance;
        protected List<char> _motif = null;
        protected List<List<char>> _listOfMotif = new List<List<char>>();
        protected char[] _candidateMotif = null;
        protected int _currentBestValue;
        protected int[] _solutionStartPosition;
        protected List<int[]> _solutionStartPositionList;
        protected bool _isOptimizitaion;
        protected bool _isSumAsCriteria;
        protected bool _isAllResult;
        protected char[][] _sequenceLIst;
        protected int _patternLength;
        protected int[] _positionInSequence;
        public IRegulatoryMotifsStatisticAccumulator StatisticAccumulator { get; set; }
        //--------------------------------------------------------------------------------------
        public RegulatoryMotifsPatternEnumeration(char[] pCharSet, char[][] pSequenceLIst, int pPatternLength, bool pIsAllResult = true, bool pIsOptimizitaion = false, bool pIsSumAsCriteria = false, int pAcceptibleDistance = 0)
            : base(pCharSet, pPatternLength, 0)
        {
            _sequenceLIst = pSequenceLIst;
            _positionInSequence = new int[_fSize];

            _patternLength = pPatternLength;
            _candidateMotif = new char[_patternLength];
            _acceptibleDistance = pAcceptibleDistance;
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
                int currentDistance = 0;
                if (!_isSumAsCriteria)
                    currentDistance = Enumerable.Range(0, _fSize - 1).Max(i => DefineBestSubstringAndDistance(i));
                else
                    currentDistance = Enumerable.Range(0, _fSize - 1).Sum(i => DefineBestSubstringAndDistance(i));
                if (!_isOptimizitaion)
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
                        _motif = _fCurrentSet.Select( i => _charSet[i]).ToList();
                        _listOfMotif.Clear();
                        _listOfMotif.Add(_motif);
                        _solutionStartPosition = _positionInSequence.ToArray();
                        _solutionStartPositionList.Clear();
                        _solutionStartPositionList.Add(_solutionStartPosition);
                        StatisticAccumulator.AddRegulatoryMotifOptimalValueChange(stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds,
                        _currentBestValue, string.Join(",", _solutionStartPosition.Select(s => s.ToString())), string.Join(",", _motif.Select(s => s.ToString())));
                    }
                    if (_isAllResult && currentDistance == _currentBestValue)
                    {
                        StatisticAccumulator.UpdateOptcountInc();
                        _listOfMotif.Add(_fCurrentSet.Select(i => _charSet[i]).ToList());
                        _solutionStartPositionList.Add(_positionInSequence.ToArray());
                        StatisticAccumulator.AddRegulatoryMotifOptimalValueChange(stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds,
                       _currentBestValue, string.Join(",", _solutionStartPosition.Select(s => s.ToString())), string.Join(",", _motif.Select(s => s.ToString())));
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
        private int DefineBestSubstringAndDistance(int pNumberSequence)
        {
            int bestDistance = 0;
            int bestPosition = 0;
            for (int i = 0; i < _sequenceLIst[pNumberSequence].Length - _patternLength; i++)
            {
                int distance = 0;
                for (int j = 0; j < _fSize; j++)
                {
                    char curChar = _sequenceLIst[pNumberSequence][i + j];
                    if (_charSet[_fCurrentSet[i]] != curChar)
                        distance++;

                }
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestPosition = i;
                }
            }
            _positionInSequence[pNumberSequence] = bestPosition;
            return bestDistance;
        }
        //-----------------------------------------------------------------------------------
        protected override void SupplementInitial()
        {
            StatisticAccumulator.CreateStatistics(_fSize, string.Join(",", _sequenceLIst.Select(s => new string(s))), "", _sequenceLIst.Length,
                (int)Math.Round(_sequenceLIst.Select(s => s.Length).Average()), _patternLength);
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
                    return string.Join(",", _motif.Select(i => i.ToString()));
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
}
