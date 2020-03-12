using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    //-------------------------------------------------------------------------------------------------------
    // interface IOrGraph
    //-------------------------------------------------------------------------------------------------------
    public interface IOrGraph<T> where T : IOrVertex
    {
        List<T> Vertices { get; }

        void AddEdge(int p1, int p2);
        int GetVertexWeight(int pVertexNumber);
        int GetEdgeCount();
        bool IsContainEdge(string pNameVertexStart, string pNameVertexEnd);
        //        List<IGraph<IVertex>> CreateConnectedSubGraphes();
    }
    //-------------------------------------------------------------------------------------------------------

}
