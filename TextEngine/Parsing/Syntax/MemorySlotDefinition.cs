namespace TextEngine.Parsing.Syntax
{
    public class MemorySlotDefinition : SyntaxNode
    {
        public MemorySlotDefinition(Token<SyntaxKind> keyword, Token<SyntaxKind> slotname, Token<SyntaxKind> equalsToken, SyntaxNode initialvalue)
        {
            KeywordToken = keyword;
            SlotnameToken = slotname;
            EqualsToken = equalsToken;
            ValueToken = initialvalue;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Token<SyntaxKind> KeywordToken { get; }
        public Token<SyntaxKind> SlotnameToken { get; }
        public Token<SyntaxKind> EqualsToken { get; }
        public SyntaxNode ValueToken { get; }
    }
}