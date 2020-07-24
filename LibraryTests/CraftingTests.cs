using Geten;
using Geten.Core.Crafting;
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

            Assert.AreEqual(output.Name, "iron_sword");
        }

        [TestInitialize]
        public void Init()
        {
            inventory = new Inventory(10);
            //inventory.AddItem("wood", 5);
            //inventory.AddItem("iron", 4);
            inventory.AddItem(new Item("wood", null, null, true, true), 5);
            inventory.AddItem(new Item("iron", null, null, true, true), 4);

            book = new RecipeBook("test");
            var ingredients = new Ingredients
            {
                ["wood"] = 4,
                ["iron"] = 3
            };

            recipe = new Recipe("best sword ever", ingredients, new Item("iron_sword"));

            book.Add(recipe);
        }

        [TestMethod]
        public void IsCraftable_Should_Pass()
        {
            Assert.IsTrue(CraftingTable.IsCraftable(recipe, inventory));
        }
    }
}