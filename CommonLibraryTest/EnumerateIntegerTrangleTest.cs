using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using CommonLibrary;

namespace DNAMappingTest
{
    [TestClass]
    public class EnumerateIntegerTrangleTest
    {
        [TestMethod]
        public void NotDecreasingExecuteTestCase1()
        {
            // arrange
            List<string> expectedResult = new List<string>()
            {
                "1,1,1,1",
                "1,1,1,2",
                "1,1,1,3",
                "1,1,1,4",
                "1,1,2,2",
                "1,1,2,3",
                "1,1,2,4",
                "1,1,3,3",
                "1,1,3,4",
                "1,1,4,4",
                "1,2,2,2",
                "1,2,2,3",
                "1,2,2,4",
                "1,2,3,3",
                "1,2,3,4",
                "1,2,4,4",
                "1,3,3,3",
                "1,3,3,4",
                "1,3,4,4",
                "1,4,4,4",
                "2,2,2,2",
                "2,2,2,3",
                "2,2,2,4",
                "2,2,3,3",
                "2,2,3,4",
                "2,2,4,4",
                "2,3,3,3",
                "2,3,3,4",
                "2,3,4,4",
                "2,4,4,4",
                "3,3,3,3",
                "3,3,3,4",
                "3,3,4,4",
                "3,4,4,4",
                "4,4,4,4"
            };
            EnumerateIntegerTrangleSimple enumeration = new EnumerateIntegerTrangleSimple(4, 4, 1);
            // act
            enumeration.Execute();
            // assert
            Assert.AreEqual(expectedResult.Count, enumeration.Result.Count, "Wrong number rows in result");
            for(int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], enumeration.Result[i], $"Wrong string in position {i} - {enumeration.Result[i]}. Expected - {expectedResult[i]}");
            }

        }

        [TestMethod]
        public void NotDecreasingExecuteTestCase2()
        {
            // arrange
            List<string> expectedResult = new List<string>()
            {
                "1,1,1",
                "1,1,2",
                "1,1,3",
                "1,1,4",
                "1,1,5",
                "1,2,2",
                "1,2,3",
                "1,2,4",
                "1,2,5",
                "1,3,3",
                "1,3,4",
                "1,3,5",
                "1,4,4",
                "1,4,5",
                "1,5,5",
                "2,2,2",
                "2,2,3",
                "2,2,4",
                "2,2,5",
                "2,3,3",
                "2,3,4",
                "2,3,5",
                "2,4,4",
                "2,4,5",
                "2,5,5",
                "3,3,3",
                "3,3,4",
                "3,3,5",
                "3,4,4",
                "3,4,5",
                "3,5,5",
                "4,4,4",
                "4,4,5",
                "4,5,5",
                "5,5,5"
            };
            EnumerateIntegerTrangleSimple enumeration = new EnumerateIntegerTrangleSimple(5, 3, 1);
            // act
            enumeration.Execute();
            // assert
            Assert.AreEqual(expectedResult.Count, enumeration.Result.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], enumeration.Result[i], $"Wrong string in position {i} - {enumeration.Result[i]}. Expected - {expectedResult[i]}");
            }

        }

        [TestMethod]
        public void IncreasingExecuteTestCase1()
        {
            // arrange
            List<string> expectedResult = new List<string>()
            {
                "1,2,3,4",
                "1,2,3,5",
                "1,2,3,6",
                "1,2,3,7",
                "1,2,4,5",
                "1,2,4,6",
                "1,2,4,7",
                "1,2,5,6",
                "1,2,5,7",
                "1,2,6,7",
                "1,3,4,5",
                "1,3,4,6",
                "1,3,4,7",
                "1,3,5,6",
                "1,3,5,7",
                "1,3,6,7",
                "1,4,5,6",
                "1,4,5,7",
                "1,4,6,7",
                "1,5,6,7",
                "2,3,4,5",
                "2,3,4,6",
                "2,3,4,7",
                "2,3,5,6",
                "2,3,5,7",
                "2,3,6,7",
                "2,4,5,6",
                "2,4,5,7",
                "2,4,6,7",
                "2,5,6,7",
                "3,4,5,6",
                "3,4,5,7",
                "3,4,6,7",
                "3,5,6,7",
                "4,5,6,7"
            };
            EnumerateIntegerTrangleSimple enumeration = new EnumerateIntegerTrangleSimple(7, 4, 1, 1);
            // act
            enumeration.Execute();
            // assert
            Assert.AreEqual(expectedResult.Count, enumeration.Result.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], enumeration.Result[i], $"Wrong string in position {i} - {enumeration.Result[i]}. Expected - {expectedResult[i]}");
            }
        }


        [TestMethod]
        public void IncreasingExecuteTestCase2()
        {
            // arrange
            List<string> expectedResult = new List<string>()
            {
                "0,1,2,3",
                "0,1,2,4",
                "0,1,2,5",
                "0,1,3,4",
                "0,1,3,5",
                "0,1,4,5",
                "0,2,3,4",
                "0,2,3,5",
                "0,2,4,5",
                "0,3,4,5",
                "1,2,3,4",
                "1,2,3,5",
                "1,2,4,5",
                "1,3,4,5",
                "2,3,4,5"
            };
            EnumerateIntegerTrangleSimple enumeration = new EnumerateIntegerTrangleSimple(5, 4, 0, 1);
            // act
            enumeration.Execute();
            // assert
            Assert.AreEqual(expectedResult.Count, enumeration.Result.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], enumeration.Result[i], $"Wrong string in position {i} - {enumeration.Result[i]}. Expected - {expectedResult[i]}");
            }

        }

    }


    public class EnumerateIntegerTrangleSimple : EnumerateIntegerTrangle
    {
        private List<string> _result = new List<string>();
        //--------------------------------------------------------------------------------------
        public List<string> Result
        {
            get
            {
                return _result;
            }
        }
        //--------------------------------------------------------------------------------------
        public EnumerateIntegerTrangleSimple(int pLimit, int pLength, int pMinimumValue = 0, int pForwardAdditive = 0)
            : base(pLimit, pLength, pMinimumValue ,pForwardAdditive)
        {
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
                _result.Add(string.Join(",", _fCurrentSet.Select(t => t.ToString())));
            return false;
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
