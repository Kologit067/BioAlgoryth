using CommonLibrary;
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
        protected char[] _alphabet;
        protected int _currentBestValue;
        protected int[] _solutionStartPosition;
        protected List<int[]> _solutionStartPositionList;
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
        public RegulatoryMotifsSubSequencesEnumeration(char[][] pCharSets, char[] pAlphabet, int pSubstringLength, bool pIsAllResult = true, bool pIsOptimizitaion = false, bool pIsSumAsCriteria = false, int pAcceptibleDistance = 0)
            : base(pCharSets, pSubstringLength, null)
        {
            _candidateMotif = new char[pSubstringLength];
            _acceptibleDistance = pAcceptibleDistance;
            _alphabet = pAlphabet;
            _alphabetDatas = _alphabet.ToDictionary(a => a, a => 0);
            _isOptimizitaion = pIsOptimizitaion;
            _isSumAsCriteria = pIsSumAsCriteria;
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
                    currentDistance = Enumerable.Range(0, _fSize - 1).Max(i => DefineLocalDistance(i));
                else
                    currentDistance = Enumerable.Range(0, _fSize - 1).Sum(i => DefineLocalDistance(i));
                if ( !_isOptimizitaion)
                {
                    if (currentDistance <= _acceptibleDistance)
                    {
                        _motif = _candidateMotif.ToList();
                        _listOfMotif.Add(_motif);
                        _solutionStartPosition = _fCurrentSet.ToArray();
                        _solutionStartPositionList.Add(_solutionStartPosition);
                        return !_isAllResult;
                    }
                }
                else
                {
                    if (currentDistance < _currentBestValue)
                    {
                        _currentBestValue = currentDistance;
                        _motif = _candidateMotif.ToList();
                        _listOfMotif.Clear();
                        _listOfMotif.Add(_motif);
                        _solutionStartPosition = _fCurrentSet.ToArray();
                        _solutionStartPositionList.Clear();
                        _solutionStartPositionList.Add(_solutionStartPosition);
                    }
                    if (_isAllResult && currentDistance == _currentBestValue)
                    {
                        _listOfMotif.Add(_candidateMotif.ToList());
                        _solutionStartPositionList.Add(_fCurrentSet.ToArray());
                    }
                    return !_isAllResult && _currentBestValue == 0;
                }
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
        private void CalculateCandidateMotif()
        {
            for (int i = 0; i < _substringLength; i++)
            {
                Parallel.ForEach(_alphabetDatas.Keys, k => {
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
        //--------------------------------------------------------------------------------------
    }

    public class AlphabetData
    {
        public char Data { get; set; }
        public int Count { get; set; }
    }

}
