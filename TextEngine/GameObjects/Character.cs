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

using Geten.Core;
using Geten.MapItems;
using System;

namespace Geten.GameObjects
{
    /// <summary>
    /// Represents a Character
    /// </summary>
    public abstract class Character : GameObject
    {
        /// <summary>
        /// Constructs a Character
        /// </summary>
        /// <param name="name">The Character's name</param>
        /// <param name="health">The Character's initial health</param>
        /// <param name="maxHealth">The Character's maximum health</param>
        /// <param name="desc">The Character's description</param>
        public Character(string name = "Character", string desc = "", int health = 100, int maxHealth = 100)
          : base(name, desc)
        {
            Health = health;
            MaxHealth = maxHealth;
            CharacterMoney = new Money(null, 0);
            Inventory = new Inventory();
        }

        public Character()
            : base(null, null)
        {
            CharacterMoney = new Money(null, 0);
            Inventory = new Inventory();
        }

        /// <summary>
        /// The amount of local currence that the character has
        /// </summary>
        public Money CharacterMoney { get; set; }

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
                else if (GetProperty<int>(nameof(Health)) > MaxHealth)
                    SetProperty(nameof(Health), MaxHealth);
                else
                    SetProperty(nameof(Health), value);
            }
        }

        /// <summary>
        /// The Character's Inventory
        /// </summary>
        public Inventory Inventory { get; }

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