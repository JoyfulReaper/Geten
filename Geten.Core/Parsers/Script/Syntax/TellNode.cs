using Geten.Core.Parsing;

namespace Geten.Core.Parsers.Script.Syntax
{
    public class TellNode : SyntaxNode
    {
        public TellNode(Token<SyntaxKind> messageToken)
        {
            MessageToken = messageToken;
        }

        public Token<SyntaxKind> MessageToken { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}