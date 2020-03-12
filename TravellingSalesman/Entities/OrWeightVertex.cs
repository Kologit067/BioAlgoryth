using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesman.Entities
{
    //--------------------------------------------------------------------------------------
    // class OrWeightVertex 
    //--------------------------------------------------------------------------------------
    public class OrWeightVertex
    {
        private int _number;
        public int Number
        {
            get
            {
                return _number;
            }
        }
        //--------------------------------------------------------------------------------------
        public List<OrWeightEdge> InEdges { get; set; }
        //--------------------------------------------------------------------------------------
        public List<OrWeightEdge> OutEdges { get; set; }
        //--------------------------------------------------------------------------------------
        public OrWeightVertex(int number)
        {
            _number = number;
        }
        //--------------------------------------------------------------------------------------
    }
}
