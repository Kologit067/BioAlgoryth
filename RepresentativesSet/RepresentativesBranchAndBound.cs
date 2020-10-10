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
        protected int[][] listOfSetAsBinary;
        //--------------------------------------------------------------------------------------
        public RepresentativesBranchAndBound(int pLength, int[][] pListOfSet)
            : base(pLength, pListOfSet)
        {
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
        }
        //--------------------------------------------------------------------------------------
        protected override void AddAction(int element)
        {
            base.AddAction(element);
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            return base.MakeAction();
            if (_fCurrentCardinality > currentMinimum)
                return false;


            if (_fCurrentPosition == _fSize - 1)
            {
                bool isIntersect = true;
                for (int k = 0; k < listOfSet.Length; k++)
                {
                    if (!listOfSet[k].Any(s => _fCurrentSet[s] > 0))
                    {
                        isIntersect = false;
                        break;
                    }
                }
                if (isIntersect)
                {
                    if (_fCurrentCardinality < currentMinimum)
                    {
                        for (int i = 0; i < _fCurrentSet.Count; i++)
                        {
                            _fCurrentOptimalSet[i] = _fCurrentSet[i];
                        }
                        currentMinimum = _fCurrentCardinality;
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
            }
            return false;

        }
        //--------------------------------------------------------------------------------------
    }
}
