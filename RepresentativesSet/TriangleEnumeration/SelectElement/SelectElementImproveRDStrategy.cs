using RepresentativesSet.Model;
using System.Collections.Generic;
using System.Linq;

namespace RepresentativesSet.TriangleEnumeration.SelectElement
{
    public class SelectElementImproveRDStrategy : SelectElementStrategy
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
            double maxRelation = RelationCountDistinct(SetList, 0);
            for (int i = 0; i < rest[pPosition].Count; i++)
            {
                if (max == elements[rest[pPosition][i]].Weight)
                {
                    double relation = RelationCountDistinct(SetList, i);
                    if (relation < maxRelation)
                    {
                        maxInd = i;
                        maxRelation = relation;
                    }
                }
            }
            return (max, maxInd);
        }

        protected static double RelationCountDistinct(List<SetInfo> setList, int i)
        {
            double count = setList.Where(s => s.IncludedInSolution == 0 && !s.Elements.Contains(i)).Sum(s => s.Elements.Count);
            double distinct = setList.Where(s => s.IncludedInSolution == 0 && !s.Elements.Contains(i)).SelectMany(s => s.Elements).Distinct().Count();
            return 1.0 * count / distinct;
        }
    }

}
