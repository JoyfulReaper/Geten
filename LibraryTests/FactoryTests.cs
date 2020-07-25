using Geten;
using Geten.Core;
using Geten.Core.Exceptions;
using Geten.Factories;
using Geten.GameObjects;
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
            var item = ObjectFactory.Create<Item>("sword");
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