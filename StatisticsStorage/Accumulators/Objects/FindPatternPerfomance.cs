using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsStorage.Accumulators.Objects
{
    public class FindPatternPerfomance
    {
        public string Algorithm { get; set; }
	    public int TextSize { get; set; }
        public int PatternSize { get; set; }
        public int AlphabetSize { get; set; }
        public string Text { get; set; }
        public string Pattern { get; set; }
        public string OutputPresentation { get; set; }
        public string AdditionalInfo { get; set; }
        public long IterationCount { get; set; }
        public long NumberOfComparison { get; set; }
        public long Duration { get; set; }
        public long DurationMilliSeconds { get; set; }
        public DateTime? DateComplete { get; set; }
        //--------------------------------------------------------------------------------------------------------------------
        public void IterationCountInc(int count = 1)
        {
            IterationCount += count;
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void NumberOfComparisonInc(int count = 1)
        {
            NumberOfComparison += count;
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveStatisticData(string outputPresentation, long duration, long durationMilliSeconds, DateTime dateComplete, string additionalInfo)
        {

            OutputPresentation = outputPresentation;
            Duration = duration;
            DurationMilliSeconds = durationMilliSeconds;
            DateComplete = dateComplete;
            AdditionalInfo = additionalInfo;
        }
    //--------------------------------------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------------------------------------
}
