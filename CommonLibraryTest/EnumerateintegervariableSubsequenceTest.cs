using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CommonLibrary;
using System.Linq;

namespace CommonLibraryTest
{
    [TestClass]
    public class EnumerateintegervariableSubsequenceTest
    {
        [TestMethod]
        public void NotDecreasingExecuteTestCase1()
        {
            // arrange
            List<string> expectedResult = new List<string>()
            {
                "agct,taga,gatc",
                "agct,taga,atct",
                "agct,taga,tcta",
                "agct,agac,gatc",
                "agct,agac,atct",
                "agct,agac,tcta",
                "agct,gacg,gatc",
                "agct,gacg,atct",
                "agct,gacg,tcta",

                "gctg,taga,gatc",
                "gctg,taga,atct",
                "gctg,taga,tcta",
                "gctg,agac,gatc",
                "gctg,agac,atct",
                "gctg,agac,tcta",
                "gctg,gacg,gatc",
                "gctg,gacg,atct",
                "gctg,gacg,tcta",

                "ctgc,taga,gatc",
                "ctgc,taga,atct",
                "ctgc,taga,tcta",
                "ctgc,agac,gatc",
                "ctgc,agac,atct",
                "ctgc,agac,tcta",
                "ctgc,gacg,gatc",
                "ctgc,gacg,atct",
                "ctgc,gacg,tcta",


            };
            EnumerateintegervariableSubsequenceSimple enumeration = new EnumerateintegervariableSubsequenceSimple(new char[][]
            {
                new char[]{ 'a', 'g', 'c', 't', 'g', 'c' },
                new char[]{ 't', 'a', 'g', 'a', 'c', 'g' },
                new char[]{ 'g', 'a', 't', 'c', 't', 'a' },
                },
            4);
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
    public class EnumerateintegervariableSubsequenceSimple : EnumerateintegervariableSubsequence
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
        public EnumerateintegervariableSubsequenceSimple(char[][] pCharSet, int pSubstringLength, int[] pMinimumValue = null)
            : base(pCharSet, pSubstringLength, pMinimumValue)
        {
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
                _result.Add(string.Join(",", Enumerable.Range(0, _fSize).Select(i => string.Join("", _charSets[i].Skip(_fCurrentSet[i]).Take(_substringLength).Select( c => Convert.ToString(c))))));
            return false;
        }
        //--------------------------------------------------------------------------------------
    }
}
