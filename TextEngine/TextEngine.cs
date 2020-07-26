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
using Geten.Core.Exceptions;
using Geten.GameObjects;
using Geten.MapItems;
using System;
using System.Collections.Generic;

namespace Geten
{
    /// <summary>
    /// Represent the directions the player can move in
    /// </summary>
    public enum Direction { Invalid, North, South, East, West, Up, Down };

    /// <summary>
    /// Static class for keeping track of important information
    /// </summary>
    public static class TextEngine
    {
        //private static readonly Queue<string> messages = new Queue<string>();

        private static Player player = null;

        private static Room startRoom = null;

        public delegate string InputDelegate(string prompt);

        public delegate void OutputDelegate(string message);

        /// <value>
        /// Flag indicating if the game has ended
        /// </value>
        public static bool GameOver { get; private set; } = true;

        public static InputDelegate Input { get; set; } = null;

        public static OutputDelegate Output { get; set; } = null;

        /// <summary>
        /// The active playable character
        /// </summary>
        public static Player Player
        {
            get => player;
            set
            {
                if (value == null)
                    throw (new InvalidCharacterException("Player cannot be null"));
                player = value;
            }
        }

        /// <summary>
        /// The room in which the game should begin
        /// </summary>
        public static Room StartRoom
        {
            get { return startRoom; }
            set
            {
                if (Input == null || Output == null)
                    throw new Exception("Input and Output delegates must be set");
                if (player == null)
                    throw new InvalidCharacterException("Player must be set before setting StartRoom");

                if (SymbolTable.Contains(value.Name))
                {
                    startRoom = value;
                    Player.Location = startRoom;
                    startRoom.Enter(Player, Direction.Invalid);
                }
                else
                {
                    throw new RoomDoesNotExisitException(value.Name + " has not been added to the map");
                }
            }
        }

        // public static void AddMessage(string message) => messages.Enqueue(message);

        public static void AddMessage(string message)
        {
            Output(message);
        }

        /// <summary>
        /// Get the name of a direction
        /// </summary>
        /// <param name="dir">Direction to get the name of</param>
        /// <param name="shortName">Return short name susch as "N" instead of "North"</param>
        /// <returns>String containing the name of the direction</returns>
        public static string DirectionName(Direction dir, bool shortName = false)
        {
            switch (dir)
            {
                case Direction.North:
                    if (shortName)
                        return "N";
                    return "North";

                case Direction.East:
                    if (shortName)
                        return "E";
                    return "East";

                case Direction.West:
                    if (shortName)
                        return "W";
                    return "West";

                case Direction.South:
                    if (shortName)
                        return "S";
                    return "South";

                case Direction.Up:
                    return "Up";

                case Direction.Down:
                    return "Down";

                default:
                    return "Unknown Direction";
            }
        }

        public static IEnumerable<Room> GetAllRooms()
        {
            return SymbolTable.GetAll<Room>();
        }

        public static Direction GetDirectionFromChar(char c)
        {
            switch (Char.ToUpper(c))
            {
                case 'N':
                    return Direction.North;

                case 'S':
                    return Direction.South;

                case 'E':
                    return Direction.East;

                case 'W':
                    return Direction.West;

                case 'U':
                    return Direction.Up;

                case 'D':
                    return Direction.Down;

                default:
                    return Direction.Invalid;
            }
        }

        public static Direction GetDirectionFromString(string dir)
        {
            switch (dir.ToLower())
            {
                case "north":
                    return Direction.North;

                case "south":
                    return Direction.South;

                case "east":
                    return Direction.East;

                case "west":
                    return Direction.West;

                case "up":
                    return Direction.Up;

                case "down":
                    return Direction.Down;

                default:
                    return Direction.Invalid;
            }
        }

        public static string GetInput(string prompt)
        {
            return Input(prompt);
        }

        public static bool IsCommand(string name)
        {
            var cmds = new List<string>
            {
                "quit", "exit",
                "go", "n", "s", "e", "w", "up", "down",
                "look",
                "pickup",
                "take",
                "show",
                "inv",
            };

            return cmds.Contains(name.ToLower());
        }

        public static void ProccessCommand(string command)
        {
            CommandProccessor.ProcessCommand(command);
        }

        /*
        public static string GetMessage() => messages.Dequeue();

        public static bool HasMessage() => messages.Count > 0;
        */

        /// <summary>
        /// Preform any setup and prepare fot the game to begin
        /// </summary>
        public static void StartGame()
        {
            if (!GameOver)
                throw new InvalidOperationException("A game is in progress");

            if (startRoom == null)
                throw new InvalidOperationException("The starting room has not been set");

            GameOver = false;

            // TODO: Any critical start up such as loading/placing/creating rooms, NPCs, items, ETC
        }
    }
}