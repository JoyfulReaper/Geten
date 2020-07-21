using Geten.Core.Parsing;

namespace Geten.Parsers.Script.Syntax
{
    public class EventSubscriptionNode : SyntaxNode
    {
        public EventSubscriptionNode(Token<SyntaxKind> keywordToken, Token<SyntaxKind> nameToken, BlockNode body)
        {
            Body = body;
            NameToken = nameToken;
            KeywordToken = keywordToken;
        }

        public BlockNode Body { get; }
        public Token<SyntaxKind> KeywordToken { get; }
        public Token<SyntaxKind> NameToken { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}