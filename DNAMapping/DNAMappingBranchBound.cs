using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNAMapping
{
    //--------------------------------------------------------------------------------------
    // class DNAMappingBranchBound 
    //--------------------------------------------------------------------------------------
    public class DNAMappingBranchBound : DNAMappingBase
    {
        public override void Calculate(int[] pairwiseDifferences)
        {
            _pairwiseDifferences = pairwiseDifferences;
            DefineRestrictionMapSize();
        }
    }
}
