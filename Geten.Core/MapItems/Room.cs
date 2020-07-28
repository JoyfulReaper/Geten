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

using Geten.Core.GameObjects;
using Geten.Core.Parsers.Script.Syntax;
using System.Collections.Generic;

namespace Geten.Core.MapItems
{
    /// <summary>
    /// Represents a room with 6 sides, one for each Direction
    /// </summary>
    public class Room : MapSite
    {
        private readonly Dictionary<Direction, MapSite> sides = new Dictionary<Direction, MapSite>();

        /// <summary>
        /// The Items contained in the Room's Inventory
        /// </summary>
        public Inventory Inventory { get; } = new Inventory();

        /// <summary>
        /// Enter the room
        /// </summary>
        /// <param name="character">The Character entering the room</param>
        /// <param name="heading">The Character's heading</param>
        public override void Enter(Character character, Direction heading)
        {
            if (character == TextEngine.Player)
                SetProperty("Visisted", true);

            character.Move(this);
            TextEngine.AddMessage(Description);
        }

        public List<Exit> GetAllExits()
        {
            var exits = new List<Exit>();
            foreach (var item in sides)
            {
                if (item.Value is Exit e)
                {
                    exits.Add(e);
                }
            }
            return exits;
        }

        public Dictionary<Direction, MapSite> GetAllSides()
        {
            return sides;
        }

        public override List<string> GetPropertyPositionMap()
        {
            var map = base.GetPropertyPositionMap();
            map.Add("description");
            map.Add("lookDescription");
            map.Add("startLocation");
            return map;
        }

        /// <summary>
        /// Get a side of the room
        /// </summary>
        /// <param name="dir">Direction of side to retreive</param>
        /// <returns>MapSite on the side of the room indicated</returns>
        public MapSite GetSide(Direction dir)
        {
            return sides[dir];
        }

        public override void Initialize(PropertyList properties)
        {
            AddDefaultValue("Visited", false);
            AddDefaultValue("startLocation", false);
            AddDefaultValue("lookedAt", false);
            base.Initialize(properties);
            InitializeSides();
        }

        /// <summary>
        /// Set the MapSite on the side of the room at the given Direction
        /// </summary>
        /// <param name="dir">The Direction of the room to add the side at (ex: Add a wall to the east)</param>
        /// <param name="site">The MapSite to add at the given Direction</param>
        public void SetSide(Direction dir, MapSite site)
        {
            sides[dir] = site;
        }

        // Should we allow this to be set as well?
        private void InitializeSides()
        {
            Wall wall = null;
            Roof roof = null;
            Floor floor = null;

            if (SymbolTable.Contains<Wall>("wall"))
            {
                wall = SymbolTable.GetInstance<Wall>("wall");
            }
            else
            {
                wall = GameObject.Create<Wall>("wall", null);
            }

            if (SymbolTable.Contains<Roof>("roof"))
            {
                roof = SymbolTable.GetInstance<Roof>("roof");
            }
            else
            {
                roof = GameObject.Create<Roof>("roof", null);
            }

            if (SymbolTable.Contains<Floor>("floor"))
            {
                floor = SymbolTable.GetInstance<Floor>("floor");
            }
            else
            {
                floor = GameObject.Create<Floor>("floor", null);
            }

            SetSide(Direction.Up, roof);
            SetSide(Direction.Down, floor);
            SetSide(Direction.North, wall);
            SetSide(Direction.South, wall);
            SetSide(Direction.East, wall);
            SetSide(Direction.West, wall);
        }
    }
}