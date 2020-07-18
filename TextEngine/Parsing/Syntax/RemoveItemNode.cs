using System;
using System.Collections.Generic;
using System.Text;

namespace TextEngine.Parsing.Syntax
{
    public class RemoveItemNode : TargetedNode
    {
        public RemoveItemNode(Token<SyntaxKind> action, Token<SyntaxKind> argument, Token<SyntaxKind> name, Token<SyntaxKind> from, Token<SyntaxKind> target) : base(action, argument, name, from, target)
        {
        }
    }
}
