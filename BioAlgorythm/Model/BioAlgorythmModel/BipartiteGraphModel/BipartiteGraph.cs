using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BioAlgorythmModel.BipartiteGraphModel
{
    //----------------------------------------------------------------------------------------------------------------------
    // class BipartiteGraph
    //----------------------------------------------------------------------------------------------------------------------
    public class BipartiteGraph
    {
        public List<BipartiteVertex> LeftSet { get; set; }
        public List<BipartiteVertex> RightSet { get; set; }
        public BipartiteGraph(string strGraph)
        {
            try
            {
                LeftSet = new List<BipartiteVertex>();
                RightSet = new List<BipartiteVertex>();
                string[] clauseArray = strGraph.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < clauseArray.Length; i++)
                {
                    string clause = clauseArray[i];
                    string[] vertexArray = clause.Replace("(", "").Replace(")", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    RightSet.Add(new BipartiteVertex(i, vertexArray.Where(v => int.TryParse(v, out _)).Select(v => int.Parse(v))));
                }
                List<int> leftList = RightSet.SelectMany(s => s.AdjacentVertices).Distinct().OrderBy(v => v).ToList();
                if (leftList.Count != leftList.Max() + 1)
                    throw new ArgumentException("Gap in left list " + string.Join(",", leftList));
                LeftSet = leftList.Select(l => new BipartiteVertex(l, RightSet.Select((v, i) => (v, i)).Where(g => g.v.AdjacentVertices.Contains(l)).Select(g => g.i))).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
