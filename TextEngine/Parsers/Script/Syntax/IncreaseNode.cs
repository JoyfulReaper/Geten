using Geten.Core.Parsing;

namespace Geten.Parsers.Script.Syntax
{
    public class IncreaseNode : ChangeQuantityNode
    {
        public Token<SyntaxKind> IncreaseKeyword { get; }
        public Token<SyntaxKind> IncreaseTarget { get; }
        public Token<SyntaxKind> OfKeyword { get; }
        public Token<SyntaxKind> Instance { get; }
        public Token<SyntaxKind> ByKeyword { get; }
        public Token<SyntaxKind> IncreaseAmount { get; }
        public IncreaseNode(Token<SyntaxKind> increaseKeyword, Token<SyntaxKind> increaseTarget, Token<SyntaxKind> ofKeyword, Token<SyntaxKind> instance, Token<SyntaxKind> byKeyword, Token<SyntaxKind> increaseAmount) : base (increaseTarget, increaseAmount, instance)
        {
            IncreaseKeyword = increaseKeyword;
            OfKeyword = ofKeyword;
            ByKeyword = byKeyword;
        }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
