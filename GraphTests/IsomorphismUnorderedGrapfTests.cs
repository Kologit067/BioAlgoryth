using Microsoft.VisualStudio.TestTools.UnitTesting;
using IsomorphismGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLib;

namespace IsomorphismGraph.Tests
{
    [TestClass()]
    public class IsomorphismUnorderedGrapfTests
    {
        [TestMethod()]
        public void IsIsomorphicTest()
        {
            // arrange
            // 
            Graph<CVertex> graph1 = new Graph<CVertex>(8);
            graph1.AddEdge(0, 1);
            graph1.AddEdge(0, 4);
            graph1.AddEdge(1, 3);
            graph1.AddEdge(1, 7); 
            graph1.AddEdge(2, 5);
            graph1.AddEdge(2, 6);
            graph1.AddEdge(2, 7);
            graph1.AddEdge(3, 4);
            graph1.AddEdge(3, 6);
            graph1.AddEdge(5, 6);
            graph1.AddEdge(6, 7);
            Graph<CVertex> graph2 = new Graph<CVertex>(8);
            graph2.AddEdge(0,1);
            graph2.AddEdge(1,4);
            graph2.AddEdge(0,7);
            graph2.AddEdge(0,2);
            graph2.AddEdge(3,6);
            graph2.AddEdge(5,6);
            graph2.AddEdge(2,6);
            graph2.AddEdge(4,7);
            graph2.AddEdge(5,7);
            graph2.AddEdge(3,5);
            graph2.AddEdge(2,5);
            IsomorphismUnorderedGrapf algorithm = new IsomorphismUnorderedGrapf(graph1, graph2);
            string expectedPermitation = "1,0,6,7,4,3,5,2";

            // act
            bool result = algorithm.IsIsomorphic();
            string permitation = algorithm.ShowString;

            // assert
            Assert.AreEqual(result, true, "Wrong result");
            Assert.AreEqual(permitation, expectedPermitation, "Wrong result");
        }
    }
}