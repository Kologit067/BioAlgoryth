using System;
using System.Collections.Generic;

namespace CompareSort
{
    public class SelectionSort<T> : ICompareSort<T> where T : IComparable<T>
    {
        public void Sort(List<T> inputList)
        {
            for (int i = 0; i < inputList.Count; i++)
            {
                T min = inputList[i];
                int imin = i;
                for (int j = i+1; j < inputList.Count; j++)
                {
                    if (min.CompareTo(inputList[j]) > 0)
                    {
                        min = inputList[j];
                        imin = j;
                    }
                }
                T temp = inputList[i];
                inputList[i] = inputList[imin];
                inputList[imin] = temp;
            }
        }
    }
}
