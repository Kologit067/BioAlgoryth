using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsStorage.Accumulators
{
    public class StatisyicsPerfomance
    {
        //--------------------------------------------------------------------------------------
        protected long _fIterationCount;
        public long IterationCount
        {
            get
            {
                return _fIterationCount;
            }
        }
        //--------------------------------------------------------------------------------------
        protected long _fDurationMilliSeconds;
        public long DurationMilliSeconds
        {
            get
            {
                return _fDurationMilliSeconds;
            }
        }
        //--------------------------------------------------------------------------------------
        protected long _fElapsedTicks;
        public long ElapsedTicks
        {
            get
            {
                return _fElapsedTicks;
            }
        }
        protected long fCountTerminal;
        //--------------------------------------------------------------------------------------
        public long CountTerminal
        {
            get
            {
                return fCountTerminal;
            }
        }
        protected long fUpdateOptcount;
        //--------------------------------------------------------------------------------------
        public long UpdateOptcount
        {
            get
            {
                return fUpdateOptcount;
            }
        }
        protected long fElemenationCount;
        //--------------------------------------------------------------------------------------
        public long ElemenationCount
        {
            get
            {
                return fElemenationCount;
            }
        }
        //--------------------------------------------------------------------------------------
    }
}
