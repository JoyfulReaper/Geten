using Geten.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

    internal class TestObject : GameObject
    {
        public TestObject() : base("test", "nothing")
        {
        }
    }
}