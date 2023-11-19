using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    //--------------------------------------------------------------------------------------
    // class EnumerateIntegerTrangleOrdered 
    //--------------------------------------------------------------------------------------
    public class EnumerateIntegerTrangleOrdered : EnumerateIntegerTrangle
    {
        protected List<List<int>> passed;
        protected List<List<int>> rest;
        //--------------------------------------------------------------------------------------
        public EnumerateIntegerTrangleOrdered(int pLimit, int pLength, int pMinimumValue = 0, int pForwardAdditive = 0) : base(pLimit, pLength, pMinimumValue, pForwardAdditive)
        {
            passed = Enumerable.Range(0, pLength).Select(p => new List<int>(pLimit)).ToList();
            rest = Enumerable.Range(0, pLength).Select(p => Enumerable.Range(0, pLimit).ToList()).ToList();
        }
        //--------------------------------------------------------------------------------------
        public string PassedAsString
        {
            get
            { 
                return string.Join("  ", passed.Select(p => $"[{string.Join(",",p)}]")); 
            }
        }
        //--------------------------------------------------------------------------------------
        public string RestAsString
        {
            get
            {
                return string.Join("  ", rest.Select(r => $"[{string.Join(",", r)}]"));
            }
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
