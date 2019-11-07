using GraphLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticulationPoints
{
    //-------------------------------------------------------------------------------------------------------
    // class СonnectedСomponent
    //-------------------------------------------------------------------------------------------------------
    public class СonnectedСomponent
    {
        //-------------------------------------------------------------------------------------------------------
        public bool IsConnected<T>(IGraph<T> pGraph) where T : IVertex
        {
            Queue<IVertex> processQueue = new Queue<IVertex>();
            var first = pGraph.Vertices.First(v => !v.IsDeleted);
            first.ComponentNumber = 1;
            processQueue.Enqueue(first);
            while(processQueue.Count > 0)
            {
                IVertex current = processQueue.Dequeue();
                foreach(int i in current.AdjacentVertices)
                {
                    if (!pGraph.Vertices[i].IsDeleted && pGraph.Vertices[i].ComponentNumber == 0)
                    {
                        pGraph.Vertices[i].ComponentNumber = 1;
                        processQueue.Enqueue(pGraph.Vertices[i]);
                    }
                }
            }
            bool result = !pGraph.Vertices.Any(v => !v.IsDeleted && v.ComponentNumber != 1);
            pGraph.ClearComponentInfo();
            return result;
        }
        //-------------------------------------------------------------------------------------------------------
    }
    //-------------------------------------------------------------------------------------------------------
}
