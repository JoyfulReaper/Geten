using System;
using System.Collections.Generic;
using System.Text;

namespace TextEngine.Parsing.Syntax
{
    class IncreaseNode : SyntaxNode
    {
        public Token<SyntaxKind> IncreaseKeyword { get; }
        public Token<SyntaxKind> IncreaseTarget { get; }
        public Token<SyntaxKind> OfKeyword { get; }
        public Token<SyntaxKind> Instance { get; }
        public Token<SyntaxKind> ByKeyword { get; }
        public Token<SyntaxKind> IncreaseAmount { get; }
        public IncreaseNode(Token<SyntaxKind> increaseKeyword, Token<SyntaxKind> increaseTarget, Token<SyntaxKind> ofKeyword, Token<SyntaxKind> instance, Token<SyntaxKind> byKeyword, Token<SyntaxKind> increaseAmount)
        {
            IncreaseKeyword = increaseKeyword;
            IncreaseTarget = increaseTarget;
            OfKeyword = ofKeyword;
            Instance = instance;
            ByKeyword = byKeyword;
            IncreaseAmount = increaseAmount;
        }
    }
}
