using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RepresentativesSet.Model
{
    public class SetInfo
    {
        public List<int> Elements { get; set; }
        public int IncludedInSolution { get; set; }
        public int Number { get; set; }
        public SetInfo(IEnumerable<int> elements, int number)
        {
            Elements = elements.ToList();
            IncludedInSolution = 0;
            Number = number;
        }
        public override string ToString()
        {
            return $"{Number}: Included {IncludedInSolution}. {string.Join(",", Elements)}";
        }
        public string ShortString
        {
            get
            {
                return $"{Number}: I-{IncludedInSolution}. {string.Join(",", Elements)}";
            }
        }
    }
}
