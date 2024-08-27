using CommonLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib
{
    //--------------------------------------------------------------------------------------
    // class MultiGraph
    //--------------------------------------------------------------------------------------
    public class MultiGraph
    {
        public List<MultiVertex> Vertices { get; private set; }
        public List<MultiEdge> Edges { get; private set; }
        public MultiGraph(string graphAsString) 
        {
            int[][] sets = CollectionPresentation.StringToArray(graphAsString);
            Vertices = sets.SelectMany(l => l).Distinct().OrderBy(l => l).Select(l => new MultiVertex(l-1)).ToList();
            int count = Vertices.Select(l => l.Ind).Count();
            int max = Vertices.Select(l => l.Ind).Max();
            if (count != max+1)
            {
                throw new ArgumentException("Gap in element list");
            }
            foreach (int[] set in sets)
            {
                MultiEdge edge = new MultiEdge(set.Select(i => i - 1));
                Edges.Add(edge);
                foreach (int i in set)
                {
                    Vertices[i-1].Edges.Add(edge);
                }
            }
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
