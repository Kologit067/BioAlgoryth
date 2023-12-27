using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
