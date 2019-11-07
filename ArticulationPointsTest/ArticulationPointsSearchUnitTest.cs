using System;
using System.Collections.Generic;
using System.Linq;
using ArticulationPoints;
using GraphLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArticulationPointsTest
{
    //-------------------------------------------------------------------------------------------------------
    // class ArticulationPointsSearchUnitTest
    //-------------------------------------------------------------------------------------------------------
    [TestClass]
    public class ArticulationPointsSearchUnitTest
    {
        //-------------------------------------------------------------------------------------------------------
        [TestMethod]
        public void FindArticulationPointsTest1()
        {
            // arrange
            ArticulationPointsSearch<CVertex> articulationPointsSearch = new ArticulationPointsSearch<CVertex>();
            Graph<CVertex> graph = new Graph<CVertex>(6);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(0, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(3, 5);
            graph.AddEdge(4, 5);
            string expectedResult = "2,3";
            // act            
            List<int> points = articulationPointsSearch.FindArticulationPoints(graph);
            string actualResult = string.Join(",", points.OrderBy(p => p));
            // assert
            Assert.AreEqual(expectedResult, actualResult, "");
        }
        //-------------------------------------------------------------------------------------------------------
        [TestMethod]
        public void FindArticulationPointsTest2()
        {
            // arrange
            ArticulationPointsSearch<CVertex> articulationPointsSearch = new ArticulationPointsSearch<CVertex>();
            Graph<CVertex> graph = new Graph<CVertex>(5);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(0, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(2, 4);
            string expectedResult = "2";
            // act            
            List<int> points = articulationPointsSearch.FindArticulationPoints(graph);
            string actualResult = string.Join(",", points.OrderBy(p => p));
            // assert
            Assert.AreEqual(expectedResult, actualResult, "");
        }
        //-------------------------------------------------------------------------------------------------------
        [TestMethod]
        public void FindArticulationPointsTest3()
        {
            // arrange
            ArticulationPointsSearch<CVertex> articulationPointsSearch = new ArticulationPointsSearch<CVertex>();
            Graph<CVertex> graph = new Graph<CVertex>(6);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(0, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 5);
            string expectedResult = "2,3";
            // act            
            List<int> points = articulationPointsSearch.FindArticulationPoints(graph);
            string actualResult = string.Join(",", points.OrderBy(p => p));
            // assert
            Assert.AreEqual(expectedResult, actualResult, "");
        }
        //-------------------------------------------------------------------------------------------------------
        [TestMethod]
        public void FindArticulationPointsTest4()
        {
            // arrange
            ArticulationPointsSearch<CVertex> articulationPointsSearch = new ArticulationPointsSearch<CVertex>();
            Graph<CVertex> graph = new Graph<CVertex>(6);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(0, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(3, 5);
            string expectedResult = "2,3";
            // act            
            List<int> points = articulationPointsSearch.FindArticulationPoints(graph);
            string actualResult = string.Join(",", points.OrderBy(p => p));
            // assert
            Assert.AreEqual(expectedResult, actualResult, "");
        }
        //-------------------------------------------------------------------------------------------------------
        [TestMethod]
        public void FindArticulationPointsTest5()
        {
            // arrange
            ArticulationPointsSearch<CVertex> articulationPointsSearch = new ArticulationPointsSearch<CVertex>();
            Graph<CVertex> graph = new Graph<CVertex>(7);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(0, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(2, 4);
            graph.AddEdge(2, 5);
            graph.AddEdge(5, 6);
            graph.AddEdge(2, 6);
            string expectedResult = "2";
            // act            
            List<int> points = articulationPointsSearch.FindArticulationPoints(graph);
            string actualResult = string.Join(",", points.OrderBy(p => p));
            // assert
            Assert.AreEqual(expectedResult, actualResult, "");
        }
        //-------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------
        [TestMethod]
        public void ArticulationPointsBruteForceTest1()
        {
            // arrange
            ArticulationPointsBruteForce<CVertex> articulationPointsBruteForce = new ArticulationPointsBruteForce<CVertex>();
            Graph<CVertex> graph = new Graph<CVertex>(6);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(0, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(3, 5);
            graph.AddEdge(4, 5);
            string expectedResult = "2,3";
            // act            
            List<int> points = articulationPointsBruteForce.FindArticulationPoints(graph);
            string actualResult = string.Join(",", points.OrderBy(p => p));
            // assert
            Assert.AreEqual(expectedResult, actualResult, "");
        }
        //-------------------------------------------------------------------------------------------------------
        [TestMethod]
        public void ArticulationPointsBruteForceTest2()
        {
            // arrange
            ArticulationPointsBruteForce<CVertex> articulationPointsBruteForce = new ArticulationPointsBruteForce<CVertex>();
            Graph<CVertex> graph = new Graph<CVertex>(5);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(0, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(2, 4);
            string expectedResult = "2";
            // act            
            List<int> points = articulationPointsBruteForce.FindArticulationPoints(graph);
            string actualResult = string.Join(",", points.OrderBy(p => p));
            // assert
            Assert.AreEqual(expectedResult, actualResult, "");
        }
        //-------------------------------------------------------------------------------------------------------
        [TestMethod]
        public void ArticulationPointsBruteForceTest3()
        {
            // arrange
            ArticulationPointsBruteForce<CVertex> articulationPointsBruteForce = new ArticulationPointsBruteForce<CVertex>();
            Graph<CVertex> graph = new Graph<CVertex>(6);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(0, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 5);
            string expectedResult = "2,3";
            // act            
            List<int> points = articulationPointsBruteForce.FindArticulationPoints(graph);
            string actualResult = string.Join(",", points.OrderBy(p => p));
            // assert
            Assert.AreEqual(expectedResult, actualResult, "");
        }
        //-------------------------------------------------------------------------------------------------------
        [TestMethod]
        public void ArticulationPointsBruteForceTest4()
        {
            // arrange
            ArticulationPointsBruteForce<CVertex> articulationPointsBruteForce = new ArticulationPointsBruteForce<CVertex>();
            Graph<CVertex> graph = new Graph<CVertex>(6);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(0, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(3, 5);
            string expectedResult = "2,3";
            // act            
            List<int> points = articulationPointsBruteForce.FindArticulationPoints(graph);
            string actualResult = string.Join(",", points.OrderBy(p => p));
            // assert
            Assert.AreEqual(expectedResult, actualResult, "");
        }
        //-------------------------------------------------------------------------------------------------------
        [TestMethod]
        public void ArticulationPointsBruteForceTest5()
        {
            // arrange
            ArticulationPointsBruteForce<CVertex> articulationPointsBruteForce = new ArticulationPointsBruteForce<CVertex>();
            Graph<CVertex> graph = new Graph<CVertex>(7);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(0, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(2, 4);
            graph.AddEdge(2, 5);
            graph.AddEdge(5, 6);
            graph.AddEdge(2, 6);
            string expectedResult = "2";
            // act            
            List<int> points = articulationPointsBruteForce.FindArticulationPoints(graph);
            string actualResult = string.Join(",", points.OrderBy(p => p));
            // assert
            Assert.AreEqual(expectedResult, actualResult, "");
        }
        //-------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------
        [TestMethod]
        public void FindArticulationPointsTest()
        {
            // arrange
            int size = 8;
            // act            
            EnumerateGraphBySize(size);
            // assert
        }
        //-------------------------------------------------------------------------------------------------------
        private void EnumerateGraphBySize(int pSize)
        {
            СonnectedСomponent connectedСomponent = new СonnectedСomponent();
            ArticulationPointsSearch<CVertex> articulationPointsSearch = new ArticulationPointsSearch<CVertex>();
            ArticulationPointsBruteForce<CVertex> articulationPointsBruteForce = new ArticulationPointsBruteForce<CVertex>();
            int length = pSize * (pSize - 1) / 2;
            int maxValue = 1 << (length - 1);
            int maxI = pSize - 1;
            for (int k = 0; k < maxValue; k++)
            {
                Graph<CVertex> graph = new Graph<CVertex>(pSize);
                Graph<CVertex> graph1 = new Graph<CVertex>(pSize);
                Graph<CVertex> graph2 = new Graph<CVertex>(pSize);
                int val = k;
                for (int i = 0; i < maxI; i++)
                {
                    for (int j = 0; j < pSize; j++)
                    {
                        int bin = val & 1;
                        if (bin != 0)
                        {
                            graph.AddEdge(i, j);
                            graph1.AddEdge(i, j);
                            graph2.AddEdge(i, j);
                        }
                        val >>= 1;
                    }
                    if (connectedСomponent.IsConnected(graph))
                    {
                        List<int> points1 = articulationPointsSearch.FindArticulationPoints(graph1);
                        List<int> points2 = articulationPointsBruteForce.FindArticulationPoints(graph2);
                        Assert.AreEqual(string.Join(",", points2), string.Join(",", points1), "Different result - articulationPointsSearch and articulationPointsBruteForce.");
                    }
                }

            }
        }
        //-------------------------------------------------------------------------------------------------------
    }
    //-------------------------------------------------------------------------------------------------------
}
