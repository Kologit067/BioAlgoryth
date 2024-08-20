using RepresentativesSet.Model;
using System.Collections.Generic;
using System.Linq;

namespace RepresentativesSet.TriangleEnumeration.SelectElement
{
    public class SelectElementRelationStrategy : SelectElementStrategy
    {
        public override (int Max, int MaxInd) FirstElement(int pPosition, List<ElementInfo> elements, List<SetInfo> SetList, List<List<int>> rest)
        {
            double min = Relation(SetList, elements[rest[pPosition][0]]);
            int minInd = 0;
            for (int i = 1; i < rest[pPosition].Count; i++)
            {
                if (min < elements[rest[pPosition][i]].Weight)
                {
                    min = Relation(SetList, elements[rest[pPosition][i]]);
                    minInd = i;
                }
            }
            return ((int)min, minInd);
        }

        protected static double Relation(List<SetInfo> setList, ElementInfo e)
        {
            var filteredSetList = e.SetList.Where(k => setList[k].IncludedInSolution == 0);
            int sum = filteredSetList.Sum(k => setList[k].Elements.Count());
            int count = filteredSetList.Count();
            return 1.0 * filteredSetList.Sum(k => setList[k].Elements.Count()) / filteredSetList.Count();
        }
    }

}
