using Geten.Core;
using Geten.Core.Parsers.Script;
using Geten.Core.Parsing;
using System;
using System.Linq;

namespace TreePrinter
{
	public static class Program
	{
		private static void Main()
		{
			while (true)
			{
				Console.Write(">> ");
				var input = (CaseSensisitiveString)Console.ReadLine();

				if (input == "quit")
					Environment.Exit(0);

				var parser = new ScriptParser();
				var tree = parser.Parse(input);
				if (parser.Diagnostics.Any())
				{
					foreach (var d in parser.Diagnostics)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine(d);
						Console.ResetColor();
					}
				}
				SyntaxNode.PrettyPrint(tree);
			}
		}
	}
}