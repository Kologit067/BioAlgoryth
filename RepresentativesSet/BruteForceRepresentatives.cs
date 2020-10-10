using System.Collections.Generic;
using System.Linq;

namespace RepresentativesSet
{
    //--------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------
    public class BruteForceRepresentatives
    {
        protected List<string> _fOptimalSets;		        // 
        //--------------------------------------------------------------------------------------
        public List<int> ExecuteByBinary(int[][] pListOfSet)
        {
            _fOptimalSets = new List<string>();
            int maxNumber = pListOfSet.Max(s => s.Max()) + 1;
            long limit = 1 << maxNumber;
            int[] listOfSetAsBinary = new int[pListOfSet.Length];
            for (int i = 0; i < listOfSetAsBinary.Length; i++)
            {
                foreach (int pos in pListOfSet[i])
                    listOfSetAsBinary[i] |= 1 << pos;
            }
            long currentMinimumSet = limit - 1;
            int currentMinimum = maxNumber;
            for (int i = 0; i < limit; i++)
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
                if (isIntersect)
                {
                    int candidatValue = DefineOnBit(i, limit, currentMinimum);
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
        protected virtual int DefineOnBit(int numberAsSet, long limit, int curMin)
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
