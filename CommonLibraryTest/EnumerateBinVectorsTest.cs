using System;
using System.Collections.Generic;
using System.Linq;
using CommonLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonLibraryTest
{
    [TestClass]
    //--------------------------------------------------------------------------------------
    // class EnumerateBinVectorsTest
    //--------------------------------------------------------------------------------------
    public class EnumerateBinVectorsTest
    {
        [TestMethod]
        //--------------------------------------------------------------------------------------
        public void TestCaseSize4Manual()
        {
            List<string> expectedResult = new List<string>()
            {
                "0,0,0,0",
                "0,0,0,1",
                "0,0,1,0",
                "0,0,1,1",
                "0,1,0,0",
                "0,1,0,1",
                "0,1,1,0",
                "0,1,1,1",
                "1,0,0,0",
                "1,0,0,1",
                "1,0,1,0",
                "1,0,1,1",
                "1,1,0,0",
                "1,1,0,1",
                "1,1,1,0",
                "1,1,1,1"
            };
            EnumerateBinVectorsSimple enumeration = new EnumerateBinVectorsSimple(4);
            // act
            enumeration.Execute();
            // assert
            Assert.AreEqual(expectedResult.Count, enumeration.Result.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], enumeration.Result[i], $"Wrong string in position {i} - {enumeration.Result[i]}. Expected - {expectedResult[i]}");
            }
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        //--------------------------------------------------------------------------------------
        public void TestCaseSizeAsParameter()
        {
            int size = 10;
            List<string> expectedResult = new List<string>();
            int limit = 1 << size;
            int[] binAsAray = new int[size];
            for( int binAsNumber = 0; binAsNumber < limit; binAsNumber++)
            {
                int bin = binAsNumber;
                for (int i = size-1; i >= 0; i--)
                {
                    binAsAray[i] = bin & 1;
                    bin >>= 1;
                }
                expectedResult.Add(string.Join(",", binAsAray.Select(t => t.ToString())));
            }

            EnumerateBinVectorsSimple enumeration = new EnumerateBinVectorsSimple(size);
            // act
            enumeration.Execute();
            // assert
            Assert.AreEqual(expectedResult.Count, enumeration.Result.Count, "Wrong number rows in result");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], enumeration.Result[i], $"Wrong string in position {i} - {enumeration.Result[i]}. Expected - {expectedResult[i]}");
            }
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
    // class EnumerateBinVectorsSimple
    //--------------------------------------------------------------------------------------
    public class EnumerateBinVectorsSimple : EnumerateBinVectors
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
        public EnumerateBinVectorsSimple(int pLength)
            : base(pLength)
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
