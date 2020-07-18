using System;
using TextEngine.CommandParsing;
using TextEngine.Parsing.Text;

namespace DumbTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Command: ");
            CommandLexer l = new CommandLexer(SourceText.From(Console.ReadLine()));
            var r = l.getAllTokens();

            foreach (var t in r)
                Console.WriteLine(t);
        }
    }
}
