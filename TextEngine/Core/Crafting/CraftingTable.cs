using Geten.GameObjects;
using System.Collections.Generic;

namespace Geten.Core.Crafting
{
    public static class CraftingTable
    {
        public static Item Craft(Recipe recipe, Inventory inventory)
        {
            // Set ingredient variables
            Dictionary<string, int> ing = recipe.Ingredients;
            List<string> ingList = new List<string>(ing.Keys);
            int c = ingList.Count;

            // For each Ingredient in the Recipe, remove the required amount from this Inventory
            for (int i = 0; i < c; i++)
            {
                inventory.RemoveItem(ingList[i], ing[ingList[i]]);
            }

            return recipe.Items;
        }

        public static bool IsCraftable(Recipe recipe, Inventory inventory)
        {
            // Set ingredient variables
            Dictionary<string, int> ing = recipe.Ingredients;
            List<string> ingList = new List<string>(ing.Keys);
            int c = ingList.Count;

            // Iterate through all ingredients in the recipe
            for (int i = 0; i < c; i++)
            {
                // If the inventory contains the current Loot item, continue ...
                if (inventory.HasItem(ingList[i]))
                {
                    // If the inventory's quantity of the current Loot item
                    // is less then the Recipe's requirements, return false
                    if (inventory.ItemQuantity(ingList[i]) < ing[ingList[i]]) return false;
                }
                // .. otherwise return false
                else
                {
                    return false;
                }
            }

            // Return true if all previous checks passed
            return true;
        }
    }

    public class RecipeBook
    {
        private List<Recipe> list;

        public RecipeBook(string name)
        {
            this.list = new List<Recipe>();
            Name = name;
        }

        public List<Recipe> Contents
        {
            get { return this.list; }
        }

        public string Name { get; }

        public RecipeBook Add(Recipe recipe)
        {
            this.list.Add(recipe);

            return this;
        }

        public void Clear()
        {
            this.list.Clear();
        }

        public bool Contains(Recipe recipe)
        {
            return this.list.Contains(recipe);
        }

        public bool Contains(string name)
        {
            int c = this.list.Count;

            for (int i = 0; i < c; i++)
            {
                if (list[i].Name == name) return true;
            }

            return false;
        }

        public RecipeBook Remove(Recipe recipe)
        {
            if (this.list.Contains(recipe))
            {
                this.list.Remove(recipe);
            }

            return this;
        }

        public int Total()
        {
            return this.list.Count;
        }
    }
}