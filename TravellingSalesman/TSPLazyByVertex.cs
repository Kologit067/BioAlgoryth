using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravellingSalesman.Entities;

namespace TravellingSalesman
{
    //--------------------------------------------------------------------------------------
    // class TSPLazyByVertex 
    //--------------------------------------------------------------------------------------
    public class TSPLazyByVertex
    {
        //--------------------------------------------------------------------------------------
        public List<int> Process(int[,] matrix)
        {
            List<int> path = new List<int>();
            if (matrix.GetLength(0) != matrix.GetLength(1))
                throw new ArgumentException("Wrong size of matrix");

            int size = matrix.GetLength(0);
            OrWeightVertex[] vertecies = Enumerable.Range(0, size).Select(i => new OrWeightVertex(i)).ToArray();
            List<OrWeightEdge> edges = new List<OrWeightEdge>(size*size);

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (i != j)
                        edges.Add(new OrWeightEdge(matrix[i, j]) { InVertex = vertecies[i], OutVertex = vertecies[j]});

            edges = edges.OrderByDescending(e => e.Weight).ToList();
            for(int i = 0; i < edges.Count; i++)
            {
                edges[i].InVertex.InEdges.Add(edges[i]);
                edges[i].OutVertex.OutEdges.Add(edges[i]);
            }

            OrWeightVertex currentVertex = edges[0].InVertex;
            path.Add(currentVertex.Number);
            while (path.Count < size)
            {
                OrWeightVertex nextVertex = currentVertex.OutEdges[0].OutVertex;
                var curEdges = currentVertex.OutEdges.ToList();
                for (int j = 1; j < curEdges.Count; j++)
                    curEdges[j].Delete();

                currentVertex = nextVertex;
                path.Add(currentVertex.Number);
            }

            return path;
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
