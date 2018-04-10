using BaseContract;
using ExactStringCompare.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExactStringCompare
{
    //--------------------------------------------------------------------------------------
    // class SuffixTreeBase 
    //--------------------------------------------------------------------------------------
    public abstract class SuffixTreeBase
    {
        protected Stopwatch stopwatch;
        protected SuffixTreeNode root;
        public ISuffixTreeAccumulator StatisticAccumulator { get; set; }
        public abstract SuffixTreeNode Execute(string text);
        //--------------------------------------------------------------------------------------
        public string NodePresentationAsString()
        {
            return NodePresentationAsString(root);
        }
        //--------------------------------------------------------------------------------------
        protected string NodePresentationAsString(SuffixTreeNode node)
        {
            return $"[{node.StarSegment}-{node.EndSegment}]({string.Join(",", node.Chields.OrderBy(n => n.Key).Select(n => NodePresentationAsString(n.Value)))})";
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
