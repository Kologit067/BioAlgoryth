using GraphLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticulationPoints
{
    public class ArticulationPointsBruteForce<T> where T : IVertex
    {
        public List<int> FindArticulationPoints(IGraph<T> pGraph)
        {
            СonnectedСomponent connectedСomponent = new СonnectedСomponent();
            List<int> result = new List<int>();
            for (int i = 0; i < pGraph.Vertices.Count; i++)
            {
                pGraph.MarkVertexAsDeleted(i);
                if (!connectedСomponent.IsConnected(pGraph))
                    result.Add(i);
                pGraph.RecoverVertex(i);
            }

            return result;
        }
    }
}
