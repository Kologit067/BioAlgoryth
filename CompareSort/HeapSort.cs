using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareSort
{
    public class HeapSort<T> : CompareSortBase<T>, ICompareSort<T> where T : IComparable<T>
    {

        public override void Sort(List<T> inputList)
        {
            CreateHeap(inputList);
            for (int i = inputList.Count-1; i > 0; i--)
            {
                Swap(inputList, 0, i);
                int iCurrent = 0;
                bool isMoved = true;
                while(isMoved)
                {
                    isMoved = false;
                    int iChild1 = (iCurrent << 1) + 1;
                    int iChild2 = (iCurrent << 1) + 2;
                    if (iChild1 < i)
                    {
                        if (iChild2 < i)
                        {
                            if (inputList[iChild2].CompareTo(inputList[iChild1]) > 0)
                                iChild1 = iChild2;
                            if (inputList[iCurrent].CompareTo(inputList[iChild1]) < 0)
                            {
                                Swap(inputList, iCurrent, iChild1);
                                isMoved = true;
                                iCurrent = iChild1;
                            }
                        }
                        else if (inputList[iCurrent].CompareTo(inputList[iChild1]) < 0)
                        {
                            Swap(inputList, iCurrent, iChild1);
                        }
                    }
                }
            }
        }

        public void CreateHeap(List<T> inputList)
        {
            for (int i = 1; i < inputList.Count; i++)
            {
                int iCurrent = i;
                int iParent = (i - 1) >> 1;
                while (iCurrent > 0 && inputList[iParent].CompareTo(inputList[iCurrent]) < 0)
                {
                    Swap(inputList, iParent, iCurrent);
                    iCurrent = iParent;
                    iParent = (iCurrent - 1) >> 1;
                }
            }
        }
        
    }
}
