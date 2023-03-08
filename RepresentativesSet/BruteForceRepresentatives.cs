using System;
using System.Collections.Generic;
using System.Linq;

namespace RepresentativesSet
{
    //--------------------------------------------------------------------------------------
    // class BruteForceRepresentatives
    //--------------------------------------------------------------------------------------
    public class BruteForceRepresentatives
    {
        int[][] jaggedArray2 = {
    new int[] { 1, 3, 5, 7, 9 },
    new int[] { 0, 2, 4, 6 },
    new int[] { 11, 22 }
};

        protected List<string> _fOptimalSets;		        // 
        //--------------------------------------------------------------------------------------
        // pListOfSubSet - subsets is presented as list of numbers of element of Set that included into subset
        public List<int> ExecuteByBinary(int[][] pListOfSubSet)
        {
            _fOptimalSets = new List<string>();
            int maxNumber = pListOfSubSet.Max(s => s.Max()) + 1;

            long[] listOfSetAsBinary = pListOfSubSet.Select(s => BruteForceRepresentatives.ElementNumbersToLongAsBinaryVector(s)).ToArray();

            return ExecuteByLongAsBinaryVector(listOfSetAsBinary, maxNumber);
        }
        //--------------------------------------------------------------------------------------
        public List<int> ExecuteByLongAsBinaryVector(long[] listOfSetAsBinary, int maxNumber)
        {
            return ExecuteByLongAsBinaryVectorGen(listOfSetAsBinary, maxNumber, (l, i) =>
            {
                bool isIntersect = true;
                for (int k = 0; k < listOfSetAsBinary.Length; k++)
                {
                    if ((listOfSetAsBinary[k] & i) == 0)
                    {
                        isIntersect = false;
                        break;
                    }
                }
                return isIntersect;
            });
        }
        //--------------------------------------------------------------------------------------
        public List<int> ExecuteByLongAsBinaryVectorVer2(long[] listOfSetAsBinary, int maxNumber)
        {
            return ExecuteByLongAsBinaryVectorGen(listOfSetAsBinary, maxNumber, (l,i) => l.All(s => (s & i) != 0));
        }
        //--------------------------------------------------------------------------------------
        private List<int> ExecuteByLongAsBinaryVectorGen(long[] listOfSetAsBinary, int maxNumber, Func<long[], int, bool> IsIntersect)
        {
            long limit = 1 << maxNumber;
            long currentMinimumSet = limit - 1;
            int currentMinimum = maxNumber;
            for (int i = 0; i < limit; i++)
            {
                bool isIntersect = IsIntersect(listOfSetAsBinary, i);
                if (isIntersect)
                {
                    int candidatValue = DefineSumOfBit(i, limit, currentMinimum);
                    if (candidatValue <= currentMinimum)
                    {
                        if (candidatValue < currentMinimum)
                        {
                            currentMinimum = candidatValue;
                            currentMinimumSet = i;
                            _fOptimalSets.Clear();
                        }
                        _fOptimalSets.Add(string.Join(",", GetAsElementNumbers(i, maxNumber)));
                    }
                }
            }
            List<int> result = GetAsElementNumbers(currentMinimumSet, maxNumber);
            return result;
        }
        //--------------------------------------------------------------------------------------
        // long type representation of binary vector --> array of number psition with value 1(true) (kind of subset representaion)
        public static List<int> GetAsElementNumbers(long currentMinimumSet,int maxNumber)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < maxNumber; i++)
            {
                if (((1 << i) & currentMinimumSet) != 0)
                    result.Add(i);
            }
            return result;
        }
        //--------------------------------------------------------------------------------------
        // array of number psition with value 1(true) (kind of subset representaion) --> long type representation of binary vector
        public static long ElementNumbersToLongAsBinaryVector(int[] numberElements)
        {
            long result = 0;
            foreach (int pos in numberElements)
                result |= 1 << pos;
            return result;
        }        
        //--------------------------------------------------------------------------------------
        public static int DefineSumOfBit(int numberAsSet, long limit, int curMin)
        {
            int sum = 0;
            int pos = 1;
            while (pos <= limit && sum <= curMin)
            {
                if ((numberAsSet & pos) != 0)
                    sum++;
                pos <<= 1;
            }
            return sum;
        }
        //--------------------------------------------------------------------------------------
        public static long DefineSumOfBitVer2(long numberAsSet)
        {
            long result = (numberAsSet & 0x5555555555555555) + ((numberAsSet >> 1) & 0x5555555555555555);
            result = (result & 0x3333333333333333) + ((result >> 2) & 0x3333333333333333);
            result = (result & 0x0F0F0F0F0F0F0F0F) + ((result >> 4) & 0x0F0F0F0F0F0F0F0F);
            result = (result & 0x00FF00FF00FF00FF) + ((result >> 8) & 0x00FF00FF00FF00FF);
            result = (result & 0x0000FFFF0000FFFF) + ((result >> 16) & 0x0000FFFF0000FFFF);
            result = (result & 0x00000000FFFFFFFF) + ((result >> 32) & 0x00000000FFFFFFFF);

            return result;
        }        
        //--------------------------------------------------------------------------------------
        public List<string> OptimalSets
        {
            get
            {
                return _fOptimalSets;
            }
            set
            {
                _fOptimalSets = value;
            }
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
