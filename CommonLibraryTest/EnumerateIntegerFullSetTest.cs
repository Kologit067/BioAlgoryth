using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CommonLibrary;
using System.Linq;

namespace CommonLibraryTest
{
    [TestClass]
    public class EnumerateIntegerFullSetTest
    {
        [TestMethod]
        public void ExecuteTestCase1()
        {
            // arrange
            List<string> expectedResult = new List<string>()
            {
                "0,0,0",
                "0,0,1",
                "0,0,2",
                "0,0,3",
                "0,1,0",
                "0,1,1",
                "0,1,2",
                "0,1,3",
                "0,2,0",
                "0,2,1",
                "0,2,2",
                "0,2,3",
                "0,3,0",
                "0,3,1",
                "0,3,2",
                "0,3,3",
                "1,0,0",
                "1,0,1",
                "1,0,2",
                "1,0,3",
                "1,1,0",
                "1,1,1",
                "1,1,2",
                "1,1,3",
                "1,2,0",
                "1,2,1",
                "1,2,2",
                "1,2,3",
                "1,3,0",
                "1,3,1",
                "1,3,2",
                "1,3,3",
                "2,0,0",
                "2,0,1",
                "2,0,2",
                "2,0,3",
                "2,1,0",
                "2,1,1",
                "2,1,2",
                "2,1,3",
                "2,2,0",
                "2,2,1",
                "2,2,2",
                "2,2,3",
                "2,3,0",
                "2,3,1",
                "2,3,2",
                "2,3,3",
                "3,0,0",
                "3,0,1",
                "3,0,2",
                "3,0,3",
                "3,1,0",
                "3,1,1",
                "3,1,2",
                "3,1,3",
                "3,2,0",
                "3,2,1",
                "3,2,2",
                "3,2,3",
                "3,3,0",
                "3,3,1",
                "3,3,2",
                "3,3,3"
            };
            EnumerateIntegerFullSetSimple enumeration = new EnumerateIntegerFullSetSimple(3, 3);
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
        public void ExecuteTestCase2()
        {
            // arrange
            List<string> expectedResult = new List<string>()
            {
                "2,2,2",
                "2,2,3",
                "2,2,4",
                "2,3,2",
                "2,3,3",
                "2,3,4",
                "2,4,2",
                "2,4,3",
                "2,4,4",
                "3,2,2",
                "3,2,3",
                "3,2,4",
                "3,3,2",
                "3,3,3",
                "3,3,4",
                "3,4,2",
                "3,4,3",
                "3,4,4",
                "4,2,2",
                "4,2,3",
                "4,2,4",
                "4,3,2",
                "4,3,3",
                "4,3,4",
                "4,4,2",
                "4,4,3",
                "4,4,4"
            };
            EnumerateIntegerFullSetSimple enumeration = new EnumerateIntegerFullSetSimple(4, 3, 2);
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

    public class EnumerateIntegerFullSetSimple : EnumerateIntegerFullSet
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
        public EnumerateIntegerFullSetSimple(int pLimit, int pLength, int pMinimumValue = 0)
            : base(pLimit, pLength, pMinimumValue)
        {
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (fCurrentPosition == _fSize - 1)
                _result.Add(string.Join(",", fCurrentSet.Select(t => t.ToString())));
            return false;
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------

}
