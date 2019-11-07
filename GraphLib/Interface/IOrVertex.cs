using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    //-------------------------------------------------------------------------------------------------------
    // Interface IOrVertex
    //-------------------------------------------------------------------------------------------------------
    public interface IOrVertex
    {
        string Name { get; }
        List<int> EndPoints
        {
            get;
        }
        List<int> StartPoints
        {
            get;
        }
        int Weight
        {
            get;
        }
        int ComponentNumber { get; }
        //-------------------------------------------------------------------------------------------------------
        IEnumerable<int> AdjacentVertices
        {
            get;
        }
        //-------------------------------------------------------------------------------------------------------
        bool ContaintVertex(int pCurrentVertex);
        //-------------------------------------------------------------------------------------------------------
    }
    //-------------------------------------------------------------------------------------------------------

}
