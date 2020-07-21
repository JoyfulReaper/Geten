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

        [TestMethod]
        public void GameObject_Initializer_Should_Pass()
        {
            var go = new TestObject {
                { "name", "friend" },
                { "attackable", false }
            };

            Assert.AreEqual(go.PropertyCount, 2);
        }
    }

    class TestObject : GameObject { }
}