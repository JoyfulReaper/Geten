using Geten.Core.Parsing;

namespace Geten.Parsers.Script.Syntax
{
    public class PlayNode : SyntaxNode
    {
        public PlayNode(Token<SyntaxKind> playKeyword, Token<SyntaxKind> target, Token<SyntaxKind> inKeyword, Token<SyntaxKind> loop)
        {
            PlayKeyword = playKeyword;
            Target = target;
            InKeyword = inKeyword;
            Loop = loop;
        }

        public Token<SyntaxKind> InKeyword { get; }

        public Token<SyntaxKind> Loop { get; }

        public Token<SyntaxKind> PlayKeyword { get; }

        public Token<SyntaxKind> Target { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}