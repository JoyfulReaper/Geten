using System.Collections.Generic;

namespace Geten.Core.Crafting
{
    public class Ingredients : Dictionary<string, int>
    {
        public Ingredients Add(string name, int quantity = 1)
        {
            //this.Add(name, quantity);
            base.Add(name, quantity);

            return this;
        }

        public Ingredients Remove(string name, int quantity = 1)
        {
            if (this.ContainsKey(name))
            {
                if (this[name] - quantity >= 1)
                {
                    this[name] -= quantity;
                }
                else
                {
                    this.Remove(name);
                }
            }

            return this;
        }
    }
}