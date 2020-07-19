namespace TextEngine.Parsing.Syntax
{
    public class WeaponDefinitionNode : PropertyOnlyBasedCommand
    {
        public WeaponDefinitionNode(Token<SyntaxKind> keywordToken, Token<SyntaxKind> nameToken, Token<SyntaxKind> withToken, PropertyList properties, BlockNode body) : base(keywordToken, nameToken, withToken, properties, body)
        {
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}