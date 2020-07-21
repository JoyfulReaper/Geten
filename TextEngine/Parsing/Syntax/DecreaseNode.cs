namespace TextEngine.Parsing.Syntax
{
    public class DecreaseNode : ChangeQuantityNode
    {
        public Token<SyntaxKind> IncreaseKeyword { get; }
        public Token<SyntaxKind> IncreaseTarget { get; }
        public Token<SyntaxKind> OfKeyword { get; }
        public Token<SyntaxKind> Instance { get; }
        public Token<SyntaxKind> ByKeyword { get; }
        public Token<SyntaxKind> IncreaseAmount { get; }
        
        public DecreaseNode(Token<SyntaxKind> increaseKeyword, Token<SyntaxKind> increaseTarget, Token<SyntaxKind> ofKeyword, Token<SyntaxKind> instance, Token<SyntaxKind> byKeyword, Token<SyntaxKind> increaseAmount) : base (increaseTarget, increaseAmount, instance)
        {
            IncreaseKeyword = increaseKeyword;
            OfKeyword = ofKeyword;
            ByKeyword = byKeyword;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}