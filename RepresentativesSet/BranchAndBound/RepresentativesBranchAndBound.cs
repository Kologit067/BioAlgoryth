using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepresentativesSet
{
    //--------------------------------------------------------------------------------------
    // class RepresentativesBranchAndBound
    //--------------------------------------------------------------------------------------
    public class RepresentativesBranchAndBound : RepresentativesBranchAndBoundByValue
    {
//        protected int[][] listOfSetAsBinary;
        protected List<int>[] listOfElements;
        protected int numberOfElement;
        protected int[] counterOfSet;
        protected int commonCounter;
        //--------------------------------------------------------------------------------------
        public RepresentativesBranchAndBound(int pLength, int[][] pListOfSet)
            : base(pLength, pListOfSet)
        {
            numberOfElement = pListOfSet.Max(x => x.Max());
            listOfElements = new List<int>[numberOfElement];
            counterOfSet = new int[pListOfSet.Length];
            for(int i = 0; i < pListOfSet.Length; i++)
            {
                foreach(int e in pListOfSet[i])
                {
                    if (listOfElements[e] == null)
                        listOfElements[e] = new List<int>();
                    listOfElements[e].Add(i);
                }
            }
            //listOfSetAsBinary = new int[listOfSet.Length][];
            //for (int i = 0; i < listOfSetAsBinary.Length; i++)
            //{
            //    listOfSetAsBinary[i] = new int[pLength];
            //    foreach (int p in listOfSet[i])
            //        listOfSetAsBinary[i][(1 << p)] = 1;
            //}
            //Parallel.For(0, listOfSetAsBinary.Length, i =>
            //{
            //    listOfSetAsBinary[i] = new int[pLength];
            //    foreach (int p in listOfSet[i])
            //        listOfSetAsBinary[i][(1 << p)] = 1;

            //});
        }
        //--------------------------------------------------------------------------------------
        protected override void RemoveAction(int element)
        {
            base.RemoveAction(element);
            foreach(int i in listOfElements[_fCurrentPosition])
            {
                counterOfSet[i] -= 1;
                if (counterOfSet[i] == 0)
                    commonCounter--;
            }
        }
        //--------------------------------------------------------------------------------------
        protected override void AddAction(int element)
        {
            base.AddAction(element);
            foreach (int i in listOfElements[_fCurrentPosition])
            {
                counterOfSet[i] += 1;
                if (counterOfSet[i] == 1)
                    commonCounter++;
            }
        }
        //--------------------------------------------------------------------------------------
        protected override void SupplementInitial()
        {
            StatisticAccumulator.CreateStatistics(listOfSet, _inputDataShort, nameof(RepresentativesBranchAndBound));
            _currentCardinality = _fCurrentSet[0];
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {

            if (_fCurrentPosition == _fSize - 1)
            {
                if (commonCounter == counterOfSet.Length)
                {
                    UpdateOptimalResults(_currentCardinality);
                }
            }
            return false;

        }
        //-----------------------------------------------------------------------------------
        protected override bool IsCompleteByCardinality()
        {
            return _currentCardinality > currentMinimum || commonCounter == counterOfSet.Length;
        }        
        //--------------------------------------------------------------------------------------
    }
}
