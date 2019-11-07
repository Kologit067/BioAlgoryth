using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    //-------------------------------------------------------------------------------------------------------
    // class CBaseVertex
    //-------------------------------------------------------------------------------------------------------
    public class CBaseVertex
    {
        protected string _fName;
        protected int _fComponentNumber;
        //------------------------------------------------------------------------------------------------------
        public string Name
        {
            get
            {
                return _fName;
            }
        }
        public int ComponentNumber
        {
            get
            {
                return _fComponentNumber;
            }
            set
            {
                _fComponentNumber = value;
            }
        }
        //-------------------------------------------------------------------------------------------------------
        public CBaseVertex()
        {
        }
        //-------------------------------------------------------------------------------------------------------
        public CBaseVertex(string pName)
        {
            _fName = pName;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
