using BaseContract;
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
    // class SuffixTreeSimple 
    //--------------------------------------------------------------------------------------
    public class SuffixTreeSimple
    {
        public static readonly string AlgorythmName = "STSMP";
        protected Stopwatch stopwatch;
        SuffixTreeNode root;
        public ISuffixTreeAccumulator StatisticAccumulator { get; set; }
        //--------------------------------------------------------------------------------------
        public SuffixTreeNode Execute(string text)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            StatisticAccumulator.CreateStatistics(text);

            int lastPositionInText = text.Length;
            text += "$";
            root = new SuffixTreeNode();
            for (int i = 0; i < text.Length-1; i++)
            {
                int j = i;
                SuffixTreeNode current = root;
                while (current != null)
                {
                    if (current.Chields.ContainsKey(text[j]))
                    {
                        SuffixTreeNode next = current.Chields[text[j]];
                        int j0 = j;
                        int k = next.StarPosition;
                        StatisticAccumulator.IterationCountInc(3);
                        while (k <= next.EndPosition)
                        {
                            StatisticAccumulator.IterationCountInc();
                            StatisticAccumulator.NumberOfComparisonInc();
                            if (text[j++] != text[k])
                                break;
                            k++;
                        }
                        if (k > next.EndPosition)
                        {
                            StatisticAccumulator.IterationCountInc();
                            current = next;
                        }
                        else
                        {
                            StatisticAccumulator.IterationCountInc(14);
                            SuffixTreeNode newMiddle = new SuffixTreeNode()
                            {
                                Parent = next.Parent,
                                StarPosition = next.StarPosition,
                                EndPosition = k - 1,
                                StartSymbol = next.StartSymbol
                            };
                            SuffixTreeNode newLeaf = new SuffixTreeNode()
                            {
                                Parent = newMiddle,
                                StarPosition = j-1,
                                EndPosition = lastPositionInText,
                                StartSymbol = text[j-1]
                            };
                            newMiddle.Chields.Add(text[k], next);
                            newMiddle.Chields.Add(text[j-1], newLeaf);
                            newMiddle.Parent.Chields[newMiddle.StartSymbol] = newMiddle;
                            next.Parent = newMiddle;
                            next.StarPosition = k;
                            next.StartSymbol = text[k];
                            break;
                        }
                    }
                    else
                    {
                        StatisticAccumulator.IterationCountInc(2);
                        current.Chields.Add(text[j], new SuffixTreeNode()
                        {
                            Parent = current,
                            StarPosition = j,
                            EndPosition = lastPositionInText,
                            StartSymbol = text[j]
                        });
                        break;
                    }
                }
            }

            stopwatch.Stop();
            long elapsedTicks = stopwatch.ElapsedTicks;
            long durationMilliSeconds = stopwatch.ElapsedMilliseconds;
            string outputPresentation = NodePresentationAsString(root);

            StatisticAccumulator.SaveStatisticData(outputPresentation, elapsedTicks, durationMilliSeconds, DateTime.Now, null);

            return root;
        }
        //--------------------------------------------------------------------------------------
        public string NodePresentationAsString()
        {
            return NodePresentationAsString(root);
        }
        //--------------------------------------------------------------------------------------
        private string NodePresentationAsString(SuffixTreeNode node)
        {
            return $"[{node.StarPosition}-{node.EndPosition}]({string.Join(",", node.Chields.OrderBy(n => n.Key).Select(n => NodePresentationAsString(n.Value)))})";
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
