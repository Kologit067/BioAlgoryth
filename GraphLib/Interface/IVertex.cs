using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    //-------------------------------------------------------------------------------------------------------
    // Interface IVertex
    //-------------------------------------------------------------------------------------------------------
    public interface IVertex
    {
        string Name { get; }
        int Weight
        {
            get;
        }
        int ComponentNumber { get; set; }
        //-------------------------------------------------------------------------------------------------------
        List<int> AdjacentVertices
        {
            get;
        }
        //-------------------------------------------------------------------------------------------------------
        bool IsDeleted
        {
            get;
        }
        bool IsProcessed { get; }
        int Level { get; set; }
        bool IsContainVertex(int pCurrentVertex);
        void MarkVertexAsDeleted();
        void RecoverVertex();
        void SetProcessed();
        void SetName(string pName);
        //-------------------------------------------------------------------------------------------------------
    }
    //-------------------------------------------------------------------------------------------------------

}
