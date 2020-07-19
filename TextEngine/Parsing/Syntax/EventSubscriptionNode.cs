namespace TextEngine.Parsing.Syntax
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
        public Token<SyntaxKind> NameToken { get; }
        public Token<SyntaxKind> KeywordToken { get; }
    }
}