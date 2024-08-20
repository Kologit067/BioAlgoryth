using CommonLibrary.Helpers;
using RepresentativesSet;
using System.Linq;

namespace RepresentativesSetTest.Base
{
    //--------------------------------------------------------------------------------------
    // class EnumerateRepresentativesAdvStepTestBase
    //--------------------------------------------------------------------------------------
    public class EnumerateRepresentativesAdvStepTestBase
    {
        protected int _fLimit;
        protected int _fSize;
        protected int _fCardinality;
        protected int[] _fCurrentSet;
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
        protected int _wrongResultImpCount = 0;
        public int WrongResultImpCount
        {
            get
            {
                return _wrongResultImpCount;
            }
        }
        //--------------------------------------------------------------------------------------
        protected int _wrongResultImpRDCount = 0;
        public int WrongResultImpRDCount
        {
            get
            {
                return _wrongResultImpRDCount;
            }
        }
        //--------------------------------------------------------------------------------------
        protected int _gapCount = 0;
        public int GapCount
        {
            get
            {
                return _gapCount;
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
        protected int[][] GetAndTestListOfSet()
        {
            int[][] listOfSet = _fCurrentSet.Select(t => BruteForceRepresentativesBinaryNumbders.GetAsElementNumbers(t, _fCardinality).ToArray()).ToArray();
            int count = listOfSet.SelectMany(l => l).Distinct().Count();
            int max = listOfSet.SelectMany(l => l).Max();
            string listAsString = listOfSet.AsString();
            if (max + 1 != count)
            {
                _gapCount++;
                return null;
            }
            bool isAnyOne = listOfSet.Any(l => l.Count() == 1);
            if (isAnyOne)
            {
                _oneCount++;
                return null;
            }
            return listOfSet;
        }
    }
    //--------------------------------------------------------------------------------------
}
