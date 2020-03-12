using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravellingSalesman.Entities;

namespace TravellingSalesman
{
    //--------------------------------------------------------------------------------------
    // class TSPLazyByEdge 
    //--------------------------------------------------------------------------------------
    public class TSPLazyByEdge
    {
        List<List<OrWeightVertex>> pathes = new List<List<OrWeightVertex>>();
        //--------------------------------------------------------------------------------------
        public List<int> Process(int[,] matrix)
        {
            pathes.Clear();
            if (matrix.GetLength(0) != matrix.GetLength(1))
                throw new ArgumentException("Wrong size of matrix");

            int size = matrix.GetLength(0);
            OrWeightVertex[] vertecies = Enumerable.Range(0, size).Select(i => new OrWeightVertex(i)).ToArray();
            List<OrWeightEdge> edges = new List<OrWeightEdge>(size * size);

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (i != j)
                        edges.Add(new OrWeightEdge(matrix[i, j]) { InVertex = vertecies[i], OutVertex = vertecies[j] });

            edges = edges.OrderByDescending(e => e.Weight).ToList();
            for (int i = 0; i < edges.Count; i++)
            {
                edges[i].InVertex.InEdges.Add(edges[i]);
                edges[i].OutVertex.OutEdges.Add(edges[i]);
            }

            pathes.Add(new List<OrWeightVertex>() { edges[0].InVertex, edges[0].OutVertex });
            for (int i = 1; i < edges.Count; i++)
            {
                OrWeightEdge edge = edges[i];
                int inPath = FindInPath(edge);
                int outPath = FindOutPath(edge);
                if (inPath > -2 && outPath > -2 )
                {
                    if (inPath == -1 && outPath == -1)
                    {
                        pathes.Add(new List<OrWeightVertex>() { edge.InVertex, edge.OutVertex });
                    }
                    else if (inPath == -1 && outPath > -1)
                    {
                        pathes[outPath].Insert(0, edge.InVertex);
                    }
                    else if (inPath > -1 && outPath == -1)
                    {
                        pathes[inPath].Add(edge.OutVertex);
                    }
                    else if (inPath > -1 && outPath > -1)
                    {
                        pathes[inPath].AddRange(pathes[outPath]);
                        pathes.Remove(pathes[outPath]);
                    }

                }
            }

            return pathes[0].Select(v => v.Number).ToList();
        }

        private int FindOutPath(OrWeightEdge edge)
        {
            for(int i = 0; i < pathes.Count; i++)
            {
                if (edge.OutVertex.Number == pathes[i][0].Number)
                    return i;
                if (pathes[i].Any(v => v.Number == edge.OutVertex.Number))
                    return -2;
            }
            return -1;
        }

        private int FindInPath(OrWeightEdge edge)
        {
            for (int i = 0; i < pathes.Count; i++)
            {
                if (edge.InVertex.Number == pathes[i][pathes[i].Count-1].Number)
                    return i;
                if (pathes[i].Any(v => v.Number == edge.InVertex.Number))
                    return -2;
            }
            return -1;
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
