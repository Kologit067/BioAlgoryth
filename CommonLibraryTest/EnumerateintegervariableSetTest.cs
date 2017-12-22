using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonLibrary;
using System.Collections.Generic;
using System.Linq;

namespace CommonLibraryTest
{
    [TestClass]
    public class EnumerateintegervariableSetTest
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
                "1,0,0",
                "1,0,1",
                "1,0,2",
                "1,0,3",
                "1,1,0",
                "1,1,1",
                "1,1,2",
                "1,1,3",
                "2,0,0",
                "2,0,1",
                "2,0,2",
                "2,0,3",
                "2,1,0",
                "2,1,1",
                "2,1,2",
                "2,1,3"
            };
            EnumerateintegervariableSetSimple enumeration = new EnumerateintegervariableSetSimple(new int[3] {2, 1, 3}, 3);
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
                "1,0,2",
                "1,0,3",
                "1,0,4",
                "1,0,5",
                "1,1,2",
                "1,1,3",
                "1,1,4",
                "1,1,5",
                "2,0,2",
                "2,0,3",
                "2,0,4",
                "2,0,5",
                "2,1,2",
                "2,1,3",
                "2,1,4",
                "2,1,5",
                "3,0,2",
                "3,0,3",
                "3,0,4",
                "3,0,5",
                "3,1,2",
                "3,1,3",
                "3,1,4",
                "3,1,5"
            };
            EnumerateintegervariableSetSimple enumeration = new EnumerateintegervariableSetSimple(new int[3] { 3, 1, 5 }, 3, new int[] { 1, 0, 2});
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


    public class EnumerateintegervariableSetSimple : EnumerateintegervariableSet
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
        public EnumerateintegervariableSetSimple(int[] pLimits, int pLength, int[] pMinimumValues = null)
            : base(pLimits, pLength, pMinimumValues)
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
