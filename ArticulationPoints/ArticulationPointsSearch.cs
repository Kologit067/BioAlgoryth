using GraphLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticulationPoints
{
    //-------------------------------------------------------------------------------------------------------
    // class ArticulationPointsSearch
    //-------------------------------------------------------------------------------------------------------
    public class ArticulationPointsSearch<T> where T : IVertex
    {
        private IGraph<T> _fGraph;
        private List<int> _fArticulationPoints = new List<int>();
        //-------------------------------------------------------------------------------------------------------
        public List<int> FindArticulationPoints(IGraph<T> pGraph) 
        {
            _fGraph = pGraph;

            ProcessLevel(0, _fGraph.Vertices[0], 0);

            return _fArticulationPoints;
        }
        //-------------------------------------------------------------------------------------------------------
        private int ProcessLevel(int pVertexNumber, IVertex pVertex, int pLevel)
        {
            int result = pLevel;
            int topFromSelf = pLevel;
            int topFromChildren = pLevel;
            pVertex.SetProcessed();
            pVertex.Level = pLevel;
            int newChildren = 0;

            for (int i = 0; i < pVertex.AdjacentVertices.Count; i++)
            {
                IVertex curVertex = _fGraph.Vertices[ pVertex.AdjacentVertices[i]];
                if ( curVertex.IsProcessed)
                {
                    if (curVertex.Level < topFromSelf && curVertex.Level < pVertex.Level - 1) 
                        topFromSelf = curVertex.Level;
                }
                else
                {
                    int childRev = ProcessLevel(pVertex.AdjacentVertices[i], curVertex, pLevel + 1);
                    if (childRev < topFromChildren)
                        topFromChildren = childRev;
                    newChildren++;
                }
            }

            if (topFromChildren >= pLevel && (newChildren > 0 && pLevel > 0 || newChildren > 1 ))
                _fArticulationPoints.Add(pVertexNumber);

            return Math.Min(topFromChildren, topFromSelf);
        }
        //-------------------------------------------------------------------------------------------------------
    }
    //-------------------------------------------------------------------------------------------------------
}
