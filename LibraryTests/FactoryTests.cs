using Geten;
using Geten.Core;
using Geten.Core.Exceptions;
using Geten.Factories;
using Geten.GameObjects;
using Geten.MapItems;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LibraryTests
{
    [TestClass]
    public class FactoryTests
    {
        [TestMethod]
        public void Create_Abstract_Should_Throw()
        {
            Assert.ThrowsException<ObjectFactoryException>(new Action(() =>
            {
                ObjectFactory.Create<GameObject>();
            }));
        }

        [TestMethod]
        public void Create_GameObject_Item_Should_Pass()
        {
            var item = ObjectFactory.Create<Item>("sword object");
        }

        [TestMethod]
        public void Create_Item_With_PropertyMap_Should_Pass()
        {
            dynamic item = ObjectFactory.Create<Item>("TestObject", "TestObjects", "It looks weird", true, false);
            Assert.AreEqual("TestObject", item.Name);
            Assert.AreEqual("It looks weird", item.Description);
            Assert.AreEqual(true, item.visible);
            Assert.AreEqual(false, item.obtainable);
        }

        [TestMethod]
        public void Create_Room_With_PropertyMap_Should_Pass()
        {
            dynamic room = ObjectFactory.Create<Room>("Another Test Room", "It's just for testing!", "You look very carefully, but you don't see anything that isn't testing releated!");
            Assert.AreEqual("Another Test Room", room.Name);
            Assert.AreEqual("It's just for testing!", room.Description);
        }

        [TestMethod]
        public void Create_Something_Should_Throw()
        {
            Assert.ThrowsException<ObjectFactoryException>(new Action(() =>
            {
                ObjectFactory.Create<int>(12);
            }));
        }

        [TestMethod]
        public void GetFactoryOf_Should_Pass()
        {
            var f = ObjectFactory.GetFactoryOf<Item>();

            Assert.IsTrue(f is GameObjectFactory);
        }

        [TestInitialize]
        public void Init()
        {
            if (!ObjectFactory.IsRegisteredFor<GameObject>())
            {
                ObjectFactory.Register<GameObjectFactory, GameObject>();
            }
        }

        [TestMethod]
        public void IsRegistered_Should_Pass()
        {
            Assert.IsTrue(ObjectFactory.IsRegisteredFor<GameObject>()); //test for base object
            Assert.IsTrue(ObjectFactory.IsRegisteredFor<Character>()); //test for inherited object
        }
    }
}