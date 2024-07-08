using CommonLibrary.Helpers;
using CommonLibrary;
using RepresentativesSet;
using System.Linq;

namespace RepresentativesSetTest.Base
{

    //--------------------------------------------------------------------------------------
    // class EnumerateRepresentativesSimpleStepTestBase
    //--------------------------------------------------------------------------------------
    public abstract class EnumerateRepresentativesSimpleStepTestBase : EnumerateIntegerTrangle
    {
        protected int _fCardinality;
        protected long _maxCount;
        protected int _stepCounter = 0;
        protected int _gapCount = 0;
        public int GapCount
        {
            get
            {
                return _gapCount;
            }
        }
        //--------------------------------------------------------------------------------------
        protected long _step;
        public long Step
        {
            get
            {
                return _step;
            }
        }
        //--------------------------------------------------------------------------------------
        protected int _oneCount = 0;
        public int OneCount
        {
            get
            {
                return _oneCount;
            }
        }
        //--------------------------------------------------------------------------------------
        protected long _count = 0;
        public long Count
        {
            get
            {
                return _count;
            }
        }
        //--------------------------------------------------------------------------------------
        protected int _wrongResultCount = 0;
        public int WrongResultCount
        {
            get
            {
                return _wrongResultCount;
            }
        }
        //--------------------------------------------------------------------------------------
        public double WrongRelation
        {
            get
            {
                return 1.0 * _wrongResultCount / _count;
            }
        }
        //--------------------------------------------------------------------------------------
        public EnumerateRepresentativesSimpleStepTestBase(int pCardinality, int pLength, long maxCount, int pMinimumValue = 1, int pForwardAdditive = 1)
            : base((1 << pCardinality) - 1, pLength, pMinimumValue, pForwardAdditive)
        {
            _fBreakElement = 0;
            _fCardinality = pCardinality;
            _maxCount = maxCount;
            long number = Combinatorics.Combination(_fLimit, _fSize);
            _step = (int)(number / _maxCount);
        }
        //--------------------------------------------------------------------------------------
        protected abstract void ActAction(int[][] listOfSet);
        //--------------------------------------------------------------------------------------
        protected abstract void AssertAction();
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                // arrange
                _count++;

                _stepCounter++;
                if (_stepCounter >= _step)
                {
                    int[][] listOfSet = _fCurrentSet.Select(t => BruteForceRepresentativesBinaryNumbders.GetAsElementNumbers(t, _fCardinality).ToArray()).ToArray();
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
                    _stepCounter = 0;
                    // act
                    ActAction(listOfSet);
                    // assert
                    AssertAction();
                }
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
        protected override int FirstElement(int pPosition)
        {
            if (pPosition == 0)
                return 3;
            int first = _fCurrentSet[pPosition - 1] + _forwardAdditive;
            while (BruteForceRepresentativesBinaryNumbders.DefineSumOfBitVer2(first) == 1 && (first < _fLimit))
                first += _forwardAdditive;
            return first;
        }
        //--------------------------------------------------------------------------------------
        protected override bool NextElement(int pPosition)
        {
            if (_fCurrentSet[pPosition] >= _fLimit)
                return false;
            if (BruteForceRepresentativesBinaryNumbders.DefineSumOfBitVer2(_fCurrentSet[pPosition]) == 1)
                return false;
            _fCurrentSet[pPosition]++;
            if (BruteForceRepresentativesBinaryNumbders.DefineSumOfBitVer2(_fCurrentSet[pPosition]) == 1)
                _fCurrentSet[pPosition]++;
            return true;
        }
        //--------------------------------------------------------------------------------------
    }
}
