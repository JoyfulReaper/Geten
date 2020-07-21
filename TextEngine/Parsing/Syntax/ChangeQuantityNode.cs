using System;
using System.Collections.Generic;
using System.Text;

namespace TextEngine.Parsing.Syntax
{
    public abstract class ChangeQuantityNode : SyntaxNode
    {
        public ChangeQuantityNode(Token<SyntaxKind> target, Token<SyntaxKind> amount, Token<SyntaxKind> instance)
        {
            Target = target;
            Amount = amount;
            Instance = instance;
        }

        public Token<SyntaxKind> Target { get; }
        public Token<SyntaxKind> Amount { get; }
        public Token<SyntaxKind> Instance { get; }
    }
}
