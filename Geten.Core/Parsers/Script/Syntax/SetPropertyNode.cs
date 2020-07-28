using Geten.Core.Parsing;

namespace Geten.Core.Parsers.Script.Syntax
{
    public class SetPropertyNode : SyntaxNode
    {
        public SetPropertyNode(Token<SyntaxKind> setPropertyKeyword, Token<SyntaxKind> ofKeyword, Token<SyntaxKind> target, Token<SyntaxKind> property, SyntaxNode value)
        {
            SetPropertyKeyword = setPropertyKeyword;
            Target = target;
            Property = property;
            Value = value;
            OfKeyword = ofKeyword;
        }

        public Token<SyntaxKind> OfKeyword { get; }

        public Token<SyntaxKind> Property { get; }

        public Token<SyntaxKind> SetPropertyKeyword { get; }

        public Token<SyntaxKind> Target { get; }

        public SyntaxNode Value { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}