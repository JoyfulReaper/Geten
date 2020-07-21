using Geten.Core;
using Geten.Core.Parsing;
using Geten.Core.Parsing.Text;
using Geten.Parsers.Script;
using Geten.Parsers.Script.Syntax;
using System;
using System.Collections.Generic;

namespace DumbTests
{
    public static class Program
    {
        static void Main(string[] args)
        {
            CaseInsensitiveString a = new CaseInsensitiveString("TeSt");
            Console.WriteLine(a);

            Console.WriteLine(a.Equals("test")); // False
            Console.WriteLine(a == "test"); // True
            Console.WriteLine(a != "test"); // False

            Environment.Exit(0);
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
