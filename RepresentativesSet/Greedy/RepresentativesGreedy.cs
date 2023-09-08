using BaseContract;
using StatisticsStorage.Accumulators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int numberOfElemnts = pListOfSet.SelectMany(l => l.Select(i => i)).Distinct().Count();
            elements = new List<int>[numberOfElemnts];
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i] = listOfSet.Select((l, k) => (l, k)).Where(o => o.l.Any(n => n == i)).Select(o => o.k).ToList();
            }
            StatisticAccumulator = new FakeRepresentativesStatisticAccumulator();
            _inputData = (Newtonsoft.Json.JsonConvert.SerializeObject(listOfSet));
            _inputDataShort = (Newtonsoft.Json.JsonConvert.SerializeObject(listOfSetAsNumber));
            StatisticAccumulator.CreateStatistics(_inputData, _inputDataShort, nameof(RepresentativesBranchAndBoundByValue));
            Solution = new List<int>();
        }

        public void ExecuteSimple()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
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
            while (listOfSet.Where(s => s.Count() > 0).Count() > 0)
            {
                var maxCount = elements.Max(e => e.Count);
                var maxList = elements.Where(e => e.Count() == maxCount).Select((e, i) => (e, i)).ToList();
                (List<int> e, int i) max = maxList.First();
                StatisticAccumulator.IterationCountInc();
                if (maxList.Count > 1)
                {
                    StatisticAccumulator.IterationCountInc();
                    max = maxList.OrderBy(m => m.e.Sum(k => listOfSet[k].Count())).Last();
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