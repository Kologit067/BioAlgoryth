using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    //-------------------------------------------------------------------------------------------------------
    // class Graph
    //-------------------------------------------------------------------------------------------------------
    public class Graph<T> : IGraph<T> where T : IVertex, new()
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
        public Graph(int pSize)
        {
            _fVertices = new List<T>();
            for (int i = 0; i < pSize; i++)
            {
                T vertex = new T();
                vertex.SetName(i.ToString());
                _fVertices.Add(vertex);
            }
        }
        //-------------------------------------------------------------------------------------------------------
        public void AddEdge(int pVertexFirst, int pVertexSecond)
        {
            if (_fVertices[pVertexFirst].AdjacentVertices.Contains(pVertexSecond))
                return;
            _fVertices[pVertexFirst].AdjacentVertices.Add(pVertexSecond);
            _fVertices[pVertexSecond].AdjacentVertices.Add(pVertexFirst);
        }
        //-------------------------------------------------------------------------------------------------------
        public int GetVertexWeight(int pVertexNumber)
        {
            return _fVertices[pVertexNumber].Weight;
        }
        //-------------------------------------------------------------------------------------------------------
        public int GetEdgeCount()
        {
            return _fVertices.Sum(v => v.AdjacentVertices.Count)/2;
        }
        //-------------------------------------------------------------------------------------------------------
        public bool IsContainEdge(string pNameVertexFirst, string pNameVertexSecond)
        {
            return _fVertices.Any(v => v.Name == pNameVertexFirst && v.AdjacentVertices.Any(e => _fVertices[e].Name == pNameVertexSecond));
        }
        //-------------------------------------------------------------------------------------------------------
        public void MarkVertexAsDeleted(int pIndexVertex)
        {
            _fVertices[pIndexVertex].MarkVertexAsDeleted();
        }
        //-----------------------------------------------------------------------------------------------------
        public void RecoverVertex(int pIndexVertex)
        {
            _fVertices[pIndexVertex].RecoverVertex();
        }
        //-----------------------------------------------------------------------------------------------------
        public void ClearComponentInfo()
        {
            for (int i = 0; i < _fVertices.Count; i++)
            {
                _fVertices[i].ComponentNumber = 0;
            }
        }
        //-------------------------------------------------------------------------------------------------------
    }
    //-------------------------------------------------------------------------------------------------------
}
