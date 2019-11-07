using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    //-------------------------------------------------------------------------------------------------------
    // interface IGraph
    //-------------------------------------------------------------------------------------------------------
    public interface IGraph<T> where T : IVertex
    {
        List<T> Vertices { get; }

        void AddEdge(int p1, int p2);
        int GetVertexWeight(int pVertexNumber);
        int GetEdgeCount();
        bool IsContainEdge(string pNameVertexStart, string pNameVertexEnd);
        void MarkVertexAsDeleted(int i);
        void RecoverVertex(int i);
        void ClearComponentInfo();
    }
}
