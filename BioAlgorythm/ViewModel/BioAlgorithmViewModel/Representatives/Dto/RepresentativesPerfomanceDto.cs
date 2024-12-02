using System;

namespace BioAlgorithmViewModel.Representatives.Dto
{
    public class RepresentativesPerfomanceDto
    {
        public string Algorithm { get; set; }
        public int NumberOfSet { get; set; }
        public int Dimension { get; set; }
        public long Step { get; set; }
        public string InputLen { get; set; }
        public string InputLenSort { get; set; }
        public string InputLenAvg { get; set; }
        public string InputData { get; set; }
        public string InputDataShort { get; set; }
        public long NumberOfIteration { get; set; }
        public long Duration { get; set; }
        public long DurationMilliSeconds { get; set; }
        public DateTime DateComplete { get; set; }
        public bool IsComplete { get; set; }
        public string LastRoute { get; set; }
        public string OptimalRoute { get; set; }
        public string CountTerminal { get; set; }
        public string BestValue { get; set; }
        public string UpdateOptcount { get; set; }
        public string ElemenationCount { get; set; }
        public string Isomorphic { get; set; }
    }
}
