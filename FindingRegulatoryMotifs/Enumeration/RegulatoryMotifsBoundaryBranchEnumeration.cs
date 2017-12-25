using CommonLibrary;
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
        protected int _acceptibleDistance;
        protected List<char> _motif = null;
        protected List<List<char>> _listOfMotif = new List<List<char>>();
        protected char[] _candidateMotif = null;
        protected int _currentBestValue;
        protected int[] _currentBestValueList;
        protected int[] _solutionStartPosition;
        protected List<int[]> _solutionStartPositionList;
        protected int[] _positionInSequence;
        protected bool _isOptimizitaion;
        protected bool _isSumAsCriteria;
        protected bool _isAllResult;
        protected char[][] _sequenceLIst;
        protected int _patternLength;
        protected int _currentDistance;
        //--------------------------------------------------------------------------------------
        public RegulatoryMotifsBoundaryBranchEnumeration(char[] pCharSet, char[][] pSequenceLIst, int pPatternLength, bool pIsAllResult = true, bool pIsOptimizitaion = false, bool pIsSumAsCriteria = false, int pAcceptibleDistance = 0)
            : base(pCharSet, pPatternLength, 0)
        {
            _sequenceLIst = pSequenceLIst;
            _positionInSequence = new int[_fSize];
            _currentBestValueList = new int[_fSize];

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
            if (fCurrentPosition == 0)
                return false;
            if (fCurrentSet[0] > 0 || !ChaeckCurrentPart())
                return true;
            if (fCurrentPosition >= _fSize - 1)
            {
                if (!_isOptimizitaion)
                {
                        _motif = _candidateMotif.ToList();
                        _listOfMotif.Add(_motif);
                        _solutionStartPosition = fCurrentSet.ToArray();
                        _solutionStartPositionList.Add(_solutionStartPosition);
                        return !_isAllResult;
                }
                else
                {
                    if (_currentDistance < _currentBestValue)
                    {
                        _currentBestValue = _currentDistance;
                        _motif = fCurrentSet.Select(i => _charSet[i]).ToList();
                        _listOfMotif.Clear();
                        _listOfMotif.Add(_motif);
                        _solutionStartPosition = _positionInSequence.ToArray();
                        _solutionStartPositionList.Clear();
                        _solutionStartPositionList.Add(_solutionStartPosition);
                    }
                    if (_isAllResult && _currentDistance == _currentBestValue)
                    {
                        _listOfMotif.Add(fCurrentSet.Select(i => _charSet[i]).ToList());
                        _solutionStartPositionList.Add(_positionInSequence.ToArray());
                    }
                    return !_isAllResult && _currentBestValue == 0;
                }
            }
            else if (fCurrentSet[fCurrentPosition] > _fLimit)
                return true;
            return false;
        }
        //--------------------------------------------------------------------------------------
        private bool ChaeckCurrentPart()
        {
            _currentBestValueList[fCurrentPosition] = DefineBestSubstringAndDistance(fCurrentPosition);
            _currentDistance = 0;
            if (!_isSumAsCriteria)
                _currentDistance = _currentBestValueList.Take(fCurrentPosition).Max();
            else
                _currentDistance = _currentBestValueList.Take(fCurrentPosition).Sum();
            if (!_isOptimizitaion)
            {
                if (_currentDistance > _acceptibleDistance)
                {
                    return false;
                }
            }
            else
            {
                if (_currentDistance > _currentBestValue)
                {
                    return false;
                }
                if (_isAllResult && _currentDistance == _currentBestValue)
                {
                    return false;
                }
            }

            return true;
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (fCurrentPosition == _fSize - 1)
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
                        _motif = _candidateMotif.ToList();
                        _listOfMotif.Add(_motif);
                        _solutionStartPosition = fCurrentSet.ToArray();
                        _solutionStartPositionList.Add(_solutionStartPosition);
                        return !_isAllResult;
                    }
                }
                else
                {
                    if (currentDistance < _currentBestValue)
                    {
                        _currentBestValue = currentDistance;
                        _motif = fCurrentSet.Select(i => _charSet[i]).ToList();
                        _listOfMotif.Clear();
                        _listOfMotif.Add(_motif);
                        _solutionStartPosition = _positionInSequence.ToArray();
                        _solutionStartPositionList.Clear();
                        _solutionStartPositionList.Add(_solutionStartPosition);
                    }
                    if (_isAllResult && currentDistance == _currentBestValue)
                    {
                        _listOfMotif.Add(fCurrentSet.Select(i => _charSet[i]).ToList());
                        _solutionStartPositionList.Add(_positionInSequence.ToArray());
                    }
                    return !_isAllResult && _currentBestValue == 0;
                }
            }
            return false;
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
                    if (_charSet[fCurrentSet[i]] != curChar)
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
        //--------------------------------------------------------------------------------------
    }
}
