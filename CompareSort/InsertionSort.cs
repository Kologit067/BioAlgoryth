using System;
using System.Collections.Generic;


namespace CompareSort
{
    public class InsertionSort<T> : ICompareSort<T> where T : IComparable<T>
    {
        public void Sort(List<T> inputList)
        {
            for(int i = 1; i < inputList.Count; i++)
            {
                T insertElement = inputList[i];
                int j = i;
                while (j > 0 && insertElement.CompareTo(inputList[j-1]) < 0)
                {
                    inputList[j] = inputList[j - 1];
                    j--;
                }
                inputList[j] = insertElement;
            }
        }
    }
}
