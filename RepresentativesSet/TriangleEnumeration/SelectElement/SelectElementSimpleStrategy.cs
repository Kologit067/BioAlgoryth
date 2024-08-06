using RepresentativesSet.Model;
using System.Collections.Generic;

namespace RepresentativesSet.TriangleEnumeration.SelectElement
{
    public class SelectElementSimpleStrategy : SelectElementStrategy
    {
        public override (int Max, int MaxInd) FirstElement(int pPosition, List<ElementInfo> elements, List<SetInfo> SetList, List<List<int>> rest)
        {
            int max = elements[rest[pPosition][0]].Weight;
            int maxInd = 0;
            for (int i = 0; i < rest[pPosition].Count; i++)
            {
                if (max < elements[rest[pPosition][i]].Weight)
                {
                    max = elements[rest[pPosition][i]].Weight;
                    maxInd = i;
                }
            }
            return (max, maxInd);
        }
    }
}
