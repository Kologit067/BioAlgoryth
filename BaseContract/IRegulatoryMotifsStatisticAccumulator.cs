using CommonLibrary.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseContract
{
    public interface IRegulatoryMotifsStatisticAccumulator
    {
        void IterationCountInc();
        void TerminalCountInc();
        void UpdateOptcountInc();
        void ElemenationCountInc();
        void CreateStatistics(int size, string inputData, string algorithm, int numberOfSequence,
                string sequenceLengthes, int motifLength, AlgorythmParameters algorythmParameters);
        void AddRegulatoryMotifOptimalValueChange(long duration, long durationMilliSeconds,
            int optimalValue, string startPosition, string motif);
        void SaveStatisticData(string outputPresentation, int optimalValue, long duration, long durationMilliSeconds, DateTime dateComplete,
            bool isComplete, string lastRoute, string optimalRoute, List<List<char>> listOfMotif, List<int[]> solutionStartPositionList);
    }
}
