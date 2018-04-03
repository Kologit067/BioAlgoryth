using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonLibrary;
using System.Collections.Generic;
using System.Linq;

namespace PermitationTest
{
    [TestClass]
    public class PermitationBaseTest
    {
        [TestMethod]
        public void Permitation33Test()
        {
            // arrange
            PermitationSimple permitationSimple = new PermitationSimple(3, 3);
            List<string> expectedPermitations = new List<string>()
            {
                "1,2,3",
                "1,3,2",
                "2,1,3",
                "2,3,1",
                "3,1,2",
                "3,2,1",
            };
            // act
            permitationSimple.Execute();
            // assert
            Assert.AreEqual(permitationSimple.PermitationList.Count, expectedPermitations.Count, "Wrong number of permitation");
            foreach (var e in expectedPermitations)
            {
                Assert.IsTrue(permitationSimple.PermitationList.Contains(e), $"{e} is absent in result.");
            }
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void Permitation44Test()
        {
            // arrange
            PermitationSimple permitationSimple = new PermitationSimple(4, 4);
            List<string> expectedPermitations = new List<string>()
            {
                "1,2,3,4",
                "1,2,4,3",
                "1,3,2,4",
                "1,3,4,2",
                "1,4,2,3",
                "1,4,3,2",
                "2,1,3,4",
                "2,1,4,3",
                "2,3,1,4",
                "2,3,4,1",
                "2,4,1,3",
                "2,4,3,1",
                "3,1,2,4",
                "3,1,4,2",
                "3,2,1,4",
                "3,2,4,1",
                "3,4,1,2",
                "3,4,2,1",
                "4,1,2,3",
                "4,1,3,2",
                "4,2,1,3",
                "4,2,3,1",
                "4,3,1,2",
                "4,3,2,1",
            };
            // act
            permitationSimple.Execute();
            // assert
            Assert.AreEqual(permitationSimple.PermitationList.Count, expectedPermitations.Count, "Wrong number of permitation");
            foreach (var e in expectedPermitations)
            {
                Assert.IsTrue(permitationSimple.PermitationList.Contains(e), $"{e} is absent in result.");
            }
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void Permitation35Test()
        {
            // arrange
            PermitationSimple permitationSimple = new PermitationSimple(3, 5);
            List<string> expectedPermitations = new List<string>()
            {
                "1,2,3",
                "1,2,4",
                "1,2,5",
                "1,3,2",
                "1,3,4",
                "1,3,5",
                "1,4,2",
                "1,4,3",
                "1,4,5",
                "1,5,2",
                "1,5,3",
                "1,5,4",

                "2,1,3",
                "2,1,4",
                "2,1,5",
                "2,3,1",
                "2,3,4",
                "2,3,5",
                "2,4,1",
                "2,4,3",
                "2,4,5",
                "2,5,1",
                "2,5,3",
                "2,5,4",

                "3,2,1",
                "3,2,4",
                "3,2,5",
                "3,1,2",
                "3,1,4",
                "3,1,5",
                "3,4,2",
                "3,4,1",
                "3,4,5",
                "3,5,2",
                "3,5,1",
                "3,5,4",

                "4,2,3",
                "4,2,1",
                "4,2,5",
                "4,3,2",
                "4,3,1",
                "4,3,5",
                "4,1,2",
                "4,1,3",
                "4,1,5",
                "4,5,2",
                "4,5,3",
                "4,5,1",

                "5,2,3",
                "5,2,4",
                "5,2,1",
                "5,3,2",
                "5,3,4",
                "5,3,1",
                "5,4,2",
                "5,4,3",
                "5,4,1",
                "5,1,2",
                "5,1,3",
                "5,1,4"

            };
            // act
            permitationSimple.Execute();
            // assert
            Assert.AreEqual(permitationSimple.PermitationList.Count, expectedPermitations.Count, "Wrong number of permitation");
            foreach (var e in expectedPermitations)
            {
                Assert.IsTrue(permitationSimple.PermitationList.Contains(e), $"{e} is absent in result.");
            }
        }
        //--------------------------------------------------------------------------------------

    }
    //--------------------------------------------------------------------------------------
    // class PermitationSimple
    //--------------------------------------------------------------------------------------
    public class PermitationSimple : PermitationBase
    {
        private List<string> _permitationList = new List<string>();
        public List<string> PermitationList
        {
            get
            {
                return _permitationList;
            }
        }
        //--------------------------------------------------------------------------------------
        public PermitationSimple(int pSize, int pLimit)
            : base(pSize, pLimit)
        {
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            if (_fCurrentPosition == _fSize - 1)
            {
                var str = string.Join(",", _fCurrentSet.Select(i => i + 1));
                _permitationList.Add(str);
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
