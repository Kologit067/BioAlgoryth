using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseContract
{
    public interface IStringCompareAccumulator
    {
        void IterationCountInc(int count = 1);
        void NumberOfComparisonInc(int count = 1);
        void CreateStatistics(string text, string pattern);
        void SaveStatisticData(string outputPresentation, long duration, long durationMilliSeconds, DateTime dateComplete, string additionalInfo);
        void SaveRemain();
        string Delete();
    }
}
