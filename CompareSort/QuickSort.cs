using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareSort
{
    public class QuickSort<T> : CompareSortBase<T>, ICompareSort<T> where T : IComparable<T>
    {
        public override void Sort(List<T> inputList)
        {
            DivideAndSort(inputList, 0, inputList.Count - 1);
        }

        public void DivideAndSort(List<T> inputList, int startPosition, int endPosition)
        {
            int length = endPosition - startPosition + 1;
            if (startPosition > endPosition) 
                    throw new Exception("startPosition > endPosition");
            if (length < 0)
                throw new Exception("Divide logical error.");
            if (length == 1)
                return;
            if (length == 2)
            { 
                if (inputList[startPosition].CompareTo(inputList[endPosition]) > 0)
                    Swap(inputList, startPosition, endPosition);
                return;
            }
            int middlePosition = DefineMiddle(inputList, startPosition, endPosition);
            int startCurrent = startPosition;
            int endCurrent = endPosition;
            while (startCurrent < middlePosition && endCurrent > middlePosition)
            {
                while (startCurrent < middlePosition && inputList[middlePosition].CompareTo(inputList[startCurrent]) >= 0)
                    startCurrent++;
                while (endCurrent > middlePosition && inputList[middlePosition].CompareTo(inputList[endCurrent]) < 0)
                    endCurrent--;
                if (startCurrent < middlePosition && endCurrent > middlePosition)
                    Swap(inputList, startCurrent, endCurrent);
            }
            if (startCurrent == middlePosition && endCurrent > middlePosition)
            {
                while (startCurrent < endCurrent)
                {
                    while (startCurrent < endCurrent && inputList[middlePosition].CompareTo(inputList[startCurrent]) >= 0)
                        startCurrent++;
                    while (endCurrent > startCurrent && inputList[middlePosition].CompareTo(inputList[endCurrent]) <= 0)
                        endCurrent--;
                    if (startCurrent < endCurrent)
                    {
                        T middleElement = inputList[middlePosition];
                        inputList[middlePosition] = inputList[endCurrent];
                        inputList[endCurrent] = inputList[startCurrent];
                        inputList[startCurrent] = middleElement;
                        middlePosition = startCurrent++;
                    }
                    else if (inputList[middlePosition].CompareTo(inputList[endCurrent]) > 0)
                    {
                        Swap(inputList, middlePosition, endCurrent);
                        middlePosition = endCurrent - 1;
                    }
                    else if (middlePosition < endCurrent - 1)
                    {
                        Swap(inputList, middlePosition, endCurrent-1);
                        middlePosition = endCurrent - 1;
                    }
                }
            }
            if (startCurrent < middlePosition && endCurrent == middlePosition)
            {
                while (startCurrent < endCurrent)
                {
                    while (endCurrent > startCurrent && inputList[middlePosition].CompareTo(inputList[endCurrent]) <= 0)
                        endCurrent--;
                    while (startCurrent < endCurrent && inputList[middlePosition].CompareTo(inputList[startCurrent]) > 0)
                        startCurrent++;
                    if (startCurrent < endCurrent)
                    {
                        T middleElement = inputList[middlePosition];
                        inputList[middlePosition] = inputList[startCurrent];
                        inputList[startCurrent] = inputList[endCurrent];
                        inputList[endCurrent] = middleElement;
                        middlePosition = endCurrent;
                    }
                    else if (inputList[middlePosition].CompareTo(inputList[startCurrent]) < 0)
                    {
                        Swap(inputList, middlePosition, startCurrent);
                        middlePosition = startCurrent;
                    }
                    else if (middlePosition > startCurrent + 1)
                    {
                        Swap(inputList, middlePosition, startCurrent + 1);
                        middlePosition = startCurrent + 1;
                    }
                }
            }
            DivideAndSort(inputList, startPosition, middlePosition);
            if ( middlePosition < endPosition)
                DivideAndSort(inputList, middlePosition+1, endPosition);
        }


    }
}
