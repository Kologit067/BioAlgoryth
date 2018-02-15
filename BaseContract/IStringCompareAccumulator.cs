using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseContract
{
    public interface IStringCompareAccumulator
    {
        void IterationCountInc();
        void CreateStatistics(string text, string pattern);
        void SaveStatisticData(string outputPresentation, long duration, long durationMilliSeconds, DateTime dateComplete);
        void SaveRemain();
        string Delete();
    }
}
