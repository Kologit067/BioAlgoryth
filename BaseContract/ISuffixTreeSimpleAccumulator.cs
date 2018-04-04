using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseContract
{
    public interface ISuffixTreeSimpleAccumulator
    {
        void IterationCountInc(int count = 1);
        void NumberOfComparisonInc(int count = 1);
        void CreateStatistics(string InputPresentation);
        void SaveStatisticData(string outputPresentation, long duration, long durationMilliSeconds, DateTime dateComplete, string additionalInfo);
        void SaveRemain();
        string Delete();
        void RemoveStatisticData();
    }
}
