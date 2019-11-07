using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    //-------------------------------------------------------------------------------------------------------
    // class COrVertex
    //-------------------------------------------------------------------------------------------------------
    public class COrVertex : CBaseVertex, IOrVertex
    {
        private List<int> _fEndPoints = new List<int>();
        private List<int> _fStartPoints = new List<int>();
        //-------------------------------------------------------------------------------------------------------
        public List<int> EndPoints
        {
            get
            {
                return _fEndPoints;
            }
        }
        //-------------------------------------------------------------------------------------------------------
        public List<int> StartPoints
        {
            get
            {
                return _fStartPoints;
            }
        }
        //-------------------------------------------------------------------------------------------------------
        public int Weight
        {
            get
            {
                return _fStartPoints.Count + _fEndPoints.Count;
            }
        }
        //-------------------------------------------------------------------------------------------------------
        public COrVertex(string pName)
        {
            _fName = pName;
        }
        //-------------------------------------------------------------------------------------------------------
        public override string ToString()
        {
            return Name + " - " + string.Join(",", EndPoints) + "-" + string.Join(",", StartPoints);
        }
        //-------------------------------------------------------------------------------------------------------
        public IEnumerable<int> AdjacentVertices
        {
            get
            {
                return _fStartPoints.Union(EndPoints);
            }
        }
        //-------------------------------------------------------------------------------------------------------
        public bool ContaintVertex(int pCurrentVertex)
        {
            if (_fStartPoints.Contains(pCurrentVertex))
                return true;
            if (_fEndPoints.Contains(pCurrentVertex))
                return true;
            return false;
        }
        //-------------------------------------------------------------------------------------------------------
    }
    //-------------------------------------------------------------------------------------------------------
}
