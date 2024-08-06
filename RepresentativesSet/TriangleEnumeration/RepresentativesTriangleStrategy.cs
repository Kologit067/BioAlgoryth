using BaseContract;
using CommonLibrary;
using RepresentativesSet.Model;
using RepresentativesSet.TriangleEnumeration.SelectElement;
using StatisticsStorage.Accumulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;


namespace RepresentativesSet.TriangleEnumeration
{
 
    //--------------------------------------------------------------------------------------
    // class RepresentativesTriangleStrategy 
    //--------------------------------------------------------------------------------------
    public class RepresentativesTriangleStrategy : EnumerateIntegerTrangleOrdered
    {
        protected int[][] listOfSet;
        protected long[] listOfSetAsNumber;
        protected int currentMinimum;
        protected List<int> _fCurrentOptimalSet;		    // текущий оптимальный набор элементов
        protected List<string> _fOptimalSets;               // 
        protected List<SetInfo> SetList;
        protected List<ElementInfo> Elements;
        protected int commonCounter;
        protected SelectElementStrategy _selectElementStrategy;
        //--------------------------------------------------------------------------------------
        public int CurrentMinimum
        {
            get
            {
                return currentMinimum;
            }
        }
        //--------------------------------------------------------------------------------------
        public override string AlgorithmName
        {
            get
            {
                return base.AlgorithmName + _selectElementStrategy.GetType().Name.Replace("SelectElement","")
                    .Replace("Strategy", "");
            }
        }
        //--------------------------------------------------------------------------------------
        protected string _inputData;
        public string InputData
        {
            get
            {
                return _inputData;
            }
        }
        //--------------------------------------------------------------------------------------
        protected string _inputDataShort;
        public string InputDataShort
        {
            get
            {
                return _inputDataShort;
            }
        }
        public IRepresentativesStatisticAccumulator StatisticAccumulator { get; set; }
        //--------------------------------------------------------------------------------------
        public List<int> Result
        {
            get
            {
                return _fCurrentOptimalSet.Take(currentMinimum).ToList();
            }
        }
        //--------------------------------------------------------------------------------------
        public string SetListAsString
        {
            get
            {
                return string.Join(" ", SetList.Select(s => $"[{s.ShortString}]"));
            }
        }
        //--------------------------------------------------------------------------------------
        public string ElementsAsString
        {
            get
            {
                return string.Join(" ", Elements.Select(s => $"[{s.ShortString}]"));
            }
        }
        //--------------------------------------------------------------------------------------
        public List<string> OptimalSets
        {
            get
            {
                return _fOptimalSets;
            }
        }
        //--------------------------------------------------------------------------------------
        public RepresentativesTriangleStrategy(int pLength, SelectElementStrategy selectElement) : base(pLength, pLength)
        {
            _selectElementStrategy = selectElement;
            commonCounter = 0;
            StatisticAccumulator = new FakeRepresentativesStatisticAccumulator();
        }
        //--------------------------------------------------------------------------------------
        public virtual void Execute(string pListOfSetAsString)
        {
            Execute(StringToArray(pListOfSetAsString));
        }
        //--------------------------------------------------------------------------------------
        public virtual void Execute(int[][] pListOfSet)
        {
            listOfSet = pListOfSet;
            listOfSetAsNumber = listOfSet.Select(s => BruteForceRepresentativesBinaryNumbders.ElementNumbersToLongAsBinaryVector(s)).ToArray();
            if (listOfSet.Any(s => s.Any(e => e >= _fSize)))
                throw new ArgumentException("Element of set can not be > Length.");
            _fCurrentOptimalSet = _fCurrentSet.ToList();
            currentMinimum = _fSize;
            _fOptimalSets = new List<string>();
            _inputData = (Newtonsoft.Json.JsonConvert.SerializeObject(listOfSet));

            _inputDataShort = (Newtonsoft.Json.JsonConvert.SerializeObject(listOfSetAsNumber));
            SetList = pListOfSet.Select((l, i) => new SetInfo(l, i)).ToList();
            Elements = Enumerable.Range(0, _fSize).Select(i => new ElementInfo(SetList.Where(s => s.Elements.Any(e => e == i)).Select(s => s.Number), i)).ToList();
            Execute();
        }
        //--------------------------------------------------------------------------------------
        protected override int InitialElement()
        {
            return FirstElement(0);
        }
        //--------------------------------------------------------------------------------------
        protected override int FirstElement(int pPosition)
        {
            if (_fCurrentPosition < pPosition)
            {
                passed[_fCurrentPosition].ForEach(r =>
                {
                    if (rest[pPosition].Contains(r))
                        rest[pPosition].Remove(r);
                    if (!passed[pPosition].Contains(r))
                        passed[pPosition].Add(r);
                });
            }
            if (rest[pPosition].Count == 0)
            {
                if (_fCurrentPosition < pPosition)
                    CurrentPositionBackAction(pPosition);
                return _fBreakElement;
            }

            (int max, int maxInd) = _selectElementStrategy.FirstElement(pPosition, Elements, SetList, rest);

            if (max == 0)
            {
                if (_fCurrentPosition < pPosition)
                    CurrentPositionBackAction(pPosition);
                return _fBreakElement;
            }

            int selected = rest[pPosition][maxInd];
            Elements[selected].SetList.ForEach(s =>
            {
                SetList[s].IncludedInSolution++;
                if (SetList[s].IncludedInSolution == 1)
                {
                    commonCounter++;
                    SetList[s].Elements.ForEach(e =>
                    {
                        Elements[e].Weight -= 1;
                    });
                }
            });


            rest[pPosition].Remove(selected);
            passed[pPosition].Add(selected);

            return selected;
        }
        //--------------------------------------------------------------------------------------
        protected override bool NextElement(int pPosition)
        {
            if (_fCurrentSet[pPosition] == _fBreakElement)
                return false;
            _fCurrentSet[pPosition] = FirstElement(pPosition);
            if (_fCurrentSet[pPosition] == _fBreakElement)
                return false;
            return true;
        }
        //--------------------------------------------------------------------------------------
        protected override void RemoveAction(int element)
        {
            base.RemoveAction(element);
            Elements[element].SetList.ForEach(s =>
            {
                SetList[s].IncludedInSolution--;
                if (SetList[s].IncludedInSolution == 0)
                {
                    commonCounter--;
                    SetList[s].Elements.ForEach(e =>
                    {
                        Elements[e].Weight += 1;
                    });
                }
            });

        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {

            if (commonCounter == SetList.Count)
            {
                UpdateOptimalResults(_fCurrentPosition + 1);
            }
            return false;

        }
        //--------------------------------------------------------------------------------------
        protected void UpdateOptimalResults(int candidatValue)
        {
            if (candidatValue < currentMinimum)
            {
                StatisticAccumulator.UpdateOptcountInc();
                for (int i = 0; i < _fCurrentSet.Count; i++)
                {
                    _fCurrentOptimalSet[i] = _fCurrentSet[i];
                }
                currentMinimum = candidatValue;
                _fOptimalSets.Clear();
            }
            if (candidatValue <= currentMinimum)
            {
                _fOptimalSets.Add(string.Join(",", _fCurrentSet.Take(candidatValue).OrderBy(c => c)));
            }
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// проверить выполнено ли условие для текущего набора
        /// </summary>		
        protected override bool IsCompleteCondition()
        {
            IterationAction();
            if (_fCurrentPosition >= _fSize - 1 || commonCounter == SetList.Count)
            {
                TerminalAction();
                StatisticAccumulator.TerminalCountInc();
                return true;
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
        protected override void IterationAction()
        {
            StatisticAccumulator.IterationCountInc();
        }
        //--------------------------------------------------------------------------------------
        protected override void CurrentPositionBackAction(int position)
        {
            passed[position].Clear();
            for (int i = 0; i < _fSize; i++)
            {
                if (!rest[position].Contains(i))
                    rest[position].Add(i);
            }
        }
        //--------------------------------------------------------------------------------------
        protected override void SupplementInitial()
        {
            StatisticAccumulator.CreateStatistics(listOfSet, _inputDataShort, AlgorithmName);
        }
        //--------------------------------------------------------------------------------------
        public void SortSolutions()
        {
            _fOptimalSets = OptimalSets.OrderBy(s => s).ToList();

        }
        //-----------------------------------------------------------------------------------
        protected override void PostAction()
        {
            StatisticAccumulator.SaveStatisticData(ElapsedTicks, DurationMilliSeconds, DateTime.Now,
                IsComplete, CurrentSetAsString, _fOptimalSets, currentMinimum);
        }
        //--------------------------------------------------------------------------------------
        public static int[][] StringToArray(string pListOfSetAsString)
        {
            string[] clauseArray = pListOfSetAsString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<int[]> result = new List<int[]>();
            for (int i = 0; i < clauseArray.Length; i++)
            {
                string clause = clauseArray[i];
                string[] vertexArray = clause.Replace("(", "").Replace(")", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                result.Add(vertexArray.Where(v => int.TryParse(v, out _)).Select(v => int.Parse(v)).ToArray());
            }
            return result.ToArray();
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
