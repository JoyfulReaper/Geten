using Geten.Core.Parsing;
using System.Collections.Generic;

namespace Geten.Parsers.Script.Syntax
{
    public class BlockNode : SyntaxNode
    {
        public BlockNode(IEnumerable<SyntaxNode> children)
        {
            Children = children;
        }

        public IEnumerable<SyntaxNode> Children { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IEnumerable<T> Descendants<T>()
            where T : SyntaxNode
        {
            foreach (var n in Children)
            {
                if (n is BlockNode nbn)
                {
                    foreach (var nc in nbn.Descendants<T>())
                    {
                        yield return nc;
                    }
                }
                else if (n is T)
                {
                    yield return (T)n;
                }
            }
        }
    }
}