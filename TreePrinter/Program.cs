using System;
using System.Linq;
using TextEngine.MapItems;
using TextEngine.Parsing;

namespace TreePrinter
{
    public static class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.Write(">> ");
                var input = Console.ReadLine();

                if (input.ToLower() == "quit")
                    Environment.Exit(0);

                var parser = new ScriptParser();
                var tree = parser.Parse(input);
                if(parser.Diagnostics.Any())
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