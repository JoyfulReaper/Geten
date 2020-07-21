using Geten.Core.Parsing;

namespace Geten.Parsers.Script.Syntax
{
    public abstract class ChangeQuantityNode : SyntaxNode
    {
        public ChangeQuantityNode(Token<SyntaxKind> target, Token<SyntaxKind> amount, Token<SyntaxKind> instance)
        {
            Target = target;
            Amount = amount;
            Instance = instance;
        }

        public Token<SyntaxKind> Amount { get; }
        public Token<SyntaxKind> Instance { get; }
        public Token<SyntaxKind> Target { get; }
    }
}