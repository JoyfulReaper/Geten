﻿using Geten.GameObjects;
using System.Collections.Generic;

namespace Geten.Core.Crafting
{

    public class RecipeBook
    {
        public RecipeBook(string name)
        {
            this.Contents = new List<Recipe>();
            Name = name;
        }

        public List<Recipe> Contents { get; }

        public string Name { get; }

        public RecipeBook Add(Recipe recipe)
        {
            this.Contents.Add(recipe);

            return this;
        }

        public void Clear()
        {
            this.Contents.Clear();
        }

        public bool Contains(Recipe recipe)
        {
            return this.Contents.Contains(recipe);
        }

        public bool Contains(string name)
        {
            int c = this.Contents.Count;

            for (int i = 0; i < c; i++)
            {
                if (Contents[i].Name == name) return true;
            }

            return false;
        }

        public RecipeBook Remove(Recipe recipe)
        {
            if (this.Contents.Contains(recipe))
            {
                this.Contents.Remove(recipe);
            }

            return this;
        }

        public int Total()
        {
            return this.Contents.Count;
        }
    }
}