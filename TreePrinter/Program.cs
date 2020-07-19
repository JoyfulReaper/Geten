﻿using System;
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
                var parser = new ScriptParser();
                var tree = parser.Parse(input);
                SyntaxNode.PrettyPrint(tree);
            }
        }
    }
}