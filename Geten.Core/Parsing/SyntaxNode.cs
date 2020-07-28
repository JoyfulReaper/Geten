using Geten.Core.Parsers.Script;
using Geten.Core.Parsers.Script.Syntax;
using Geten.Core.Parsing.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Geten.Core.Parsing
{
    public abstract class SyntaxNode
    {
        public virtual TextSpan Span
        {
            get
            {
                var children = GetChildren();
                if (children.Any())
                {
                    var first = children.First().Span;
                    var last = children.Last().Span;

                    return TextSpan.FromBounds(first.Start, last.End);
                }

                return TextSpan.FromBounds(0, 0);
            }
        }

        public static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
        {
            var marker = isLast ? "└──" : "├──";

            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.Write(indent);
            Console.Write(marker);

            Console.ForegroundColor = node is Token<SyntaxKind> ? ConsoleColor.Blue : ConsoleColor.Cyan;

            Console.Write(node);

            Console.ResetColor();

            Console.WriteLine();

            indent += isLast ? "   " : "│  ";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
                PrettyPrint(child, indent, child == lastChild);
        }

        public abstract void Accept(IScriptVisitor visitor);

        public IEnumerable<SyntaxNode> GetChildren()
        {
            var result = new List<SyntaxNode>();

            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                if (typeof(SyntaxNode).IsAssignableFrom(property.PropertyType))
                {
                    var child = (SyntaxNode)property.GetValue(this);
                    if (child != null)
                        result.Add(child);
                }
                else if (typeof(IEnumerable<SyntaxNode>).IsAssignableFrom(property.PropertyType))
                {
                    var children = (IEnumerable<SyntaxNode>)property.GetValue(this);
                    if (children != null)
                    {
                        foreach (var child in children)
                        {
                            if (child != null)
                                result.Add(child);
                        }
                    }
                }
                else if (typeof(BlockNode).IsAssignableFrom(property.PropertyType))
                {
                    var children = (BlockNode)property.GetValue(this);
                    foreach (var child in children.Children)
                    {
                        if (child != null)
                            result.Add(child);
                    }
                }
                else if (typeof(PropertyList).IsAssignableFrom(property.PropertyType))
                {
                    var children = (PropertyList)property.GetValue(this);
                    foreach (var child in children)
                    {
                        result.Add(child.Key);
                        result.Add(child.Value);
                    }
                }
            }

            return result;
        }

        public Token<SyntaxKind> GetLastToken()
        {
            if (this is Token<SyntaxKind> token)
                return token;

            // A syntax node should always contain at least 1 token.
            return GetChildren().Last().GetLastToken();
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}