using System.Collections.Generic;
using System.Linq;

namespace RepresentativesSet.Model
{
    public class ElementInfo
    {
        public int Weight { get; set; }
        public List<int> SetList { get; set; }
        public int Number { get; set; }
        public ElementInfo(IEnumerable<int> sets, int number) 
        { 
            SetList = sets.ToList();
            Weight = SetList.Count;
            Number = number;
        }

        public int CulculatedWeight (List<SetInfo> sets)
        {
            int weight = SetList.Where(i => sets[i].IncludedInSolution == 0).Count();
            return weight;
        }
        public override string ToString()
        {
            return $"{Number}: Weight {Weight}. {string.Join(",", SetList)}";
        }
        public string ShortString
        {
            get
            {
                return $"{Number}: W- {Weight}. {string.Join(",", SetList)}";
            }
        }
    }
}
