using BaseContract;
using CommonLibrary;
using StatisticsStorage.Accumulators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepresentativesSet
{
    //--------------------------------------------------------------------------------------
    // class RepresentativesAsTree
    //--------------------------------------------------------------------------------------
    public class RepresentativesAsTree : EnumerateReverseBinVectors
    {
        protected int[][] listOfSet;
        protected long[] listOfSetAsNumber;
        protected int currentMinimum;
        protected List<int> _fCurrentOptimalSet;		    // текущий оптимальный набор элементов
        protected List<string> _fOptimalSets;               // 
        public int CurrentMinimum
        { 
            get 
            { 
                return currentMinimum; 
            } 
        }
        protected string _inputData;
        public string InputData
        {
            get
            {
                return _inputData;
            }
        }
        protected string _inputDataShort;
        public string InputDataShort
        {
            get
            {
                return _inputDataShort;
            }
        }
        public IRepresentativesStatisticAccumulator StatisticAccumulator { get; set; }
        public RepresentativesAsTree(int pLength, int[][] pListOfSet) : base(pLength)
        {
            listOfSet = pListOfSet;
            listOfSetAsNumber = listOfSet.Select(s => BruteForceRepresentatives.ElementNumbersToLongAsBinaryVector(s)).ToArray();
            if (listOfSet.Any(s => s.Any(e => e >= pLength)))
                throw new ArgumentException("Element of set can not be > Length.");
            _fCurrentOptimalSet = _fCurrentSet.ToList();
            currentMinimum = pLength;
            _fOptimalSets = new List<string>();
            _inputData = (Newtonsoft.Json.JsonConvert.SerializeObject(listOfSet));
            _inputDataShort = (Newtonsoft.Json.JsonConvert.SerializeObject(listOfSetAsNumber));
            StatisticAccumulator = new FakeRepresentativesStatisticAccumulator();
        }
        //-----------------------------------------------------------------------------------
        protected override void SupplementInitial()
        {
            StatisticAccumulator.CreateStatistics(_inputData, _inputDataShort, nameof(RepresentativesAsTree));
        }
        //--------------------------------------------------------------------------------------
        protected bool IsIntersect()
        {
            bool isIntersect = true;
            for (int k = 0; k < listOfSet.Length; k++)
            {
                if (!listOfSet[k].Any(s => _fCurrentSet[s] > 0))
                {
                    isIntersect = false;
                    break;
                }
            }
            return isIntersect;
        }
        //--------------------------------------------------------------------------------------
        protected void UpdateOptimalResults(int candidatValue)
        {
            if (candidatValue < currentMinimum)
            {
                StatisticAccumulator.UpdateOptcountInc();
                for (int i = 0; i < _fCurrentSet.Count; i++)
                {
                    _fCurrentOptimalSet[i] = _fCurrentSet[i];
                }
                currentMinimum = candidatValue;
                _fOptimalSets.Clear();
            }
            List<int> result = new List<int>();
            for (int i = 0; i < _fCurrentSet.Count; i++)
            {
                if (_fCurrentSet[i] != 0)
                    result.Add(i);
            }
            _fOptimalSets.Add(string.Join(",", result));
        }
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        public static long Combination(int n, int k)
        {
            long numerator = 1;
            long denominator = 1;
            for (int i = n - k + 1; i <= n; i++)
                numerator *= i;
            for (int i = 1; i <= k; i++)
                denominator *= i;

            return numerator / denominator;
        }
        //--------------------------------------------------------------------------------------
        public static long CombinationRec(int n, int k)
        {
            if (k == 1)
                return n;
            else if (k == n)
                return 1;
            else if (k > n)
                return 0;
            return Combination(n - 1, k - 1) + Combination(n - 1, k);
        }
        //--------------------------------------------------------------------------------------
        private static int LastIndexInCountMatrix(int n, int m, int j)
        {
            return n - (m - j);
        }
        //--------------------------------------------------------------------------------------
        public static int[] SkipEnumeration(int n, int m, long number)
        {
            int[] result = new int[m];
            long rest = number;
            int curn = n;
            int curm = m;
            int previ = 1;
            int prevj = 1;
            int lastJ = 0;
            while (rest > 0)
            {
                (int i, int j, long countForCurrentIndex) = GetFirstPosition(curn, curm, rest);
                int ii = previ;
                for (int jj = prevj; jj < prevj - 1 + j; jj++)
                {
                    result[jj - 1] = ii++;
                }
                lastJ = prevj - 2 + j;
                result[prevj - 2 + j] = previ + i - 1;
                previ += i;
                prevj += j;
                curm -= j;
                curn -= i;
                rest -= countForCurrentIndex;
            }
            lastJ++;
            while (lastJ < m)
            {
                result[lastJ] = LastIndexInCountMatrix(n, m, lastJ + 1);
                lastJ++;
            }
            return result;
        }
        //--------------------------------------------------------------------------------------
        public static (int, int, long) GetFirstPosition(int n, int m, long number)
        {
            if (number == 1)
                return (m, m, 1);
            int j = m;
            int jlast = m;
            int ilast = n;
            long countLast = 1;
            while (j > 0)
            {
                int lastIndex = LastIndexInCountMatrix(n, m, j);
                long countForLastIndex = GetCountForPosition(n, m, lastIndex, j);
                if (countForLastIndex == number)
                    return (lastIndex, j, countForLastIndex);
                if (countForLastIndex > number)
                {
                    int i = j + 1;
                    while (i <= n)
                    {
                        long countForCurrentIndex = GetCountForPosition(n, m, i, j);
                        if (countForCurrentIndex == number)
                            return (i, j, countForCurrentIndex);
                        if (countForCurrentIndex > number)
                            return (i, j, countLast);
                        jlast = j;
                        ilast = i;
                        countLast = countForCurrentIndex;
                        i++;
                    }
                    throw new Exception("Logical error GetFirstPosition");

                }
                else
                {
                    countLast = countForLastIndex;
                }
                --j;
            }
            return (0, 0, 0l);
        }

        private static long GetCountForPosition(int n, int m, int i, int j)
        {
            if (j > m)
                return 0;
            else if (i < j)
                return 0;
            else if (j == m)
                return i - j + 1;
            else if (i == j)
            {
                return GetCountForPosition(n, m, LastIndexInCountMatrix(n, m, j + 1), j + 1);
            }
            else
            {
                long prevCount = GetCountForPosition(n, m, i - 1, j);
                long combi = CombinationByMatrix(n - i, m - j);
                return prevCount + combi;
            }

        }
        //--------------------------------------------------------------------------------------
        public static long[,] CombinationMatrix;
        //--------------------------------------------------------------------------------------
        public static void SetCombinationMatrix(int n, int m)
        {
            CombinationMatrix = CreateCombinationMatrixByRec(n, m);
        }
        //--------------------------------------------------------------------------------------
        public static long[,] CreateCombinationMatrix(int n, int m)
        {
            long[,] matrix = new long[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    matrix[i, j] = Combination(i + 1, j + 1);

            return matrix;
        }
        //--------------------------------------------------------------------------------------
        public static long[,] CreateCombinationMatrixByRec(int n, int m)
        {
            long[,] matrix = new long[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    matrix[i, j] = CombinationRec(i + 1, j + 1);

            return matrix;
        }
        //--------------------------------------------------------------------------------------
        public static long CombinationByMatrix(int n, int k)
        {
            return RepresentativesBranchAndBoundByValue.CombinationMatrix[n - 1, k - 1];
        }
        //--------------------------------------------------------------------------------------
    }
}
