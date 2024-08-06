using BaseContract;
using StatisticsStorage.Accumulators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CommonLibrary.Helpers;

namespace RepresentativesSet.Greedy
{
    public abstract class RepresentativesGreedy
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
        //--------------------------------------------------------------------------------------
        public virtual string AlgorithmName
        {
            get
            {
                return GetType().Name;
            }
        }
        public RepresentativesGreedy()
        {
            StatisticAccumulator = new FakeRepresentativesStatisticAccumulator();
            Solution = new List<int>();
        }

        public virtual void Execute(int[][] pListOfSet)
        {
            this.listOfSet = pListOfSet.Select(l => l.ToList()).ToList();
            listOfSetAsNumber = listOfSet.Select(s => BruteForceRepresentativesBinaryNumbders.ElementNumbersToLongAsBinaryVector(s.ToArray())).ToArray();
            int numberOfElemnts = pListOfSet.SelectMany(l => l.Select(i => i)).Max() + 1;
            elements = new List<int>[numberOfElemnts];
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i] = listOfSet.Select((l, k) => (l, k)).Where(o => o.l.Any(n => n == i)).Select(o => o.k).ToList();
            }
            _inputData = listOfSet.AsString(); // (Newtonsoft.Json.JsonConvert.SerializeObject(listOfSet));
            _inputDataShort = (Newtonsoft.Json.JsonConvert.SerializeObject(listOfSetAsNumber));
            Solution.Clear();
        }

        protected static double RelationCountDistinct(List<List<int>> listOfSet, int i)
        {
            double count = listOfSet.Where(s => !s.Contains(i)).Sum(s => s.Count);
            double distinct = listOfSet.Where(s => !s.Contains(i)).SelectMany(s => s).Distinct().Count();
            return count / distinct;
        }

    }
}