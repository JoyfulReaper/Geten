using Geten.Core.Parsing;

namespace Geten.Parsers.Script.Syntax
{
    public abstract class TargetedNode : SyntaxNode
    {
        public TargetedNode(Token<SyntaxKind> action, Token<SyntaxKind> argument, Token<SyntaxKind> name, Token<SyntaxKind> from, Token<SyntaxKind> target)
        {
            Action = action;
            Argument = argument;
            Name = name;
            From = from;
            Target = target;
        }

        public Token<SyntaxKind> Action { get; }
        public Token<SyntaxKind> Argument { get; }
        public Token<SyntaxKind> From { get; }
        public Token<SyntaxKind> Name { get; }
        public Token<SyntaxKind> Target { get; }
    }
}