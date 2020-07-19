using System;
using System.Collections.Generic;
using System.Text;

namespace TextEngine.Parsing.Syntax
{
    public class DialogCallNode : SyntaxNode
    {
        public DialogCallNode(Token<SyntaxKind> dialogKeyword, Token<SyntaxKind> target)
        {
            DialogKeyword = dialogKeyword;
            Target = target;
        }

        public Token<SyntaxKind> DialogKeyword { get; }
        public Token<SyntaxKind> Target { get; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
