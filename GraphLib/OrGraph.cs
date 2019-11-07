using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    //-------------------------------------------------------------------------------------------------------
    // class OrGraph
    //-------------------------------------------------------------------------------------------------------
    public class OrGraph<T> : IOrGraph<T> where T : IOrVertex
    {
        private List<T> _fVertices = new List<T>();
        //-------------------------------------------------------------------------------------------------------
        public List<T> Vertices
        {
            get
            {
                return _fVertices;
            }
        }
        //-------------------------------------------------------------------------------------------------------
        public void AddEdge(int pVertexFrom, int pVertexto)
        {
            if (_fVertices[pVertexFrom].EndPoints.Contains(pVertexto))
                return;
            _fVertices[pVertexFrom].EndPoints.Add(pVertexto);
            _fVertices[pVertexto].StartPoints.Add(pVertexFrom);
        }
        //-------------------------------------------------------------------------------------------------------
        public int GetVertexWeight(int pVertexNumber)
        {
            return _fVertices[pVertexNumber].Weight;
        }
        //-------------------------------------------------------------------------------------------------------
        public int GetEdgeCount()
        {
            return _fVertices.Sum(v => v.StartPoints.Count);
        }
        //-------------------------------------------------------------------------------------------------------
        public bool IsContainEdge(string pNameVertexStart, string pNameVertexEnd)
        {
            return _fVertices.Any(v => v.Name == pNameVertexStart && v.EndPoints.Any(e => _fVertices[e].Name == pNameVertexEnd));
        }

        //-------------------------------------------------------------------------------------------------------
    }
    //-------------------------------------------------------------------------------------------------------

}
