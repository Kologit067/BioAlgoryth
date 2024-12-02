
namespace BioAlgorithmViewModel.Mappings
{
    public class AlgorithmGroupToFilterMessageBase
    {
        public string Algorithm { get; set; }
        public int NumberOfSet { get; set; }
        public int Dimension { get; set; }
        public long Step { get; set; }
    }
    public class AlgorithmGroupToFilterMessage : AlgorithmGroupToFilterMessageBase
    {

    }
    public class AlgorithmGroupToFilterByGroupMessage : AlgorithmGroupToFilterMessageBase
    {

    }
}
