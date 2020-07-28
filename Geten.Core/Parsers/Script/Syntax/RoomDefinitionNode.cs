using Geten.Core.Parsing;

namespace Geten.Core.Parsers.Script.Syntax
{
    public class RoomDefinitionNode : PropertyOnlyBasedCommand
    {
        public RoomDefinitionNode(Token<SyntaxKind> keywordToken, Token<SyntaxKind> nameToken, Token<SyntaxKind> withToken, PropertyList properties, BlockNode body) : base(keywordToken, nameToken, withToken, properties, body)
        {
        }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}