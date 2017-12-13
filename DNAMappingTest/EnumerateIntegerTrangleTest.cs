using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DNAMapping.Enumeration;
using System.Collections.Generic;
using System.Linq;

namespace DNAMappingTest
{
    [TestClass]
    public class EnumerateIntegerTrangleTest
    {
        [TestMethod]
        public void ExecuteTest()
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
                "4,3,4,4"
            };
            EnumerateIntegerTrangleSimple enumeration = new EnumerateIntegerTrangleSimple(4, 4);
            // act
            enumeration.Execute();
            // assert
            Assert.AreEqual(expectedResult.Count, enumeration.Result.Count, "Wrong number rows in result");
            for(int i = 0; i < expectedResult.Count; i++)
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
        public EnumerateIntegerTrangleSimple(int pLimit, int pLength)
            : base(pLength, pLength)
        {
        }
        //--------------------------------------------------------------------------------------
        protected override void MakeAction()
        {
            _result.Add(string.Join(",", fCurrentSet.Select(t => t.ToString()));
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
