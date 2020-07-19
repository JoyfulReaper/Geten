using System.Collections.Generic;

namespace TextEngine.Parsing.Syntax
{
    public class TellNode : SyntaxNode
    {
        public TellNode(Token<SyntaxKind> messageToken)
        {
            MessageToken = messageToken;
        }

        public Token<SyntaxKind> MessageToken { get; }
    }
}