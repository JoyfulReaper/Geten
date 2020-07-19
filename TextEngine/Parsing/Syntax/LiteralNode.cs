namespace TextEngine.Parsing.Syntax
{
    public class LiteralNode : SyntaxNode
    {
        public LiteralNode(Token<SyntaxKind> valueToken)
        {
            ValueToken = valueToken;
        }

        public Token<SyntaxKind> ValueToken { get; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}