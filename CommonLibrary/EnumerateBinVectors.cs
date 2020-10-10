using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class EnumerateBinVectors : EnumerateIntegerFullSet
    {
        //--------------------------------------------------------------------------------------
        public EnumerateBinVectors(int pLength)
            : base(1, pLength, 0)
        {
        }
    }
}
