using System;
using System.Numerics;
using System.Threading.Tasks;

namespace CommonLibrary.Helpers
{
    //--------------------------------------------------------------------------------------
    // class Combinatorics
    //--------------------------------------------------------------------------------------
    public static class Combinatorics
    {
        //--------------------------------------------------------------------------------------
        public static long[,] CombinationMatrix;
        public static BigInteger[,] BigIntCombinationMatrix;
        public static BigInteger[,,,] CountForPositionMatrix;
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
            return (0, 0, 0L);
        }
        //--------------------------------------------------------------------------------------
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
        public static long CombinationByMatrix(int n, int k)
        {
            return CombinationMatrix[n - 1, k - 1];
        }
        //--------------------------------------------------------------------------------------
        private static int LastIndexInCountMatrix(int n, int m, int j)
        {
            return n - (m - j);
        }
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
                    matrix[i, j] = CombinationByBigNumber(i + 1, j + 1);

            return matrix;
        }
        //--------------------------------------------------------------------------------------
        public static long CombinationByBigNumber(int n, int k)
        {
            BigInteger numerator = new BigInteger(1);
            BigInteger denominator = new BigInteger(1);
            for (int i = n - k + 1; i <= n; i++)
                numerator = BigInteger.Multiply(numerator, i);
            for (int i = 1; i <= k; i++)
                denominator = BigInteger.Multiply(denominator, i);

            var result = BigInteger.Divide(numerator, denominator);
            return (long)result;
        }
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
        public static BigInteger BigIntegerCombination(int n, int k)
        {
            if (k > n - k)
                k = n - k;
            BigInteger numerator = new BigInteger(1);
            BigInteger denominator = new BigInteger(1);
            for (int i = n - k + 1; i <= n; i++)
                numerator = BigInteger.Multiply(numerator, i);
            for (int i = 1; i <= k; i++)
                denominator = BigInteger.Multiply(denominator, i);

            return BigInteger.Divide(numerator, denominator);
        }
        //--------------------------------------------------------------------------------------
        public static long CalculateStep(int n, int k, int maxNumber)
        {
            BigInteger combinatio = BigIntegerCombination(n, k);

            var result = BigInteger.Divide(combinatio, maxNumber);
            return (long)result;
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
            return CombinationRec(n - 1, k - 1) + CombinationRec(n - 1, k);
        }
        //--------------------------------------------------------------------------------------
        // BigInteger
        //--------------------------------------------------------------------------------------
        public static int[] SkipEnumerationBigInteger(int n, int m, BigInteger number)
        {
            int[] result = new int[m];
            BigInteger rest = number;
            int curn = n;
            int curm = m;
            int previ = 1;
            int prevj = 1;
            int lastJ = 0;
            while (rest > 0)
            {
                (int i, int j, BigInteger countForCurrentIndex) = GetFirstPositionBigInteger(curn, curm, rest);
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
        public static (int, int, BigInteger) GetFirstPositionBigInteger(int n, int m, BigInteger number)
        {
            if (number == 1)
                return (m, m, 1);
            int j = m;
            int jlast = m;
            int ilast = n;
            BigInteger countLast = 1;
            while (j > 0)
            {
                int lastIndex = LastIndexInCountMatrix(n, m, j);
                BigInteger countForLastIndex = GetCountForPositionBigInteger(n, m, lastIndex, j);
                if (countForLastIndex == number)
                    return (lastIndex, j, countForLastIndex);
                if (countForLastIndex > number)
                {
                    int i = j + 1;
                    while (i <= n)
                    {
                        BigInteger countForCurrentIndex = GetCountForPositionBigInteger(n, m, i, j);
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
            return (0, 0, 0L);
        }
        //--------------------------------------------------------------------------------------
        public static void SetCombinationBigIntegerMatrix(int n, int m)
        {
            BigIntCombinationMatrix = CreateCombinationBigIntegerMatrix(n, m);
        }
        //--------------------------------------------------------------------------------------
        public static BigInteger[,] CreateCombinationBigIntegerMatrix(int n, int m)
        {
            BigInteger[,] matrix = new BigInteger[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    matrix[i, j] = BigIntegerCombination(i + 1, j + 1);

            return matrix;
        }
        //--------------------------------------------------------------------------------------
        public static void CreateCountForPositionMatrix(int n, int m)
        { 
            CountForPositionMatrix = new BigInteger[n + 1, m + 1, n + 1, m + 1];
            for (int i = 0; i <= n; i++)
                for (int j = 0; j <= m; j++)
                    for (int k = 0; k <= n; k++)
                        for (int l = 0; l <= m; l++)
                            CountForPositionMatrix[i, j, k, l] = -1;
        }
        //--------------------------------------------------------------------------------------
        public static BigInteger GetCountForPositionBigInteger(int n, int m, int i, int j)
        {
            if (j > m)
                return 0;
            else if (i < j)
                return 0;
            else if (j == m)
                return i - j + 1;
            else if (i == j)
            {
                return GetCountForPositionBigInteger(n, m, LastIndexInCountMatrix(n, m, j + 1), j + 1);
            }
            else
            {
                BigInteger prevCount = GetCountForPositionBigInteger(n, m, i - 1, j);
                BigInteger combi = CombinationByMatrixBigInteger(n - i, m - j);
                return prevCount + combi;
            }

        }
        //--------------------------------------------------------------------------------------
        public static BigInteger CombinationByMatrixBigInteger(int n, int k)
        {
            return BigIntCombinationMatrix[n - 1, k - 1];
        }
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        // BigInteger Inproved - Not Recursive
        //--------------------------------------------------------------------------------------
        public static int[] SkipEnumerationNoRecBigInteger(int n, int m, BigInteger number)
        {
            int[] result = new int[m];
            BigInteger rest = number;
            int curn = n;
            int curm = m;
            int previ = 1;
            int prevj = 1;
            int lastJ = 0;
            while (rest > 0)
            {
                (int i, int j, BigInteger countForCurrentIndex) = GetFirstPositionNoRecBigInteger(curn, curm, rest);
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
        public static (int, int, BigInteger) GetFirstPositionNoRecBigInteger(int n, int m, BigInteger number)
        {
            if (number == 1)
                return (m, m, 1);
            int j = m;
            int jlast = m;
            int ilast = n;
            BigInteger countLast = 1;
            while (j > 0)
            {
                int lastIndex = LastIndexInCountMatrix(n, m, j);
                BigInteger countForLastIndex = GetCountForPositionNoRecBigInteger(n, m, lastIndex, j);
                if (countForLastIndex == number)
                    return (lastIndex, j, countForLastIndex);
                if (countForLastIndex > number)
                {
                    int i = j + 1;
                    while (i <= n)
                    {
                        BigInteger countForCurrentIndex = GetCountForPositionNoRecBigInteger(n, m, i, j);
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
            return (0, 0, 0L);
        }
        //--------------------------------------------------------------------------------------
        public static BigInteger GetCountForPositionNoRecBigInteger(int n, int m, int istart, int jstart)
        {
            if (jstart > m)
                return 0;
            else if (istart < jstart)
                return 0;
            else if (jstart == m)
                return istart - jstart + 1;
            else
            {
                BigInteger result = n - m + 1;
                for (int j = m - 1; j >= jstart; j--)
                {
                    int iLimit = j == jstart ? istart : LastIndexInCountMatrix(n, m, j);
                    for (int i = j+1; i <= iLimit; i++)
                    {
                        result = BigInteger.Add( result, CombinationByMatrixBigInteger(n-i, m-j));
                    }
                }
                return result;
            }
        }
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        // BigInteger Inproved - Not Recursive Save first position
        //--------------------------------------------------------------------------------------
        public static int[] SkipEnumerationSaveFPBigInteger(int n, int m, BigInteger number)
        {
            int[] result = new int[m];
            BigInteger rest = number;
            int curn = n;
            int curm = m;
            int previ = 1;
            int prevj = 1;
            int lastJ = 0;
            while (rest > 0)
            {
                (int i, int j, BigInteger countForCurrentIndex) = GetFirstPositionSaveFPBigInteger(curn, curm, rest);
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
        public static (int, int, BigInteger) GetFirstPositionSaveFPBigInteger(int n, int m, BigInteger number)
        {
            if (number == 1)
                return (m, m, 1);
            int j = m;
            int jlast = m;
            int ilast = n;
            BigInteger countLast = 1;
            while (j > 0)
            {
                int lastIndex = LastIndexInCountMatrix(n, m, j);
                BigInteger countForLastIndex = GetCountForPositionSaveFPBigInteger(n, m, lastIndex, j);
                if (countForLastIndex == number)
                    return (lastIndex, j, countForLastIndex);
                if (countForLastIndex > number)
                {
                    int i = j + 1;
                    while (i <= n)
                    {
                        BigInteger countForCurrentIndex = GetCountForPositionSaveFPBigInteger(n, m, i, j);
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
            return (0, 0, 0L);
        }
        //--------------------------------------------------------------------------------------
        public static BigInteger GetCountForPositionSaveFPBigInteger(int n, int m, int istart, int jstart)
        {
            BigInteger result = 0;
            if (CountForPositionMatrix[n,m,istart, jstart] != -1)
                return CountForPositionMatrix[n,m,istart, jstart];
            if (jstart > m)
                result = 0;
            else if (istart < jstart)
                result = 0;
            else if (jstart == m)
                result = istart - jstart + 1;
            else
            {
                result = n - m + 1;
                for (int j = m - 1; j >= jstart; j--)
                {
                    int iLimit = j == jstart ? istart : LastIndexInCountMatrix(n, m, j);
                    for (int i = j + 1; i <= iLimit; i++)
                    {
                        result = BigInteger.Add(result, CombinationByMatrixBigInteger(n - i, m - j));
                    }
                }
            }
            CountForPositionMatrix[n, m, istart, jstart] = result;
            return result;
        }
        //--------------------------------------------------------------------------------------
        // BigInteger Inproved - Not Recursive Save first position Improve 1
        //--------------------------------------------------------------------------------------
        public static int[] SkipEnumerationSaveFPImpBigInteger(int n, int m, BigInteger number, int? curnStart = null, int? curmStart = null)
        {
            int[] result = new int[m];
            BigInteger rest = number;
            int curn = n;
            int curm = m;
            int previ = 1;
            int prevj = 1;
            int lastJ = 0;
            while (rest > 0)
            {
                (int i, int j, BigInteger countForCurrentIndex) = GetFirstPositionSaveFPBigImpInteger(curn, curm, rest, m == curm ? curmStart : null);
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
        public static (int, int, BigInteger) GetFirstPositionSaveFPBigImpInteger(int n, int m, BigInteger number, int? curmStart = null)
        {
            if (number == 1)
                return (m, m, 1);
            int j = curmStart ?? m;
            int jlast = m;
            int ilast = n;
            BigInteger countLast = 0;
            while (j > 0)
            {
                int lastIndex = LastIndexInCountMatrix(n, m, j);
                BigInteger countForLastIndex = GetCountForPositionSaveFPBigInteger(n, m, lastIndex, j);
                if (countForLastIndex == number)
                    return (lastIndex, j, countForLastIndex);
                if (countForLastIndex > number)
                {
                    int i = j + 1;
                    while (i <= n)
                    {
                        BigInteger countForCurrentIndex = GetCountForPositionSaveFPBigInteger(n, m, i, j);
                        if (countForCurrentIndex == number)
                            return (i, j, countForCurrentIndex);
                        if (countForCurrentIndex > number)
                        {
                            if (countLast == 0)
                            {
                                lastIndex = LastIndexInCountMatrix(n, m, j+1);
                                countLast = GetCountForPositionSaveFPBigInteger(n, m, lastIndex, j + 1);
                            }
                            return (i, j, countLast);
                        }
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
            return (0, 0, 0L);
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
