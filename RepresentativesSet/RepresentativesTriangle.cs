using BaseContract;
using CommonLibrary;
using RepresentativesSet.Model;
using StatisticsStorage.Accumulators;
using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace RepresentativesSet
{
    //--------------------------------------------------------------------------------------
    // class RepresentativesTriangle 
    //--------------------------------------------------------------------------------------
    public class RepresentativesTriangle : EnumerateIntegerTrangleOrdered
    {
        protected int[][] listOfSet;
        protected long[] listOfSetAsNumber;
        protected int currentMinimum;
        protected List<int> _fCurrentOptimalSet;		    // текущий оптимальный набор элементов
        protected List<string> _fOptimalSets;               // 
        protected List<SetInfo> SetList;
        protected List<ElementInfo> Elements;
        protected int commonCounter;
        //--------------------------------------------------------------------------------------
        public int CurrentMinimum
        {
            get
            {
                return currentMinimum;
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
        public IRepresentativesStatisticAccumulator StatisticAccumulator { get; set; }
        //--------------------------------------------------------------------------------------
        public List<int> Result
        {
            get
            {
                List<int> result = new List<int>();
                for (int i = 0; i < _fCurrentOptimalSet.Count; i++)
                {
                    if (_fCurrentOptimalSet[i] != 0)
                        result.Add(i);
                }
                return result;
            }
        }
        public string SetListAsString => string.Join(" ", $"[{SetList.Select(s => s.ShortString)}]");
        public string ElementsAsString => string.Join(" ", $"[{Elements.Select(s => s.ShortString)}]");
        //--------------------------------------------------------------------------------------
        public RepresentativesTriangle(int pLength, string pListOfSetAsString) : this(pLength, StringToArray(pListOfSetAsString))
        {

        }
        //--------------------------------------------------------------------------------------
        public static int[][] StringToArray(string pListOfSetAsString)
        {
            string[] clauseArray = pListOfSetAsString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<int[]> result= new List<int[]>();
            for (int i = 0; i < clauseArray.Length; i++)
            {
                string clause = clauseArray[i];
                string[] vertexArray = clause.Replace("(", "").Replace(")", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                result.Add(vertexArray.Where(v => int.TryParse(v, out _)).Select(v => int.Parse(v)).ToArray());
            }
            return result.ToArray();
        }
        //--------------------------------------------------------------------------------------
        public RepresentativesTriangle(int pLength, int[][] pListOfSet) : base(pLength, pLength)
        {
            listOfSet = pListOfSet;
            listOfSetAsNumber = listOfSet.Select(s => BruteForceRepresentatives.ElementNumbersToLongAsBinaryVector(s)).ToArray();
            if (listOfSet.Any(s => s.Any(e => e >= pLength)))
                throw new ArgumentException("Element of set can not be > Length.");
            _fCurrentOptimalSet = _fCurrentSet.ToList();
            currentMinimum = pLength;
            _fOptimalSets = new List<string>();
            _inputData = (Newtonsoft.Json.JsonConvert.SerializeObject(listOfSet));

            _inputDataShort = (Newtonsoft.Json.JsonConvert.SerializeObject(listOfSetAsNumber));
            StatisticAccumulator = new FakeRepresentativesStatisticAccumulator();
            SetList = pListOfSet.Select((l,i) => new SetInfo(l,i)).ToList();
            Elements = Enumerable.Range(0, pLength).Select(i => new ElementInfo(SetList.Where(s => s.Elements.Any(e => e == i)).Select(s => s.Number),i)).ToList();
            commonCounter = 0;
        }
        //--------------------------------------------------------------------------------------
        protected override int InitialElement()
        {
            return FirstElement(0);
        }
        //--------------------------------------------------------------------------------------
        protected override int FirstElement(int pPosition)
        {
            if (rest[pPosition].Count == 0)
                return _fBreakElement;
            int max = Elements[rest[pPosition][0]].Weight;
            int maxInd = 0;
            for(int i = 0; i < rest[pPosition].Count; i++)
            {
                if (max < Elements[rest[pPosition][i]].Weight)
                {
                    max = Elements[rest[pPosition][i]].Weight;
                    maxInd = i;
                }
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
            int nextpPosition = pPosition + 1;
            if (nextpPosition < rest.Count)
            {
                passed[pPosition].ForEach(r =>
                {
                    if (rest[nextpPosition].Contains(r))
                        rest[nextpPosition].Remove(r);
                    if (!passed[nextpPosition].Contains(r))
                        passed[nextpPosition].Add(r);
                });
            }
            return selected;
        }
        //--------------------------------------------------------------------------------------
        protected override bool NextElement(int pPosition)
        {
            if (_fCurrentSet[pPosition] == _fBreakElement)
                return false;
            _fCurrentSet[pPosition] = FirstElement(pPosition);
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
                    commonCounter--;
                SetList[s].Elements.ForEach(e =>
                {
                    Elements[e].Weight += 1;
                });
            });

        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {

            if (commonCounter == SetList.Count)
            {
                UpdateOptimalResults(_fCurrentPosition);
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
            List<int> result = new List<int>();
            for (int i = 0; i < _fCurrentSet.Count; i++)
            {
                if (_fCurrentSet[i] != 0)
                    result.Add(i);
            }
            _fOptimalSets.Add(string.Join(",", result));
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
                return true;
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
