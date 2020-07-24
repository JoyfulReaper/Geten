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

        public Recipe(string name, Ingredients ingredients, Item output)
        {
            this.Name = name;
            this.Ingredients = ingredients;
            this.Items = output;
        }

        public Ingredients Ingredients { get; set; }

        public Item Items { get; set; }

        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (GetHashCode() == obj.GetHashCode())
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Ingredients, Items);
        }
    }
}