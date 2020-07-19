namespace TextEngine.Parsing.Syntax
{
    public class DecreaseNode : SyntaxNode
    {
        public Token<SyntaxKind> IncreaseKeyword { get; }
        public Token<SyntaxKind> IncreaseTarget { get; }
        public Token<SyntaxKind> OfKeyword { get; }
        public Token<SyntaxKind> Instance { get; }
        public Token<SyntaxKind> ByKeyword { get; }
        public Token<SyntaxKind> IncreaseAmount { get; }
        
        public DecreaseNode(Token<SyntaxKind> increaseKeyword, Token<SyntaxKind> increaseTarget, Token<SyntaxKind> ofKeyword, Token<SyntaxKind> instance, Token<SyntaxKind> byKeyword, Token<SyntaxKind> increaseAmount)
        {
            IncreaseKeyword = increaseKeyword;
            IncreaseTarget = increaseTarget;
            OfKeyword = ofKeyword;
            Instance = instance;
            ByKeyword = byKeyword;
            IncreaseAmount = increaseAmount;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}