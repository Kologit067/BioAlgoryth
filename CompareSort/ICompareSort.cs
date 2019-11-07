using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareSort
{
    public interface ICompareSort<T> where T : IComparable<T>
    {
        void Sort(List<T> inputList);
    }
}
