using Geten.Core.Parsing;

namespace Geten.Core.Parsers.Script.Syntax
{
    public class LiteralNode : SyntaxNode
    {
        public LiteralNode(Token<SyntaxKind> valueToken)
        {
            ValueToken = valueToken;
        }

        public Token<SyntaxKind> ValueToken { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}