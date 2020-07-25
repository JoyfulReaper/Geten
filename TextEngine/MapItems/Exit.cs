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
using Geten.GameObjects;
using Geten.Parsers.Script.Syntax;

namespace Geten.MapItems
{
    /// <summary>
    /// Represents and exit from one MapSite to another
    /// </summary>
    public class Exit : MapSite
    {
        /// <summary>
        /// The Room on the other side of the exit
        /// </summary>
        public Room ToRoom => SymbolTable.GetInstance<Room>(GetProperty<string>("toroom"));

        /// <summary>
        /// Attempt to Enter (Use) this exit
        /// </summary>
        /// <param name="character">The Character attempting to use this Exit</param>
        /// <param name="heading">The Character's heading</param>
        public override void Enter(Character character, Direction heading)
        {
            if (GetProperty<bool>("islocked"))
                ToRoom.Enter(character, heading);
            else
                TextEngine.AddMessage("You try to go though the " + Name + ", but it is locked.");
        }

        public override void Initialize(PropertyList properties)
        {
            base.Initialize(properties);
            AddDefaultValue("visible", true);
        }
    }
}