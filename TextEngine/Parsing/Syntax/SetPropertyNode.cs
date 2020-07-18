namespace TextEngine.Parsing.Syntax
{
    public class SetPropertyNode : SyntaxNode
    {
        public SetPropertyNode(Token<SyntaxKind> setPropertyKeyword, Token<SyntaxKind> target, Token<SyntaxKind> property, SyntaxNode value)
        {
            SetPropertyKeyword = setPropertyKeyword;
            Target = target;
            Property = property;
            Value = value;
        }

        public Token<SyntaxKind> SetPropertyKeyword { get; }
        public Token<SyntaxKind> Target { get; }
        public Token<SyntaxKind> Property { get; }
        public SyntaxNode Value { get; }
    }
}
