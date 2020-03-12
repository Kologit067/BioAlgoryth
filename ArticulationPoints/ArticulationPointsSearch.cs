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
            _fArticulationPoints.Clear();

            ProcessLevel(0, _fGraph.Vertices[0], 0);

            return _fArticulationPoints.OrderBy(p => p).ToList();
        }
        //-------------------------------------------------------------------------------------------------------
        private int ProcessLevel(int pVertexNumber, IVertex pVertex, int pLevel)
        {
            int result = pLevel;
            int topFromSelf = pLevel;
            int topFromChildren = pLevel;
            int bottomFromChildren = 0;
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
                    if (childRev > bottomFromChildren)
                        bottomFromChildren = childRev;
                    newChildren++;
                }
            }

            if (bottomFromChildren >= pLevel && (newChildren > 0 && pLevel > 0 || newChildren > 1 ))
                _fArticulationPoints.Add(pVertexNumber);

            return Math.Min(topFromChildren, topFromSelf);
        }
        //-------------------------------------------------------------------------------------------------------
    }
    //-------------------------------------------------------------------------------------------------------
}
