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
    }
}