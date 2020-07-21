using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextEngine;

namespace LibraryTests
{
    [TestClass]
    public class LibTests
    {
        [TestMethod]
        public void Compare_CaseInsensitiveString_Should_Pass()
        {
            Assert.IsTrue("hElLO WoRld" == (CaseInsensitiveString)"hello world");
        }
    }
}