using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareSort
{
    public class MergeSort<T> : CompareSortBase<T>, ICompareSort<T> where T : IComparable<T>
    {
        private T[] auxiliaryList;
        public MergeSort()
        {
            
        }
        public override void Sort(List<T> inputList)
        {
            auxiliaryList = new T[inputList.Count];
            PartitionSort(inputList, 0, inputList.Count - 1);
        }
        public void PartitionSort(List<T> inputList, int startPosition, int endPosition)
        {
            int length = (endPosition - startPosition + 1);
            if (length == 1)
                return;
            if (length == 2)
            {
                if (inputList[startPosition].CompareTo(inputList[endPosition]) > 0)
                    Swap(inputList, startPosition, endPosition);
                return;
            }
            int middle = DefineMiddle(inputList, startPosition, endPosition);
            PartitionSort(inputList, startPosition, middle);
            PartitionSort(inputList, middle+1, endPosition);
            int i = 0;
            int jl = startPosition;
            int jr = middle+1;
            while (jl <= middle && jr <= endPosition)
            {
                if (inputList[jl].CompareTo(inputList[jr]) < 0)
                {
                    auxiliaryList[i++] = inputList[jl++];
                }
                else
                {
                    auxiliaryList[i++] = inputList[jr++];
                }
            }
            while (jl <= middle)
            {
                auxiliaryList[i++] = inputList[jl++];
            }
            while (jr <= endPosition)
            {
                auxiliaryList[i++] = inputList[jr++];
            }
            int j = startPosition;
            for (i = 0; i < length; i++)
                inputList[j++] = auxiliaryList[i];
        }
    }
}
