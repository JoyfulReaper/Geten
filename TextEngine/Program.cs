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
using Geten.Core.Factories;
using Geten.Core.MapItems;
using Geten.Core.Parsers.Script;
using System;
using System.IO;

namespace Geten
{
	internal static class Program
	{
		private static void Main()
		{
			//needed to create new instances of all kind of gameobjects
			ObjectFactory.Register<GameObjectFactory, GameObject>();

			ShowIntro();

			/*var script = System.IO.File.ReadAllText("Demo.script");
			var scriptParser = new ScriptParser();
			var result = scriptParser.Parse(script);
			result.Accept(new EvaluationVisitor(scriptParser.Diagnostics));
			*/

			//ToDo: ask user which game to start, when no game is installed display message and suggest some random games
			if (!GameRepository.IsAnyGameInstalled())
			{
				Console.WriteLine("No Games are installed. Please try:");
				Console.WriteLine("install game <gamename>");
				Console.WriteLine("or");
				Console.WriteLine("suggest games");
			}

			var wrapper = new GreedyWrap(Console.WindowWidth);
			TextEngine.StartGame();

			while (!TextEngine.GameOver)
			{
				wrapper.LineWidth = Console.WindowWidth;
				while (TextEngine.HasMessage())
				{
					Console.WriteLine(wrapper.LineWrap(TextEngine.GetMessage()));
				}

				ProcessExits(TextEngine.Player.Location);
				ProccessItems(TextEngine.Player.Location);

				Console.Write("\nEnter command: ");
				var input = Console.ReadLine();
				Console.WriteLine();

				TextEngine.ProccessCommand(input);
			}
		}

		private static void ProccessItems(Room room)
		{
			if (!room.GetProperty<bool>("lookedAt") || room.Inventory.Count == 0)
			{
				return;
			}

			Console.WriteLine("\nItems: ");
			var items = room.Inventory.GetAll();
			foreach (var item in items)
			{
				if (item.Key.GetProperty<bool>("visible"))
					Console.Write($"({item.Value}) {(item.Value > 1 ? item.Key.GetProperty<string>("PluralName") : item.Key.Name)} ");
			}
		}

		private static void ProcessExits(Room room)
		{
			if (!room.GetProperty<bool>("lookedAt"))
			{
				return;
			}

			Console.Write("\nExits: ");
			var sides = room.GetAllSides();
			foreach (var side in sides)
			{
				if (side.Value is Exit e && e.GetProperty<bool>("visible"))
				{
					Console.Write(side.Key.ToString() + " ");
				}
			}
		}

		private static void ShowIntro()
		{
			Console.WriteLine("----------------------------------------------------");
			Console.WriteLine("Geten - General Text Adventure Engine");
			Console.WriteLine("MIT License Copyright 2020 Kyle Givler and filmee24");
			Console.WriteLine("https://github.com/JoyfulReaper/Geten");
			Console.WriteLine("----------------------------------------------------\n");
		}
	}
}