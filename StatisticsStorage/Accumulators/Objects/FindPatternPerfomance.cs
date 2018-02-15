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
        public string Text { get; set; }
        public string Pattern { get; set; }
        public string OutputPresentation { get; set; }
        public long IterationCount { get; set; }
        public long Duration { get; set; }
        public long DurationMilliSeconds { get; set; }
        public DateTime? DateComplete { get; set; }
        //--------------------------------------------------------------------------------------------------------------------
        public void IterationCountInc()
        {
            IterationCount++;
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveStatisticData(string outputPresentation, long duration, long durationMilliSeconds, DateTime dateComplete)
        {

            OutputPresentation = outputPresentation;
            Duration = duration;
            DurationMilliSeconds = durationMilliSeconds;
            DateComplete = dateComplete;
        }
    //--------------------------------------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------------------------------------
}
