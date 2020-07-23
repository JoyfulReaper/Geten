using Geten.GameObjects;
using System;
using System.Collections.Generic;

namespace Geten.Core.Crafting
{
    public class Recipe
    {
        public Recipe()
        {
        }

        public Recipe(string name, RecipeType recipeType, Ingredients ingredients, Item output)
        {
            this.Name = name;
            this.Type = recipeType;
            this.Ingredients = ingredients;
            this.Items = output;
        }

        public enum RecipeType { Equipment, Consumable, Upgrade };

        public Ingredients Ingredients { get; set; }

        public Item Items { get; set; }

        public string Name { get; set; }

        public RecipeType Type { get; set; }

        public override bool Equals(object obj)
        {
            if (GetHashCode() == obj.GetHashCode())
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Type, Ingredients, Items);
        }
    }
}