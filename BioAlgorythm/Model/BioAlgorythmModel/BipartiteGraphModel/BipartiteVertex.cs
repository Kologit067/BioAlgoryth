using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioAlgorythmModel.BipartiteGraphModel
{
    //----------------------------------------------------------------------------------------------------------------------
    // class BipartiteVertex
    //----------------------------------------------------------------------------------------------------------------------
    public class BipartiteVertex
    {
        public List<int> AdjacentVertices { get; set; }
        public int Number { get; set; }
        public BipartiteVertex(int number, IEnumerable<int> vertices) 
        { 
            AdjacentVertices = vertices.ToList();
            Number = number;
        }
        //----------------------------------------------------------------------------------------------------------------------
        public override string ToString()
        {
            return string.Join(",", AdjacentVertices);
        }
    }
}
