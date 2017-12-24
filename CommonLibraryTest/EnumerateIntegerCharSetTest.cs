using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CommonLibrary;
using System.Linq;

namespace CommonLibraryTest
{
    [TestClass]
    public class EnumerateIntegerCharSetTest
    {
        [TestMethod]
        public void NotDecreasingExecuteTestCase1()
        {
            // arrange
            List<string> expectedResult = new List<string>()
            {
                "aaa",
                "aag",
                "aac",
                "aat",
                "aga",
                "agg",
                "agc",
                "agt",
                "aca",
                "acg",
                "acc",
                "act",
                "ata",
                "atg",
                "atc",
                "att",

                "gaa",
                "gag",
                "gac",
                "gat",
                "gga",
                "ggg",
                "ggc",
                "ggt",
                "gca",
                "gcg",
                "gcc",
                "gct",
                "gta",
                "gtg",
                "gtc",
                "gtt",

                "caa",
                "cag",
                "cac",
                "cat",
                "cga",
                "cgg",
                "cgc",
                "cgt",
                "cca",
                "ccg",
                "ccc",
                "cct",
                "cta",
                "ctg",
                "ctc",
                "ctt",

                "taa",
                "tag",
                "tac",
                "tat",
                "tga",
                "tgg",
                "tgc",
                "tgt",
                "tca",
                "tcg",
                "tcc",
                "tct",
                "tta",
                "ttg",
                "ttc",
                "ttt",

            };
            EnumerateIntegerCharSetSimple enumeration = new EnumerateIntegerCharSetSimple(new char[] { 'a','g','c','t'}, 3, 0);
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
    public class EnumerateIntegerCharSetSimple : EnumerateIntegerCharSet
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
        public EnumerateIntegerCharSetSimple(char[] pCharSet, int pLength, int pMinimumValue = 0)
            : base(pCharSet, pLength, pMinimumValue)
        {
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (fCurrentPosition == _fSize - 1)
                _result.Add(string.Join("", values: fCurrentSet.Select(i => Convert.ToString(_charSet[i]))));
            return false;
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------

}
