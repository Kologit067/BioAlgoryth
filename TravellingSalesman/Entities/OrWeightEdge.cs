using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesman.Entities
{
    //--------------------------------------------------------------------------------------
    // class OrWeightEdge 
    //--------------------------------------------------------------------------------------
    public class OrWeightEdge
    {
        private int _weight;
        public int Weight
        {
            get
            {
                return _weight;
            }
        }
        //--------------------------------------------------------------------------------------
        public OrWeightVertex InVertex { get; set; }
        //--------------------------------------------------------------------------------------
        public OrWeightVertex OutVertex { get; set; }
        //--------------------------------------------------------------------------------------
        public OrWeightEdge(int weight)
        {
            _weight = weight;
        }
        //--------------------------------------------------------------------------------------
        public void Delete()
        {
            InVertex.InEdges.Remove(this);
            OutVertex.OutEdges.Remove(this);
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
