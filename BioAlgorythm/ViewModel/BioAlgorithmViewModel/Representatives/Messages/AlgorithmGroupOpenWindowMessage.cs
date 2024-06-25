

namespace BioAlgorithmViewModel.Representatives.Messages
{
    public class AlgorithmGroupOpenWindowMessage
    {
        public string Algorithm { get; set; }
        public int NumberOfSet { get; set; }
        public int Dimension { get; set; }
        public long Step { get; set; }
    }
}
