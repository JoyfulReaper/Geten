/*
MIT License

Copyright(c) 2020 Kyle Givler

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using Geten.Core.MapItems;
using Geten.Core.Parsers.Script.Syntax;
using System;
using System.Collections.Generic;

namespace Geten.Core.GameObjects
{
    /// <summary>
    /// Represents a Character
    /// </summary>
    public abstract class Character : GameObject
    {
        /// <summary>
        /// The amount of local currence that the character has
        /// </summary>
        public Money CharacterMoney { get; set; } = new Money("", 0);

        /// <summary>
        /// Health can be any vaild int > 0. I would suggest using 0 - 100. 0 is dead
        /// </summary>
        public virtual int Health
        {
            get => GetProperty<int>(nameof(Health));
            set
            {
                if (value < 0)
                    SetProperty(nameof(Health), 0);
                else if (value > MaxHealth)
                    SetProperty(nameof(Health), MaxHealth);
                else
                    SetProperty(nameof(Health), value);
            }
        }

        /// <summary>
        /// The Character's Inventory
        /// </summary>
        public Inventory Inventory { get; } = new Inventory();

        /// <summary>
        /// The room that the Character is in
        /// </summary>
        public Room Location { get; set; }

        /// <summary>
        /// The maximum health a character can have
        /// </summary>
        public int MaxHealth
        {
            get => GetProperty<int>(nameof(MaxHealth));
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MaxHealth must be > 0");
                SetProperty(nameof(MaxHealth), value);
            }
        }

        /// <summary>
        /// The Room that the Character was in previously
        /// </summary>
        public Room PreviousLocation { get; private set; }

        public override List<string> GetPropertyPositionMap()
        {
            var map = base.GetPropertyPositionMap();
            map.Add("health");
            map.Add("maxHealth");
            map.Add("money");
            return map;
        }

        public override void Initialize(PropertyList properties)
        {
            AddDefaultValue("maxhealth", 100);
            AddDefaultValue("health", 100);
            AddDefaultValue("inventoryize", 10);
        }

        /// <summary>
        /// Check if the chacater is alive
        /// </summary>
        /// <returns>true if health > 0 other wise false;</returns>
        public virtual bool IsAlive() => Health > 0;

        /// <summary>
        /// Move the Character to another Room
        /// </summary>
        /// <param name="room">The Room to move the character to</param>
        public virtual void Move(Room room)
        {
            Location = room;
            PreviousLocation = Location;
        }

        /// <summary>
        /// A string representation of this Character
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Name: {Name}, Description {Description}, Health: {Health}, MaxHealth: {MaxHealth}";
        }
    }
}