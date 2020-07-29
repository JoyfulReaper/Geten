using Geten.Core;
using Geten.Core.Crafting;
using Geten.Core.Factories;
using Geten.Core.GameObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryTests
{
	[TestClass]
	public class CraftingTests
	{
		private RecipeBook _book;
		private Inventory _inventory;
		private Recipe _recipe;

		[TestMethod]
		public void Craft_Sword_Should_Pass()
		{
			var output = CraftingTable.Craft(_recipe, _inventory);

			Assert.AreEqual(output.Name, "iron sword");
		}

		[TestInitialize]
		public void Init()
		{
			SymbolTable.ClearAllSymbols();
			if (!ObjectFactory.IsRegisteredFor<GameObject>())
			{
				ObjectFactory.Register<GameObjectFactory, GameObject>();
			}

			_inventory = new Inventory(10);
			_inventory.AddItem(GameObject.Create<Item>("wood"), 4);
			_inventory.AddItem(GameObject.Create<Item>("iron"), 3);

			_book = GameObject.Create<RecipeBook>("test");
			var ingredients = new Ingredients
			{
				["wood"] = 4,
				["iron"] = 3
			};

			_recipe = new Recipe("best sword ever", ingredients, GameObject.Create<Weapon>("iron sword"));

			_book.Add(_recipe);
		}

		[TestMethod]
		public void IsCraftable_Should_Pass()
		{
			Assert.IsTrue(CraftingTable.IsCraftable(_recipe, _inventory));
		}
	}
}