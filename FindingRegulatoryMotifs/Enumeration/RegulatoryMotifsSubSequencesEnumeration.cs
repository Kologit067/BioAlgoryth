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
        protected bool _isAllResult;
        protected List<char> _motif = null;
        protected char[] _candidateMotif = null;
        protected List<List<char>> _listOfMotif = new List<List<char>>();
//        protected List<AlphabetData> _alphabetDatas;
        protected Dictionary<char,int> _alphabetDatas;
        protected char[] _alphabet;
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
        public RegulatoryMotifsSubSequencesEnumeration(char[][] pCharSets, char[] pAlphabet, int pSubstringLength, bool pIsAllResult = true, int pAcceptibleDistance = 0)
            : base(pCharSets, pSubstringLength, null)
        {
            _candidateMotif = new char[pSubstringLength];
            _acceptibleDistance = pAcceptibleDistance;
            _alphabet = pAlphabet;
            _alphabetDatas = _alphabet.ToDictionary(a => a, a => 0);
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (fCurrentPosition == _fSize - 1)
            {
                var pairwiseDifferencesForCurrentSet = DNAMappingBase.ProduceMatrix(fCurrentSet);
                if (_pairwiseDifferences.SequenceEqual(pairwiseDifferencesForCurrentSet.OrderBy(d => d)))
                {
                    if (_motif == null)
                        _motif = fCurrentSet.ToList();
                    _listOfMotif.Add(fCurrentSet.ToList());
                    return !_isAllResult;
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
                    char curChar = _charSets[j][fCurrentSet[fCurrentPosition] + i];
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
                char curChar = _charSets[pNumberSequence][fCurrentSet[fCurrentPosition] + i];
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
