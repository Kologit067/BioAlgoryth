using BaseContract;
using StatisticsStorage.Accumulators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary.Helpers;

namespace RepresentativesSet.Greedy
{
    public class RepresentativesGreedy
    {
        protected List<List<int>> listOfSet;
        protected long[] listOfSetAsNumber;
        protected List<int>[] elements;
        public List<int> Solution { get; set; }
        public IRepresentativesStatisticAccumulator StatisticAccumulator { get; set; }
        protected Stopwatch stopwatch;
        public string CurrenttData
        {
            get
            {
                return listOfSet.AsString();
            }
        }
        //--------------------------------------------------------------------------------------
        protected long _fElapsedTicks;
        public long ElapsedTicks
        {
            get
            {
                return _fElapsedTicks;
            }
        }
        //--------------------------------------------------------------------------------------
        protected long _fDurationMilliSeconds;
        public long DurationMilliSeconds
        {
            get
            {
                return _fDurationMilliSeconds;
            }
        }
        protected string _inputData;
        public string InputData
        {
            get
            {
                return _inputData;
            }
        }
        protected string _inputDataShort;
        public string InputDataShort
        {
            get
            {
                return _inputDataShort;
            }
        }
        public string ListOfSetAsString
        {
            get
            {
                return (Newtonsoft.Json.JsonConvert.SerializeObject(listOfSet));
            }
        }
        public string ElementAsString
        {
            get
            {
                return (Newtonsoft.Json.JsonConvert.SerializeObject(elements));
            }
        }   
        //--------------------------------------------------------------------------------------
        public string SolutionAsString
        {
            get
            {
                if (Solution != null && Solution.Count > 0)
                    return string.Join(",", Solution.Select(i => i.ToString()));
                return "Empty";
            }
        }
        public RepresentativesGreedy(int[][] pListOfSet)
        {
            this.listOfSet = pListOfSet.Select(l => l.ToList()).ToList();
            listOfSetAsNumber = listOfSet.Select(s => BruteForceRepresentatives.ElementNumbersToLongAsBinaryVector(s.ToArray())).ToArray();
            int numberOfElemnts = pListOfSet.SelectMany(l => l.Select(i => i)).Max()+1;
            elements = new List<int>[numberOfElemnts];
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i] = listOfSet.Select((l, k) => (l, k)).Where(o => o.l.Any(n => n == i)).Select(o => o.k).ToList();
            }
            StatisticAccumulator = new FakeRepresentativesStatisticAccumulator();
            _inputData = listOfSet.AsString(); // (Newtonsoft.Json.JsonConvert.SerializeObject(listOfSet));
            _inputDataShort = (Newtonsoft.Json.JsonConvert.SerializeObject(listOfSetAsNumber));
            Solution = new List<int>();
        }

        public void ExecuteSimple()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            StatisticAccumulator.CreateStatistics(listOfSet.Select(l => l.ToArray()).ToArray(), _inputDataShort, nameof(RepresentativesGreedy)+"Simple");
            while (listOfSet.Where(s => s.Count() > 0).Count() > 0)
            {
                var max = elements.Select((e, i) => (e, i)).OrderBy(o => o.e.Count).Last();
                Solution.Add(max.i);
                var deletedSets = max.e.ToList();
                StatisticAccumulator.IterationCountInc();
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
        public void ExecuteImproved()
        {

            stopwatch = new Stopwatch();
            stopwatch.Start();
            StatisticAccumulator.CreateStatistics(listOfSet.Select(l => l.ToArray()).ToArray(), _inputDataShort, nameof(RepresentativesGreedy) + "Improve");
            while (listOfSet.Where(s => s.Count() > 0).Count() > 0)
            {
                var maxCount = elements.Max(e => e.Count);
                var maxList = elements.Select((e, i) => (e, i)).Where(s => s.e.Count() == maxCount).ToList();
                (List<int> e, int i) max = maxList.First();
                StatisticAccumulator.IterationCountInc();
                if (maxList.Count > 1)
                {
                    StatisticAccumulator.IterationCountInc();
                    max = maxList.OrderBy(m => m.e.Sum(k => listOfSet[k].Count())).First();
                }

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
        public void ExecuteImprovedRD()
        {

            stopwatch = new Stopwatch();
            stopwatch.Start();
            StatisticAccumulator.CreateStatistics(listOfSet.Select(l => l.ToArray()).ToArray(), _inputDataShort, nameof(RepresentativesGreedy) + "ImproveRD");
            while (listOfSet.Where(s => s.Count() > 0).Count() > 0)
            {
                var maxCount = elements.Max(e => e.Count);
                var maxList = elements.Select((e, i) => (e, i)).Where(s => s.e.Count() == maxCount).ToList();
                (List<int> e, int i) max = maxList.First();
                StatisticAccumulator.IterationCountInc();
                if (maxList.Count > 1)
                {
                    StatisticAccumulator.IterationCountInc();
                    max = maxList.OrderBy(m => RelationCountDistinct(listOfSet, m.i)).Last();
                }

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

        private static double RelationCountDistinct(List<List<int>> listOfSet, int i)
        {
            double count = listOfSet.Where(s => !s.Contains(i)).Sum(s => s.Count);
            double distinct = listOfSet.Where(s => !s.Contains(i)).SelectMany(s => s).Distinct().Count();
            return count / distinct;
        }

        public void ExecuteRelation()
        {

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