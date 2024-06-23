using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioAlgorythmModel.RepresentativesModel
{
    public class RepresentativesPerfomanceFilter
    {
        public string Algorithm { get; set; }
        public int? NumberOfSet { get; set; }
        public int? Dimension { get; set; }
        public long? Step { get; set; }
        public string InputLen { get; set; }
        public string InputLenSort { get; set; }
        public long? NumberOfIterationFrom { get; set; }
        public long? NumberOfIterationTo { get; set; }
        public long? DurationFrom { get; set; }
        public long? DurationTo { get; set; }
        public bool? IsComplete { get; set; }
        public int? CountTerminalFrom { get; set; }
        public int? CountTerminalTo { get; set; }
        public int? BestValue { get; set; }
    }
}
