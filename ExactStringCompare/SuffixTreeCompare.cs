using ExactStringCompare.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExactStringCompare
{
    //--------------------------------------------------------------------------------------
    // class SuffixTreeCompare 
    //--------------------------------------------------------------------------------------
    public class SuffixTreeCompare : StringPreprocessing
    {
        public static readonly string AlgorythmName = "SFTCMP";
        //--------------------------------------------------------------------------------------

        public List<int> FindSubstring(string text, string pattern, SuffixTreeBase suffixTreeBase,  bool isSaveStatisticsForEmpty = true)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            StatisticAccumulator.CreateStatistics(text, pattern);

            SuffixTreeNode root = suffixTreeBase.Execute(text);

            List<int> result = new List<int>();

            SuffixTreeNode lastNode = null;
            SuffixTreeNode currentNode = root;
            int patternPosition = 0;
            while (lastNode == null)
            {
                SuffixTreeNode nextNode = null;
                if (!currentNode.Chields.TryGetValue(pattern[patternPosition], out nextNode))
                    break;
                int i = 0;
                for (i = nextNode.StarSegment; i <= nextNode.EndSegment; i++)
                {
                    if (text[i] != pattern[patternPosition])
                        break;
                    if (patternPosition++ == pattern.Length - 1)
                    {
                        lastNode = nextNode;
                        break;
                    }
                }
                if (i < nextNode.EndSegment || lastNode != null)
                    break;
            }

            if (lastNode != null)
            {
                Collectleave(lastNode, result);
            }

            stopwatch.Stop();
            if (result.Count > 0 || isSaveStatisticsForEmpty)
            {
                long elapsedTicks = stopwatch.ElapsedTicks;
                long durationMilliSeconds = stopwatch.ElapsedMilliseconds;
                _outputPresentation = string.Join(",", result.Select(p => p.ToString()));

                StatisticAccumulator.SaveStatisticData(_outputPresentation, elapsedTicks, durationMilliSeconds, DateTime.Now, null);
            }
            else
            {
                StatisticAccumulator.RemoveStatisticData();
            }

            return result;
        }
        //--------------------------------------------------------------------------------------
        private void Collectleave(SuffixTreeNode node,List<int> list)
        {
            if (node.Chields.Count == 0)
            {
                list.Add(node.StarPosition);
            }
            else
            {
                foreach (var n in node.Chields.Values)
                    Collectleave(n, list);
            }
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
