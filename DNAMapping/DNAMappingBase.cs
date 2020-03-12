using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNAMapping
{
    //--------------------------------------------------------------------------------------
    // class DNAMappingBase 
    //--------------------------------------------------------------------------------------
    public abstract class DNAMappingBase
    {
        protected int[] _pairwiseDifferences;
        protected int _pairwiseDifferencesSize;
        protected int[] _restrictionMap;
        protected int _restrictionMapSize;
        //--------------------------------------------------------------------------------------
        protected virtual void DefineRestrictionMapSize()
        {
            _pairwiseDifferencesSize = _pairwiseDifferences.Length;
            _restrictionMapSize = DefineRestrictionMapSizeFromDifferencesSize(_pairwiseDifferencesSize);
        }
        //--------------------------------------------------------------------------------------
        public static int DefineRestrictionMapSizeFromDifferencesSize(int pairwiseDifferencesSize)
        {
            int baseNumber = (int)Math.Round(Math.Sqrt(2 * pairwiseDifferencesSize), 0);
            int restrictionMapSize = 0;
            for (int i = baseNumber - 2; i < baseNumber + 3; i++)
                if ((i * (i - 1)) == 2 * pairwiseDifferencesSize)
                    restrictionMapSize = i;
            if (restrictionMapSize == 0)
                throw new Exception("Incorrect size of Pairwise Differences table.");
            return restrictionMapSize;
        }
        //--------------------------------------------------------------------------------------
        public abstract void Calculate(int[] _pairwiseDifferences);
        //--------------------------------------------------------------------------------------
        public static int[] ProduceMatrix(IList<int> vector)
        {
            int[] result = new int[vector.Count * (vector.Count - 1) / 2];
            int k = 0;
            for (int i = 1; i < vector.Count; i++)
                for (int j = 0; j < i; j++)
                    result[k++] = vector[i] - vector[j];
            return result;
        }
        //--------------------------------------------------------------------------------------
        internal static int[] ProduceMatrixOnIndexBase(List<int> indecies, int[] vector)
        {
            int[] result = new int[indecies.Count * (indecies.Count - 1) / 2];
            int k = 0;
            for (int i = 1; i < indecies.Count; i++)
                for (int j = 0; j < i; j++)
                    result[k++] = vector[indecies[i]] - vector[indecies[j]];
            return result;
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
