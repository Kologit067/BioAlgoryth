using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExactStringCompare.Helpers
{
    public class SuffixTreeNode
    {
        public SuffixTreeNode Parent { get; set; }
        public Dictionary<char, SuffixTreeNode> Chields { get; set; }
        public int StarSegment { get; set; }
        public int EndSegment { get; set; }
        public char StartSymbol { get; set; }
        public int StarPosition { get; set; }

        public SuffixTreeNode()
        {
            Chields = new Dictionary<char, SuffixTreeNode>();
            StarSegment = 0;
            EndSegment = 0;
        }
    }
}
