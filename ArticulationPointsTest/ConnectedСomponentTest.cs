using System;
using ArticulationPoints;
using GraphLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArticulationPointsTest
{
    //-------------------------------------------------------------------------------------------------------
    // class ConnectedСomponentTest
    //-------------------------------------------------------------------------------------------------------
    [TestClass]
    public class ConnectedСomponentTest
    {
        [TestMethod]
        //-------------------------------------------------------------------------------------------------------
        public void IsConnectedTest1()
        {
            // arrange
            СonnectedСomponent connectedСomponent = new СonnectedСomponent();
            Graph<CVertex> graph = new Graph<CVertex>(6);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 4);
            graph.AddEdge(3, 5);
            graph.AddEdge(4, 5);
            bool expectedResult = false;
            // act            
            bool actualResult = connectedСomponent.IsConnected(graph);
            // assert
            Assert.AreEqual(expectedResult, actualResult, "");
        }
        //-------------------------------------------------------------------------------------------------------
        [TestMethod]
        public void IsConnectedTest2()
        {
            // arrange
            СonnectedСomponent connectedСomponent = new СonnectedСomponent();
            Graph<CVertex> graph = new Graph<CVertex>(6);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(0, 2);
            graph.AddEdge(3, 4);
            graph.AddEdge(3, 5);
            graph.AddEdge(4, 5);
            bool expectedResult = false;
            // act            
            bool actualResult = connectedСomponent.IsConnected(graph);
            // assert
            Assert.AreEqual(expectedResult, actualResult, "");
        }
        //-------------------------------------------------------------------------------------------------------
        [TestMethod]
        public void IsConnectedTest3()
        {
            // arrange
            СonnectedСomponent connectedСomponent = new СonnectedСomponent();
            Graph<CVertex> graph = new Graph<CVertex>(6);
            graph.AddEdge(0, 1);
            graph.AddEdge(2, 3);
            graph.AddEdge(4, 5);
            bool expectedResult = false;
            // act            
            bool actualResult = connectedСomponent.IsConnected(graph);
            // assert
            Assert.AreEqual(expectedResult, actualResult, "");
        }
        //-------------------------------------------------------------------------------------------------------
        [TestMethod]
        public void IsConnectedTest4()
        {
            // arrange
            СonnectedСomponent connectedСomponent = new СonnectedСomponent();
            Graph<CVertex> graph = new Graph<CVertex>(6);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 5);
            bool expectedResult = true;
            // act            
            bool actualResult = connectedСomponent.IsConnected(graph);
            // assert
            Assert.AreEqual(expectedResult, actualResult, "");
        }
        //-------------------------------------------------------------------------------------------------------
    }
    //-------------------------------------------------------------------------------------------------------
}
