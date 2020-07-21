using Geten.Core.Parsing;

namespace Geten.Parsers.Script.Syntax
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

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Token<SyntaxKind> SetPropertyKeyword { get; }
        public Token<SyntaxKind> Target { get; }
        public Token<SyntaxKind> Property { get; }
        public SyntaxNode Value { get; }
        public Token<SyntaxKind> OfKeyword { get; }
    }
}
