using System;
using System.Collections.Generic;
using TextEngine.Parsing;
using TextEngine.Parsing.Syntax;
using TextEngine.Parsing.Text;

namespace DumbTests
{
    public static class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Enter Command: ");
            //var input = Console.ReadLine();

            var input = "weapon \"sword\" with mindamage 10 and maxdamage 35 end end";
            ScriptLexer l = new ScriptLexer(SourceText.From(input));
            var r = l.GetAllTokens();

            foreach (var t in r)
                Console.WriteLine(t);

            Console.WriteLine("--------------------------------------------------");

            ScriptParser p = new ScriptParser();
            BlockNode bn = (BlockNode)p.Parse("weapon \"sword\" with mindamage 10 and maxdamage 35 end end");
            VistChildNode(bn);
        }

        private static void VistChildNode(SyntaxNode node)
        {
            Console.WriteLine(node.GetType());
            if (node is BlockNode bn)
            {
                foreach (var child in bn.Children)
                    VistChildNode(child);
            } else
            {
                var type = node.GetType();
                var properties = type.GetProperties();
                foreach(var prop in properties)
                {
                    if (prop.DeclaringType == typeof(TextSpan)) continue;
                    if(prop.Name == "Properties")
                    { 
                        Dictionary<string, object> dict = (Dictionary<string, object>) prop.GetValue(node);
                        foreach (KeyValuePair<string, object> entry in dict)
                        {
                            Console.WriteLine(entry.Key + " " + entry.Value);

                        }
                    }
                    Console.WriteLine($"{prop.Name}: {prop.GetValue(node)}");
                }
            }
        }
    }
}
