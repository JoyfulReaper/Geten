using Geten.Core;
using Geten.Core.Exceptions;
using Geten.Factories;
using Geten.GameObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryTests
{
    [TestClass]
    public class FactoryTests
    {
        [TestMethod]
        public void Create_GameObject_Item_Should_Pass()
        {
            var item = ObjectFactory.Create<Item>("sword");
        }

        [TestMethod]
        public void Create_Something_Should_Fail()
        {
            var item = ObjectFactory.Create<int>(12);
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
            ObjectFactory.IsRegisteredFor<GameObject>();
        }
    }
}