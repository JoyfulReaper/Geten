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

using System;

namespace Geten
{
    /// <summary>
    /// Represents a Weapon
    /// </summary>
    public class Weapon : Item
    {
        private int maxDamage;

        private int minDamage;

        public Weapon(string name, string pluralName, string desc, int minDamage, int maxDamage, bool visible, bool obtainable)
                        : base(name, pluralName, desc, visible, obtainable)
        {
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }

        /// <summary>
        /// Construct a weapon
        /// </summary>
        /// <param name="name">The name of the weapon</param>
        public Weapon(string name) : base(name) { }

        /// <summary>
        /// The maximum amount of Dmage a weapon is capable of inflicting
        /// </summary>
        public int MaxDamage
        {
            get => maxDamage;
            set
            {
                if (value < 0)
                    throw (new ArgumentOutOfRangeException("MaxDamage must be >= 0"));
                maxDamage = value;
            }
        }

        /// <summary>
        /// The minimum amount of Damage a weapon is capable of inflicting
        /// </summary>
        public int MinDamage
        {
            get => minDamage;
            set
            {
                if (value < 0)
                    throw (new ArgumentOutOfRangeException("MinDamage must be >= 0"));
                minDamage = value;
            }
        }
    }
}