using System.Collections.Generic;

namespace TextEngine.Parsing.Syntax
{
    public class TellNode : SyntaxNode
    {
        public TellNode(Token<SyntaxKind> messageToken)
        {
            MessageToken = messageToken;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Token<SyntaxKind> MessageToken { get; }
    }
}