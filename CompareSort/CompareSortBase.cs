using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareSort
{
    public abstract class CompareSortBase<T> : ICompareSort<T> where T : IComparable<T>
    {
        public abstract void Sort(List<T> inputList);

        protected void Swap(List<T> inputList, int first, int second)
        {
            T firstVal = inputList[first];
            inputList[first] = inputList[second];
            inputList[second] = firstVal;
        }

        protected virtual int DefineMiddle(List<T> inputList, int startPosition, int endPosition)
        {
            int length = endPosition - startPosition + 1;
            return startPosition + (length / 2) + length % 2 - 1;
        }
    }
}
