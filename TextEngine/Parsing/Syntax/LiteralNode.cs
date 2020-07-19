namespace TextEngine.Parsing.Syntax
{
    public class LiteralNode : SyntaxNode
    {
        public LiteralNode(Token<SyntaxKind> valueToken)
        {
            ValueToken = valueToken;
        }

        public Token<SyntaxKind> ValueToken { get; }
    }
}