using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareSort
{
    public class BubbleSort<T> : ICompareSort<T> where T : IComparable<T>
    {
        public void Sort(List<T> inputList)
        {
            bool isSorted = false;
            while ( !isSorted )
            {
                isSorted = true;
                for( int i = 0; i < inputList.Count-1; i++)
                {
                    if (inputList[i].CompareTo(inputList[i+1]) > 0)
                    {
                        T temp = inputList[i];
                        inputList[i] = inputList[i+1];
                        inputList[i+1] = temp;
                        isSorted = false;
                    }
                }
            }
        }
        public void SortImproved(List<T> inputList)
        { 
            int reverseIndex = 0;
            int startIndex = 0;
            int endIndex = inputList.Count - 2;
            while (reverseIndex >= 0)
            {
                reverseIndex = -1;
                for (int i = startIndex; i <= endIndex; i++)
                {
                    if (inputList[i].CompareTo(inputList[i + 1]) > 0)
                    {
                        T temp = inputList[i];
                        inputList[i] = inputList[i + 1];
                        inputList[i + 1] = temp;
                        reverseIndex = i;
                    }
                }
                endIndex = reverseIndex;
                for (int i = reverseIndex; i > 0; i--)
                {
                    if (inputList[i-1].CompareTo(inputList[i]) > 0)
                    {
                        T temp = inputList[i];
                        inputList[i] = inputList[i-1];
                        inputList[i-1] = temp;
                        startIndex = i;
                    }
                }
            }
        }
    }
}
