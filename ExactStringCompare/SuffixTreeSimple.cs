using BaseContract;
using ExactStringCompare.Helpers;
using System;
using System.Diagnostics;
using System.Linq;

namespace ExactStringCompare
{
    //--------------------------------------------------------------------------------------
    // class SuffixTreeSimple 
    //--------------------------------------------------------------------------------------
    public class SuffixTreeSimple : SuffixTreeBase
    {
        public static readonly string AlgorythmName = "STSMP";
        public SuffixTreeSimple()
        {
        }

        public override SuffixTreeNode Execute(string text)
        {
            if (StatisticAccumulator != null)
            {
                stopwatch = new Stopwatch();
                stopwatch.Start();
                StatisticAccumulator.CreateStatistics(text);
            }

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
                        int k = next.StarSegment;
                        if (StatisticAccumulator != null)
                            StatisticAccumulator.IterationCountInc(3);
                        while (k <= next.EndSegment)
                        {
                            if (StatisticAccumulator != null)
                            {
                                StatisticAccumulator.IterationCountInc();
                                StatisticAccumulator.NumberOfComparisonInc();
                            }
                            if (text[j++] != text[k])
                                break;
                            k++;
                        }
                        if (k > next.EndSegment)
                        {
                            if (StatisticAccumulator != null)
                                StatisticAccumulator.IterationCountInc();
                            current = next;
                        }
                        else
                        {
                            if (StatisticAccumulator != null)
                                StatisticAccumulator.IterationCountInc(14);
                            SuffixTreeNode newMiddle = new SuffixTreeNode()
                            {
                                Parent = next.Parent,
                                StarSegment = next.StarSegment,
                                EndSegment = k - 1,
                                StartSymbol = next.StartSymbol
                            };
                            SuffixTreeNode newLeaf = new SuffixTreeNode()
                            {
                                Parent = newMiddle,
                                StarSegment = j - 1,
                                EndSegment = lastPositionInText,
                                StartSymbol = text[j - 1],
                                StarPosition = i
                            };
                            newMiddle.Chields.Add(text[k], next);
                            newMiddle.Chields.Add(text[j-1], newLeaf);
                            newMiddle.Parent.Chields[newMiddle.StartSymbol] = newMiddle;
                            next.Parent = newMiddle;
                            next.StarSegment = k;
                            next.StartSymbol = text[k];
                            break;
                        }
                    }
                    else
                    {
                        if (StatisticAccumulator != null)
                            StatisticAccumulator.IterationCountInc(2);
                        current.Chields.Add(text[j], new SuffixTreeNode()
                        {
                            Parent = current,
                            StarSegment = j,
                            EndSegment = lastPositionInText,
                            StartSymbol = text[j],
                            StarPosition = i
                        });
                        break;
                    }
                }
            }

            string outputPresentation = NodePresentationAsString(root);
            if (StatisticAccumulator != null)
            {
                stopwatch.Stop();
                long elapsedTicks = stopwatch.ElapsedTicks;
                long durationMilliSeconds = stopwatch.ElapsedMilliseconds;
                StatisticAccumulator.SaveStatisticData(outputPresentation, elapsedTicks, durationMilliSeconds, DateTime.Now, null);
            }


            return root;
        }
    }
    //--------------------------------------------------------------------------------------
}
