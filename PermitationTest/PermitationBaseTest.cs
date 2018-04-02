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
