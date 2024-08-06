using RepresentativesSet.Model;
using System.Collections.Generic;
using System.Linq;

namespace RepresentativesSet.TriangleEnumeration.SelectElement
{
    public class SelectElementImproveStrategy : SelectElementStrategy
    {
        public override (int Max, int MaxInd) FirstElement(int pPosition, List<ElementInfo> elements, List<SetInfo> SetList, List<List<int>> rest)
        {
            int maxStart = elements[rest[pPosition][0]].Weight;
            int maxIndStart = 0;
            int maxCount = 1;
            for (int i = 1; i < rest[pPosition].Count; i++)
            {
                if (maxStart < elements[rest[pPosition][i]].Weight)
                {
                    maxStart = elements[rest[pPosition][i]].Weight;
                    maxIndStart = i;
                    maxCount = 1;
                }
                else if (maxStart == elements[rest[pPosition][i]].Weight)
                {
                    maxCount++;
                }
            }
            if (maxCount == 1)
                return (maxStart, maxIndStart);
            int max = maxStart;
            int maxInd = maxIndStart;
            int minSumElementInSet = elements[rest[pPosition][maxInd]].SetList.Where(k => SetList[k].IncludedInSolution == 0).Sum(k => SetList[k].Elements.Count());
            for (int i = 0; i < rest[pPosition].Count; i++)
            {
                if (max == elements[rest[pPosition][i]].Weight)
                {
                    int sumElementInSet = elements[rest[pPosition][i]].SetList.Where(k => SetList[k].IncludedInSolution == 0).Sum(k => SetList[k].Elements.Count());
                    if (sumElementInSet < minSumElementInSet)
                    {
                        maxInd = i;
                        minSumElementInSet = sumElementInSet;
                    }
                }
            }
            return (max, maxInd);
        }
    }

}
