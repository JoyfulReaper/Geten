using Geten;
using Geten.Core.Crafting;
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

            Assert.AreEqual(inventory.Count, 3);
        }

        [TestInitialize]
        public void Init()
        {
            inventory = new Inventory(100);
            inventory.AddItem("wood", 5);
            inventory.AddItem("iron", 4);

            book = new RecipeBook();
            var ingredients = new Ingredients
            {
                ["wood"] = 4,
                ["iron"] = 3
            };

            recipe = new Recipe("best sword ever", Recipe.RecipeType.Equipment, ingredients, new Geten.GameObjects.Item("iron_sword"));

            book.Add(recipe);
        }

        [TestMethod]
        public void IsCraftable_Should_Pass()
        {
            Assert.IsTrue(CraftingTable.IsCraftable(recipe, inventory));
        }
    }
}