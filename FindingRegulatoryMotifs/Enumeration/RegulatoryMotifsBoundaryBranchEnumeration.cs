using BaseContract;
using CommonLibrary;
using CommonLibrary.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindingRegulatoryMotifs.Enumeration
{
    //--------------------------------------------------------------------------------------
    // class RegulatoryMotifsBoundaryBranchEnumeration 
    //--------------------------------------------------------------------------------------
    public class RegulatoryMotifsBoundaryBranchEnumeration : EnumerateIntegerCharSet
    {
        // parameters
        protected bool _isOptimizitaion;
        protected bool _isSumAsCriteria;
        protected bool _isAllResult;
        // input 
        protected int _acceptibleDistance;
        protected char[][] _sequenceLIst;
        protected int _patternLength;
        protected int _sequenceLIstLength;
        // working variables
        protected char[] _candidateMotif = null;
        protected int[] _positionInSequence;
        protected int _currentDistance;
        //results
        protected int _currentBestValue;
        protected int[] _currentBestValueList;
        protected List<char> _motif = null;
        protected List<List<char>> _listOfMotif = new List<List<char>>();
        protected int[] _solutionStartPosition;
        protected List<int[]> _solutionStartPositionList = new List<int[]>();

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
        public IRegulatoryMotifsStatisticAccumulator StatisticAccumulator { get; set; }
        //--------------------------------------------------------------------------------------
        public RegulatoryMotifsBoundaryBranchEnumeration(char[] pCharSet, char[][] pSequenceLIst, int pPatternLength, bool pIsAllResult = true, bool pIsOptimizitaion = false, bool pIsSumAsCriteria = false, int pAcceptibleDistance = 0)
            : base(pCharSet, pPatternLength, 0)
        {
            _sequenceLIst = pSequenceLIst;
            _positionInSequence = new int[_sequenceLIstLength];
            _currentBestValueList = new int[_sequenceLIstLength];

            _patternLength = pPatternLength;
            _candidateMotif = new char[_patternLength];
            _acceptibleDistance = pAcceptibleDistance;
            _isOptimizitaion = pIsOptimizitaion;
            _isSumAsCriteria = pIsSumAsCriteria;
            _isAllResult = pIsAllResult;
            _currentBestValue = int.MaxValue;
        }
        //--------------------------------------------------------------------------------------
        protected override bool IsCompleteCondition()
        {
            this.StatisticAccumulator.IterationCountInc();
            if (_fCurrentPosition == 0)
                return false;
            if (_fCurrentSet[0] > 0 || !ChaeckCurrentPart())
            {
                StatisticAccumulator.TerminalCountInc();
                return true;
            }
            if (_fCurrentPosition >= _fSize - 1)
            {
                if (!_isOptimizitaion)
                {
                    _motif = _candidateMotif.ToList();
                    _listOfMotif.Add(_motif);
                    _solutionStartPosition = _fCurrentSet.ToArray();
                    _solutionStartPositionList.Add(_solutionStartPosition);
                    if (!_isAllResult)
                        StatisticAccumulator.TerminalCountInc();
                    return !_isAllResult;
                }
                else
                {
                    if (_currentDistance < _currentBestValue)
                    {
                        _currentBestValue = _currentDistance;
                        _motif = _fCurrentSet.Select(i => _charSet[i]).ToList();
                        _listOfMotif.Clear();
                        _listOfMotif.Add(_motif);
                        _solutionStartPosition = _positionInSequence.ToArray();
                        _solutionStartPositionList.Clear();
                        _solutionStartPositionList.Add(_solutionStartPosition);
                        StatisticAccumulator.UpdateOptcountInc();
                        StatisticAccumulator.AddRegulatoryMotifOptimalValueChange(stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds,
                        _currentBestValue, string.Join(",", _solutionStartPosition.Select(s => s.ToString())), string.Join(",", _motif.Select(s => s.ToString())));
                    }
                    else if (_isAllResult && _currentDistance == _currentBestValue)
                    {
                        _listOfMotif.Add(_fCurrentSet.Select(i => _charSet[i]).ToList());
                        _solutionStartPositionList.Add(_positionInSequence.ToArray());
                    }
                    if (!_isAllResult && _currentBestValue == 0)
                        StatisticAccumulator.TerminalCountInc();
                    return !_isAllResult && _currentBestValue == 0;
                }
            }
            else if (_fCurrentSet[_fCurrentPosition] > _fLimit)
            {
                StatisticAccumulator.TerminalCountInc();
                return true;
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
        private bool ChaeckCurrentPart()
        {
            _currentBestValueList[_fCurrentPosition] = DefineBestSubstringAndDistance(_fCurrentPosition);
            _currentDistance = 0;
            if (!_isSumAsCriteria)
                _currentDistance = Enumerable.Range(0, _sequenceLIstLength).Max(i => DefineBestSubstringAndDistance(i));
            else
                _currentDistance = Enumerable.Range(0, _sequenceLIstLength).Sum(i => DefineBestSubstringAndDistance(i));
            if (!_isOptimizitaion)
            {
                if (_currentDistance > _acceptibleDistance)
                {
                    StatisticAccumulator.ElemenationCountInc();
                    return false;
                }
            }
            else
            {
                if (_currentDistance > _currentBestValue || _isAllResult && _currentDistance == _currentBestValue)
                {
                    StatisticAccumulator.ElemenationCountInc();
                    return false;
                }
            }

            return true;
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                //int currentDistance = 0;
                //if (!_isSumAsCriteria)
                //    currentDistance = Enumerable.Range(0, _sequenceLIstLength).Max(i => DefineBestSubstringAndDistance(i));
                //else
                //    currentDistance = Enumerable.Range(0, _sequenceLIstLength).Sum(i => DefineBestSubstringAndDistance(i));
                if (!_isOptimizitaion)
                {
                    if (_currentDistance <= _acceptibleDistance)
                    {
                        _motif = _candidateMotif.ToList();
                        _listOfMotif.Add(_motif);
                        _solutionStartPosition = _fCurrentSet.ToArray();
                        _solutionStartPositionList.Add(_solutionStartPosition);
                        StatisticAccumulator.UpdateOptcountInc();
                        StatisticAccumulator.AddRegulatoryMotifOptimalValueChange(stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds,
                        _currentBestValue, string.Join(",", _solutionStartPosition.Select(s => s.ToString())), string.Join(",", _motif.Select(s => s.ToString())));
                        return !_isAllResult;
                    }
                }
                else
                {
                    if (_currentDistance < _currentBestValue)
                    {
                        _currentBestValue = _currentDistance;
                        _motif = _fCurrentSet.Select(i => _charSet[i]).ToList();
                        _listOfMotif.Clear();
                        _listOfMotif.Add(_motif);
                        _solutionStartPosition = _positionInSequence.ToArray();
                        _solutionStartPositionList.Clear();
                        _solutionStartPositionList.Add(_solutionStartPosition);
                        StatisticAccumulator.UpdateOptcountInc();
                        StatisticAccumulator.AddRegulatoryMotifOptimalValueChange(stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds,
                        _currentBestValue, string.Join(",", _solutionStartPosition.Select(s => s.ToString())), string.Join(",", _motif.Select(s => s.ToString())));
                    }
                    else if (_isAllResult && _currentDistance == _currentBestValue)
                    {
                        _listOfMotif.Add(_fCurrentSet.Select(i => _charSet[i]).ToList());
                        _solutionStartPositionList.Add(_positionInSequence.ToArray());
                        StatisticAccumulator.UpdateOptcountInc();
                        StatisticAccumulator.AddRegulatoryMotifOptimalValueChange(stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds,
                       _currentBestValue, string.Join(",", _solutionStartPosition.Select(s => s.ToString())), string.Join(",", _motif.Select(s => s.ToString())));
                    }
                    return !_isAllResult && _currentBestValue == 0;
                }
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
        private int DefineBestSubstringAndDistance(int pNumberSequence)
        {
            int bestDistance = int.MaxValue;
            int bestPosition = 0;
            for (int i = 0; i <= _sequenceLIst[pNumberSequence].Length - _patternLength; i++)
            {
                int distance = 0;
                for (int j = 0; j <= _fCurrentPosition; j++)
                {
                    char curChar = _sequenceLIst[pNumberSequence][i + j];
                    if (_charSet[_fCurrentSet[j]] != curChar)
                        distance++;

                }
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestPosition = i;
                }
            }
            _positionInSequence[pNumberSequence] = bestPosition;
            _currentBestValueList[pNumberSequence] = bestDistance;
            return bestDistance;
        }
        //-----------------------------------------------------------------------------------
        protected override void SupplementInitial()
        {
            StatisticAccumulator.CreateStatistics(_fSize, string.Join(",", _sequenceLIst.Select(s => new string(s))), "RegulatoryMotifsBoundaryBranchEnumeration", _sequenceLIst.Length,
                string.Join(",", _sequenceLIst.Select(s => s.Length)), _patternLength, new AlgorythmParameters() { });
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
    //--------------------------------------------------------------------------------------
}
