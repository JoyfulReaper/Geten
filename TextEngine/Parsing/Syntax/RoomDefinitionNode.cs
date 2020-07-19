namespace TextEngine.Parsing.Syntax
{
    public class RoomDefinitionNode : PropertyOnlyBasedCommand
    {
        public RoomDefinitionNode(Token<SyntaxKind> keywordToken, Token<SyntaxKind> nameToken, Token<SyntaxKind> withToken, PropertyList properties, BlockNode body) : base(keywordToken, nameToken, withToken, properties, body)
        {
        }
    }
}
