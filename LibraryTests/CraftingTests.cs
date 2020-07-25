using Geten;
using Geten.Core;
using Geten.Core.Crafting;
using Geten.Factories;
using Geten.GameObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryTests
{
    [TestClass]
    public class CraftingTests
    {
        private RecipeBook book;
        private Inventory inventory;
        private Recipe recipe;

        [TestMethod]
        public void Craft_Sword_Should_Pass()
        {
            var output = CraftingTable.Craft(recipe, inventory);

            Assert.AreEqual(output.Name, "iron sword");
        }

        [TestInitialize]
        public void Init()
        {
            if (!ObjectFactory.IsRegisteredFor<GameObject>())
            {
                ObjectFactory.Register<GameObjectFactory, GameObject>();
            }

            inventory = new Inventory(10);
            inventory.AddItem(GameObject.Create<Item>("wood"));
            inventory.AddItem(GameObject.Create<Item>("iron"));

            book = GameObject.Create<RecipeBook>("test");
            var ingredients = new Ingredients
            {
                ["wood"] = 4,
                ["iron"] = 3
            };

            recipe = new Recipe("best sword ever", ingredients, GameObject.Create<Weapon>("iron sword"));

            book.Add(recipe);
        }

        [TestMethod]
        public void IsCraftable_Should_Pass()
        {
            Assert.IsTrue(CraftingTable.IsCraftable(recipe, inventory));
        }
    }
}