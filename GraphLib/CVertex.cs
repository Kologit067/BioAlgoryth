using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    //-------------------------------------------------------------------------------------------------------
    // class CVertex
    //-------------------------------------------------------------------------------------------------------
    public class CVertex : CBaseVertex, IVertex
    {
        private List<int> _fAdjacentVertices = new List<int>();
        //-------------------------------------------------------------------------------------------------------
        public int Weight
        {
            get
            {
                return AdjacentVertices.Count;
            }
        }
        //-------------------------------------------------------------------------------------------------------
        private bool _fIsDeleted = false;
        public bool IsDeleted
        {
            get
            {
                return _fIsDeleted;
            }
        }
        public bool IsProcessed { get; private set; }
        //-------------------------------------------------------------------------------------------------------
        public CVertex() : base()
        {
        }
        //-------------------------------------------------------------------------------------------------------
        public CVertex(string pName) : base(pName)
        {
        }
        //-------------------------------------------------------------------------------------------------------
        public override string ToString()
        {
            return Name + " - " + string.Join(",", _fAdjacentVertices);
        }
        //-------------------------------------------------------------------------------------------------------
        public List<int> AdjacentVertices
        {
            get
            {
                return _fAdjacentVertices;
            }
        }
        //-------------------------------------------------------------------------------------------------------
        public bool IsContainVertex(int pCurrentVertex)
        {
            if (_fAdjacentVertices.Contains(pCurrentVertex))
                return true;
            return false;
        }
        //-------------------------------------------------------------------------------------------------------
        public void MarkVertexAsDeleted()
        {
            _fIsDeleted = true;
        }
        //-------------------------------------------------------------------------------------------------------
        public void RecoverVertex()
        {
            _fIsDeleted = false;
        }
        //-------------------------------------------------------------------------------------------------------
        public void SetProcessed ()
        {
            IsProcessed = true;
        }
        //-------------------------------------------------------------------------------------------------------
        public void SetName(string pName)
        {
            _fName = pName;
        }
        //-------------------------------------------------------------------------------------------------------
        public int Level { get; set; }
        //-------------------------------------------------------------------------------------------------------
    }
    //-------------------------------------------------------------------------------------------------------
}
