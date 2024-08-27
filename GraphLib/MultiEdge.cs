using System.Collections.Generic;

namespace GraphLib
{
    //--------------------------------------------------------------------------------------
    // class MultiEdge
    //--------------------------------------------------------------------------------------
    public class MultiEdge
    {
        public List<int> VertexSet {  get; set; }
        public MultiEdge(IEnumerable<int> set) { 
            VertexSet = new List<int>();
            VertexSet.AddRange(set);
        }

        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
