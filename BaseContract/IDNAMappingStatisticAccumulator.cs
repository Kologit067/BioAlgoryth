using CommonLibrary.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseContract
{
    public interface IDNAMappingStatisticAccumulator
    {
        void IterationCountInc();
        void TerminalCountInc();
        void UpdateOptcountInc();
        void ElemenationCountInc();
        void CreateStatistics(int size, string inputData, string algorithm, AlgorythmParameters algorythmParameters);
        void SaveStatisticData(string outputPresentation, long duration, long durationMilliSeconds, DateTime dateComplete,
            bool isComplete, string lastRoute, string optimalRoute, List<List<int>> listOfSolution);
    }
}
