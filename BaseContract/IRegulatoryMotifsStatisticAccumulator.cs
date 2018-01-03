using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseContract
{
    public interface IRegulatoryMotifsStatisticAccumulator
    {
        void IterationCountInc();
        void TerminalCountInc();
        void UpdateOptcountInc();
        void ElemenationCountInc();
    }
}
