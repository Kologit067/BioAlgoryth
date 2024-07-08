using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RepresentativesSet.Greedy
{
    public class RepresentativesGreedyRelation : RepresentativesGreedy
    {
        public RepresentativesGreedyRelation() : base()
        {
        }
        public override void Execute(int[][] pListOfSet)
        {
            base.Execute(pListOfSet);
            stopwatch = new Stopwatch();
            stopwatch.Start();
            while (listOfSet.Where(s => s.Count() > 0).Count() > 0)
            {
                var max = elements.Select((e, i) => (e, i)).OrderBy(o => 1.0 * o.e.Sum(k => listOfSet[k].Count()) / o.e.Count).First();

                Solution.Add(max.i);
                var deletedSets = max.e.ToList();
                for (int i = 0; i < listOfSet.Count; i++)
                {
                    StatisticAccumulator.IterationCountInc();
                    if (deletedSets.Contains(i))
                        listOfSet[i].Clear();
                }
                for (int i = 0; i < elements.Length; i++)
                {
                    StatisticAccumulator.IterationCountInc();
                    deletedSets.ForEach(d => elements[i].Remove(d));
                }
            }
            StatisticAccumulator.UpdateOptcountInc();
            stopwatch.Stop();
            _fElapsedTicks = stopwatch.ElapsedTicks;
            _fDurationMilliSeconds = stopwatch.ElapsedMilliseconds;
            StatisticAccumulator.SaveStatisticData(ElapsedTicks, DurationMilliSeconds, DateTime.Now,
                false, SolutionAsString, new List<string> { SolutionAsString }, Solution.Count);
        }
    }
}
