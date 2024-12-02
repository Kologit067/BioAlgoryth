using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            string permitation = algorithm.ShowFullString;

            // assert
            Assert.AreEqual(result, true, "Wrong result");
            Assert.AreEqual(permitation, expectedPermitation, "Wrong result");
        }
        [TestMethod()]
        public void IsIsomorphicFail1Test()
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
            graph2.AddEdge(0, 1);
            graph2.AddEdge(1, 4);
            graph2.AddEdge(0, 7);
            graph2.AddEdge(0, 2);
            graph2.AddEdge(3, 6);
            graph2.AddEdge(5, 6);
            graph2.AddEdge(2, 6);
            graph2.AddEdge(4, 7);
            graph2.AddEdge(5, 7);
//            graph2.AddEdge(3, 5);
            graph2.AddEdge(2, 5);
            IsomorphismUnorderedGrapf algorithm = new IsomorphismUnorderedGrapf(graph1, graph2);

            // act
            bool result = algorithm.IsIsomorphic();
            string permitation = algorithm.ShowFullString;

            // assert
            Assert.AreEqual(result, false, "Wrong result");
        }
        [TestMethod()]
        public void IsIsomorphic2Test()
        {
            // arrange
            // 
            Graph<CVertex> graph1 = new Graph<CVertex>(8);
            graph1.AddEdge(0, 1);
            graph1.AddEdge(0, 4);
            graph1.AddEdge(1, 3);
            graph1.AddEdge(4, 5);
            graph1.AddEdge(2, 5);
            graph1.AddEdge(2, 6);
            graph1.AddEdge(2, 7);
            graph1.AddEdge(3, 4);
            graph1.AddEdge(3, 6);
            graph1.AddEdge(5, 6);
            graph1.AddEdge(6, 7);
            Graph<CVertex> graph2 = new Graph<CVertex>(8);
            graph2.AddEdge(0, 1);
            graph2.AddEdge(1, 4);
            graph2.AddEdge(0, 7);
            graph2.AddEdge(3, 4);
            graph2.AddEdge(3, 6);
            graph2.AddEdge(5, 6);
            graph2.AddEdge(2, 6);
            graph2.AddEdge(4, 7);
            graph2.AddEdge(5, 7);
            graph2.AddEdge(3, 5);
            graph2.AddEdge(2, 5);
            IsomorphismUnorderedGrapf algorithm = new IsomorphismUnorderedGrapf(graph1, graph2);
            string expectedPermitation = "1,0,6,7,4,3,5,2";

            // act
            bool result = algorithm.IsIsomorphic();
            string permitation = algorithm.ShowFullString;

            // assert
            Assert.AreEqual(result, true, "Wrong result");
            Assert.AreEqual(permitation, expectedPermitation, "Wrong result");
        }
        [TestMethod()]
        public void IsIsomorphicFail2Test()
        {
            // arrange
            // 
            Graph<CVertex> graph1 = new Graph<CVertex>(8);
            graph1.AddEdge(0, 1);
            graph1.AddEdge(0, 4);
            graph1.AddEdge(1, 3);
            graph1.AddEdge(4, 5);
            graph1.AddEdge(2, 5);
            graph1.AddEdge(2, 6);
            graph1.AddEdge(2, 7);
            graph1.AddEdge(3, 4);
            graph1.AddEdge(1, 6);
            graph1.AddEdge(5, 6);
            graph1.AddEdge(6, 7);
            Graph<CVertex> graph2 = new Graph<CVertex>(8);
            graph2.AddEdge(0, 1);
            graph2.AddEdge(1, 4);
            graph2.AddEdge(0, 7);
            graph2.AddEdge(0, 2);
            graph2.AddEdge(3, 6);
            graph2.AddEdge(5, 6);
            graph2.AddEdge(2, 6);
            graph2.AddEdge(4, 7);
            graph2.AddEdge(5, 7);
            graph2.AddEdge(3, 5);
            graph2.AddEdge(2, 5);
            IsomorphismUnorderedGrapf algorithm = new IsomorphismUnorderedGrapf(graph1, graph2);
            string expectedPermitation = "1,0,6,7,4,3,5,2";

            // act
            bool result = algorithm.IsIsomorphic();
            string permitation = algorithm.ShowFullString;

            // assert
            Assert.AreEqual(result, false, "Wrong result");
//            Assert.AreEqual(permitation, expectedPermitation, "Wrong result");
        }
        // (1,3)  (2,5,6)  (1,4,7)  (3,6,8)  (0,7,8)  (0,2,3,7,9)  (1,3,4,7,9)  (0,1,3,5,6,7,9)  (3,4,7,8,9)  (2,3,5,6,7,8,9)
        [TestMethod()]
        public void IsIsomorphicMultiTest()
        {
            // arrange
            // 
            MultiGraph graph1 = new MultiGraph("(1,3)  (2,5,6)  (1,4,7)  (3,6,8)  (0,7,8)  (0,2,3,7,9)  (1,3,4,7,9)  (0,1,3,5,6,7,9)  (3,4,7,8,9)  (2,3,5,6,7,8,9)");
            MultiGraph graph2 = new MultiGraph("(2,4)  (3,6,7)  (2,5,8)  (4,7,9)  (1,8,9)  (1,3,4,8,0)  (2,4,5,8,0)  (1,2,4,6,7,8,0)  (4,5,8,9,0)  (3,4,6,7,8,9,0)");
            IsomorphismMultiGraph algorithm = new IsomorphismMultiGraph(graph1, graph2);
            string expectedPermitation = "1,2,3,4,5,6,7,8,9,0";

            // act
            bool result = algorithm.IsIsomorphic();
            string permitation = algorithm.ShowFullString;

            // assert
            Assert.AreEqual(result, true, "Wrong result");
            Assert.AreEqual(permitation, expectedPermitation, "Wrong result");
        }
        [TestMethod()]
        public void IsIsomorphicMultiTest2()
        {
            // arrange
            // 
            MultiGraph graph1 = new MultiGraph("(1,3)  (2,5,6)  (1,4,7)  (3,6,8)  (0,7,8)  (0,2,3,7,9)  (1,3,4,7,9)  (0,1,3,5,6,7,9)  (3,4,7,8,9)  (2,3,5,6,7,8,9)");
            MultiGraph graph2 = new MultiGraph("(3,5)  (4,7,8)  (3,6,9)  (5,8,0)  (2,9,0)  (2,4,5,9,1)  (3,5,6,9,1)  (2,3,5,7,8,9,1)  (5,6,9,0,1)  (4,5,7,8,9,0,1)");
            IsomorphismMultiGraph algorithm = new IsomorphismMultiGraph(graph1, graph2);
            string expectedPermitation = "2,3,4,5,6,7,8,9,0,1";

            // act
            bool result = algorithm.IsIsomorphic();
            string permitation = algorithm.ShowFullString;

            // assert
            Assert.AreEqual(result, true, "Wrong result");
            Assert.AreEqual(permitation, expectedPermitation, "Wrong result");
        }
        [TestMethod()]
        public void IsIsomorphicMultiTest3()
        {
            // arrange
            // 
            MultiGraph graph1 = new MultiGraph("(1,3)  (2,5,6)  (1,4,7)  (3,6,8)  (0,7,8)  (0,2,3,7,9)  (1,3,4,7,9)  (0,1,3,5,6,7,9)  (3,4,7,8,9)  (2,3,5,6,7,8,9)");
            MultiGraph graph2 = new MultiGraph("(8,6)  (7,4,3)  (8,5,2)  (6,3,1)  (9,2,1)  (9,7,6,2,0)  (8,6,5,2,0)  (9,8,6,4,3,2,0)  (6,5,2,1,0)  (7,6,4,3,2,1,0)");
            IsomorphismMultiGraph algorithm = new IsomorphismMultiGraph(graph1, graph2);
            string expectedPermitation = "9,8,7,6,5,4,3,2,1,0";

            // act
            bool result = algorithm.IsIsomorphic();
            string permitation = algorithm.ShowFullString;

            // assert
            Assert.AreEqual(result, true, "Wrong result");
            Assert.AreEqual(permitation, expectedPermitation, "Wrong result");
        }
    }
}