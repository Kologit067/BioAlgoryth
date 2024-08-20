using CommonLibrary;
using GraphLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsomorphismGraph
{
    //--------------------------------------------------------------------------------------
    // class Isomorphism
    //--------------------------------------------------------------------------------------
    public class Isomorphism : EnumerateSetOnPosition<int, int>
    {
        protected int _fSize;
        private Graph<CVertex> _graph1;
        private Graph<CVertex> _graph2;
        List<List<int>> references;
        private bool isSatisfied = true;
        //--------------------------------------------------------------------------------------
        public Isomorphism(Graph<CVertex> graph1, Graph<CVertex> graph2) : base(graph1.Vertices.Count)
        {
            _fSize = graph1.Vertices.Count;
            _graph1 = graph1;
            _graph2 = graph2;
            Dictionary<int, List<int>> weightGroup1 = _graph1.Vertices.GroupBy(v => v.Weight).OrderBy(g => g.Key).
                ToDictionary(g => g.Key, g => g.OrderBy(v => v.ComponentNumber).Select(v => v.ComponentNumber).ToList());
            Dictionary<int, List<int>> weightGroup2 = _graph2.Vertices.GroupBy(v => v.Weight).OrderBy(g => g.Key).
                ToDictionary(g => g.Key, g => g.OrderBy(v => v.ComponentNumber).Select(v => v.ComponentNumber).ToList());
            List<int> keys1 = weightGroup1.Keys.ToList();
            List<int> keys2 = weightGroup1.Keys.ToList();
            if (keys1.Count != keys2.Count)
                return;
            foreach (int key in keys1)
            {
                if (!weightGroup2.ContainsKey(key))
                    return;
                if (weightGroup1[key].Count != weightGroup2[key].Count)
                    return;
            }
            references = _graph1.Vertices.Select(v => new List<int>()).ToList();
            for (int i = 0; i < references.Count; i++)
            {
                int weight = _graph1.Vertices[i].Weight;
                references[i].AddRange(weightGroup2[weight]);
            }
        }
        //--------------------------------------------------------------------------------------
        public bool IsIsomorphic()
        {
            if (references == null)
                return false;
            Execute();
            return isSatisfied;

        }

        //--------------------------------------------------------------------------------------
        protected override void AddAction(int p)
        {
            int vertexIndex1 = _fCurrentPosition;
            int vertexIndexInSet2 = _fCurrentSet[_fCurrentPosition];
            int vertexIndex2 = references[_fCurrentPosition][vertexIndexInSet2];
            List<int> adjacentVertices = _graph1.Vertices[vertexIndex1].AdjacentVertices;
            foreach (int vertexAdjIndexInSet1 in adjacentVertices)
            {
                if (vertexAdjIndexInSet1 < _fCurrentPosition)
                {
                    int vertexAdjIndex2= references[vertexAdjIndexInSet1][_fCurrentSet[vertexAdjIndexInSet1]];
                    if (!_graph1.Vertices[vertexIndex2].AdjacentVertices.Contains(vertexAdjIndex2))
                    {
                        isSatisfied = false;
                        return;
                    }
                }
            }
            isSatisfied = true;
        }

        //--------------------------------------------------------------------------------------
        protected override void BackAction()
        {
           
        }

        //--------------------------------------------------------------------------------------
        protected override int FirstElement(int pPosition)
        {
            int element = FindNextFreeElement(0, pPosition);
            if (element < 0)
                throw new Exception("FirstElement logic error");
            return element;
        }

        //--------------------------------------------------------------------------------------
        protected override bool NextElement(int pPosition)
        {
            int element = FindNextFreeElement(_fCurrentSet[pPosition]+1, pPosition);
            if (element < 0)
                return false;
            _fCurrentSet[pPosition] = element;
            return true;
        }
        //--------------------------------------------------------------------------------------
        protected int FindNextFreeElement(int start, int pPosition)
        {
            for (int i = start; i < references[pPosition].Count; i++)
            {
                bool isIncluded = false;
                for (int j = 0; j < pPosition; j++)
                {
                    if (references[j][_fCurrentSet[j]] == references[pPosition][i])
                    {
                        isIncluded = true;
                        break;
                    }
                }
                if (!isIncluded)
                    return i;
            }
            return -1;
        }
        //--------------------------------------------------------------------------------------
        protected override void ForwardAction()
        {
            
        }

        //--------------------------------------------------------------------------------------
        protected override int InitialElement()
        {
            return FirstElement(0);
        }

        //--------------------------------------------------------------------------------------
        protected override bool IsCompleteCondition()
        {
            IterationAction();
            if (_fCurrentPosition >= _fSize - 1)
            {
                TerminalAction();
                return true;
            }
            return false;
        }

        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1 && isSatisfied)
            {
                return true;
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
        protected override void PostAction()
        {
            
        }

        //--------------------------------------------------------------------------------------
        protected override void RemoveAction(int p)
        {
            
        }

        //--------------------------------------------------------------------------------------
        protected override void SupplementInitial()
        {
            
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
