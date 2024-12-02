using CommonLibrary;
using GraphLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsomorphismGraph
{
    public class IsomorphismMultiGraph : EnumerateSetOnPosition<int, int>
    {
        protected int _fSize;
        private MultiGraph _graph1;
        private MultiGraph _graph2;
        private List<List<int>> references;
        private bool isSatisfied = true;
        //--------------------------------------------------------------------------------------
        public IsomorphismMultiGraph(MultiGraph graph1, MultiGraph graph2) : base(graph1.Vertices.Count)
        {
            _fBreakElement = -1;
            _fSize = graph1.Vertices.Count;
            _graph1 = graph1;
            _graph2 = graph2;
            Dictionary<int, List<int>> weightGroup1 = _graph1.Vertices.GroupBy(v => v.Weight).OrderBy(g => g.Key).
                ToDictionary(g => g.Key, g => g.OrderBy(v => v.Ind).Select(v => v.Ind).ToList());
            Dictionary<int, List<int>> weightGroup2 = _graph2.Vertices.GroupBy(v => v.Weight).OrderBy(g => g.Key).
                ToDictionary(g => g.Key, g => g.OrderBy(v => v.Ind).Select(v => v.Ind).ToList());
            List<int> keys1 = weightGroup1.Keys.ToList();
            List<int> keys2 = weightGroup2.Keys.ToList();
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
            Parallel.For(0, _fCurrentSet.Count, i => _fCurrentSet[i] = -1);
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
            int vertexIndex2 = GetCorrespondingVertex(_fCurrentPosition);
            List<MultiEdge> adjacentEdges = _graph1.Vertices[_fCurrentPosition].Edges;
            foreach (MultiEdge edge1 in adjacentEdges)
            {
                if (edge1.VertexSet.All( v => v < _fCurrentPosition))
                {
                    if (!_graph2.Vertices[vertexIndex2].Edges.Any(e => edge1.VertexSet.All(v => e.VertexSet.Contains(v))))
                    {
                        isSatisfied = false;
                        return;
                    }
                }
            }
            isSatisfied = true;
        }
        private int GetCorrespondingVertex(int ind)
        {
            int vertexIndexInSet2 = _fCurrentSet[ind];
            return references[ind][vertexIndexInSet2];
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
            int element = FindNextFreeElement(_fCurrentSet[pPosition] + 1, pPosition);
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
            if (_fCurrentPosition >= _fSize - 1 || !isSatisfied)
            {
                if (!IsNotTerminalCheck())
                    isSatisfied = false;
                TerminalAction();
                return true;
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
        private bool IsNotTerminalCheck()
        {
            bool isPassed = true;
            foreach (MultiEdge edge in _graph1.Edges)
            {
                var verticesEdge2 = edge.VertexSet.Select(v => GetCorrespondingVertex(v)).ToList();
                if ( !_graph2.Edges.Any(e => e.VertexSet.Count == verticesEdge2.Count && verticesEdge2.All(w => e.VertexSet.Any(v => v == w))))
                    return false;
            }

            return isPassed;
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
        public override string ShowFullString
        {
            get
            {
                if (references == null)
                    return null;
                if (_fCurrentSet != null && _fCurrentSet.Count > 0)
                    return string.Join(",", _fCurrentSet.Select((i, ind) => i >= 0 ? references[ind][i].ToString() : "_"));
                return "Empty";
            }
        }
        //--------------------------------------------------------------------------------------
    }
}
