using Geten.Core.Parsing;

namespace Geten.Parsers.Script.Syntax
{
    public class TellNode : SyntaxNode
    {
        public TellNode(Token<SyntaxKind> messageToken)
        {
            MessageToken = messageToken;
        }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Token<SyntaxKind> MessageToken { get; }
    }
}